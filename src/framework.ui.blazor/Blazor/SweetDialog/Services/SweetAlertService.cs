using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public class SweetAlertService : IAsyncSweetAlertService
    {
        const string JS_NAMESPACE = "Orion.Framework.Ui.Blazor";

        private static readonly IDictionary<Guid, TaskCompletionSource<SweetAlertResult>> PendingFireRequests =
            new Dictionary<Guid, TaskCompletionSource<SweetAlertResult>>();

        private static readonly IDictionary<Guid, TaskCompletionSource<SweetAlertQueueResult>> PendingQueueRequests =
            new Dictionary<Guid, TaskCompletionSource<SweetAlertQueueResult>>();

        private static readonly IDictionary<Guid, PreConfirmCallback> PreConfirmCallbacks =
            new Dictionary<Guid, PreConfirmCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> OnOpenCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> OnCloseCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> OnRenderCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> OnBeforeOpenCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> OnAfterCloseCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, SweetAlertCallback> OnDestroyCallbacks =
            new Dictionary<Guid, SweetAlertCallback>();

        private static readonly IDictionary<Guid, InputValidatorCallback> InputValidatorCallbacks =
            new Dictionary<Guid, InputValidatorCallback>();

        private readonly IJSRuntime jSRuntime;

        private readonly CultureInfo culture = CultureInfo.GetCultureInfo("pt-BR");

        private readonly SweetAlertTheme theme = SweetAlertTheme.Default;
        private readonly int[][] colorSchemeThemes = Array.Empty<int[]>();

        public SweetAlertService(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }

        public SweetAlertService(IJSRuntime jSRuntime, SweetAlertServiceOptions options)
        {
            this.jSRuntime = jSRuntime;
            if (options == null)
            {
                return;
            }
            theme = options.Theme;
            colorSchemeThemes = options.ColorSchemeThemes
                .Select(kvp => new int[2] { (int)kvp.Key, (int)kvp.Value })
                .ToArray();
        }

        /// <summary>
        /// Function to display a simple SweetAlert2 modal.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public async Task<SweetAlertResult> FireAsync(string title = null, string message = null, SweetAlertIcon icon = null)
        {
            var tcs = new TaskCompletionSource<SweetAlertResult>();
            Guid requestId = Guid.NewGuid();
            PendingFireRequests.Add(requestId, tcs);
            await jSRuntime.InvokeAsync<object>(
                $"{JS_NAMESPACE}.Fire",
                requestId,
                title,
                message,
                icon?.ToString(),
                (int)theme,
                colorSchemeThemes)
                .ConfigureAwait(false);
            return await tcs.Task.ConfigureAwait(false);
        }

        [JSInvokable]
        public static Task ReceiveFireResult(string requestId, SweetAlertResult result)
        {
            var requestGuid = Guid.Parse(requestId);
            PendingFireRequests.TryGetValue(requestGuid, out TaskCompletionSource<SweetAlertResult> pendingTask);
            PendingFireRequests.Remove(requestGuid);
            pendingTask.SetResult(result);
            return Task.CompletedTask;
        }

        [JSInvokable]
        public static Task ReceiveQueueResult(string requestId, SweetAlertQueueResult result)
        {
            var requestGuid = Guid.Parse(requestId);
            PendingQueueRequests.TryGetValue(requestGuid, out TaskCompletionSource<SweetAlertQueueResult> pendingTask);
            PendingQueueRequests.Remove(requestGuid);
            pendingTask.SetResult(result);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Function to display a SweetAlert2 modal, with an object of options, all being optional.
        /// </summary>
        /// <example>
        /// <code>
        /// Swal.FireAsync(new SweetAlertOptions {
        ///     Title = "Auto close alert!",
        ///     Text = "I will close in 2 seconds.",
        ///     Timer = 2000
        /// });
        /// </code>
        /// </example>
        /// <param name="settings"></param>
        public async Task<SweetAlertResult> FireAsync(SweetAlertOptions settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var tcs = new TaskCompletionSource<SweetAlertResult>();
            Guid requestId = Guid.NewGuid();
            PendingFireRequests.Add(requestId, tcs);

            AddCallbackToDictionaries(settings, requestId);

            await jSRuntime.InvokeAsync<SweetAlertResult>(
                $"{JS_NAMESPACE}.FireSettings",
                requestId,
                settings.ToPOCO(),
                (int)theme,
                colorSchemeThemes).ConfigureAwait(false);
            return await tcs.Task.ConfigureAwait(false);
        }

        private static void AddCallbackToDictionaries(SweetAlertOptions settings, Guid requestId)
        {
            if (settings.PreConfirm != null)
            {
                PreConfirmCallbacks.Add(requestId, settings.PreConfirm);
            }

            if (settings.InputValidator != null)
            {
                InputValidatorCallbacks.Add(requestId, settings.InputValidator);
            }

            if (settings.OnOpen != null)
            {
                OnOpenCallbacks.Add(requestId, settings.OnOpen);
            }

            if (settings.OnClose != null)
            {
                OnCloseCallbacks.Add(requestId, settings.OnClose);
            }

            if (settings.OnRender != null)
            {
                OnRenderCallbacks.Add(requestId, settings.OnRender);
            }

            if (settings.OnBeforeOpen != null)
            {
                OnBeforeOpenCallbacks.Add(requestId, settings.OnBeforeOpen);
            }

            if (settings.OnAfterClose != null)
            {
                OnAfterCloseCallbacks.Add(requestId, settings.OnAfterClose);
            }

            if (settings.OnDestroy != null)
            {
                OnDestroyCallbacks.Add(requestId, settings.OnDestroy);
            }
        }

        /// <summary>
        /// Reuse configuration by creating a Swal instance.
        /// </summary>
        /// <param name="settings">The default options to set for this instance.</param>
        /// <returns></returns>
        public SweetAlertMixin Mixin(SweetAlertOptions settings)
        {
            return new SweetAlertMixin(settings, this);
        }

        /// <summary>
        /// Determines if a modal is shown.
        /// </summary>
        public async Task<bool> IsVisibleAsync()
        {
            return await jSRuntime.InvokeAsync<bool>($"{JS_NAMESPACE}.IsVisible").ConfigureAwait(false);
        }

        /// <summary>
        /// Closes the currently open SweetAlert2 modal programmatically.
        /// </summary>
        /// <param name="result">The promise originally returned by <code>Swal.FireAsync()</code> will be resolved with this value.
        /// <para>If no object is given, the promise is resolved with an empty ({}) <code>SweetAlertResult</code> object.</para>
        /// </param>
        public async Task CloseAsync(SweetAlertResult result)
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.CloseResult", result).ConfigureAwait(false);
        }

        /// <summary>
        /// Closes the currently open SweetAlert2 modal programmatically.
        /// </summary>
        /// <param name="result">The promise originally returned by <code>Swal.FireAsync()</code> will be resolved with this value.
        /// <para>If no object is given, the promise is resolved with an empty ({}) <code>SweetAlertResult</code> object.</para>
        /// </param>
        public async Task CloseAsync(SweetAlertQueueResult result)
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.CloseResult", result).ConfigureAwait(false);
        }

        /// <summary>
        /// Closes the currently open SweetAlert2 modal programmatically.
        /// </summary>
        public async Task CloseAsync()
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.Close").ConfigureAwait(false);
        }

        /// <summary>
        /// Updates popup options.
        /// </summary>
        /// <param name="newSettings"></param>
        public async Task UpdateAsync(SweetAlertOptions newSettings)
        {
            if (newSettings == null)
            {
                throw new ArgumentNullException(nameof(newSettings));
            }

            Guid requestId = Guid.NewGuid();
            AddCallbackToDictionaries(newSettings, requestId);
            await jSRuntime.InvokeAsync<SweetAlertResult>(
                $"{JS_NAMESPACE}.Update",
                requestId,
                newSettings.ToPOCO()).ConfigureAwait(false);
        }

        /// <summary>
        /// Enables "Confirm" and "Cancel" buttons.
        /// </summary>
        public async Task EnableButtonsAsync()
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.EnableButtons")
               .ConfigureAwait(false);
        }

        /// <summary>
        /// Disables "Confirm" and "Cancel" buttons.
        /// </summary>
        public async Task DisableButtonsAsync()
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.DisableButtons")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Disables buttons and show loader. This is useful with HTML requests.
        /// </summary>
        public async Task ShowLoadingAsync()
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.ShowLoading")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Enables buttons and hide loader.
        /// </summary>
        public async Task HideLoadingAsync()
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.HideLoading")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Determines if modal is in the loading state.
        /// </summary>
        public Task<bool> IsLoadingAsync()
        {
            return jSRuntime.InvokeAsync<bool>($"{JS_NAMESPACE}.IsLoading").AsTask();
        }

        /// <summary>
        /// Clicks the "Confirm"-button programmatically.
        /// </summary>
        public async Task ClickConfirmAsync()
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.ClickConfirm")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Clicks the "Cancel"-button programmatically.
        /// </summary>
        public async Task ClickCancelAsync()
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.ClickCancel")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Shows a validation message.
        /// </summary>
        /// <param name="validationMessage">The validation message.</param>
        public async Task ShowValidationMessageAsync(string validationMessage)
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.ShowValidationMessage", validationMessage)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Hides validation message.
        /// </summary>
        public async Task ResetValidationMessageAsync()
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.ResetValidationMessage")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Disables the modal input. A disabled input element is unusable and un-clickable.
        /// </summary>
        public async Task DisableInputAsync()
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.DisableInput")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Enables the modal input.
        /// </summary>
        public async Task EnableInputAsync()
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.EnableInput")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// If `timer` parameter is set, returns number of milliseconds of timer remained.
        /// <para>Otherwise, returns null.</para>
        /// </summary>
        public async Task<double?> GetTimerLeftAsync()
        {
            var response = await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.GetTimerLeft")
                .ConfigureAwait(false);
            return response == null ? null : (double?)Convert.ToDouble(response.ToString(), culture.NumberFormat);
        }

        /// <summary>
        /// Stop timer. Returns number of milliseconds of timer remained.
        /// <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public async Task<double?> StopTimerAsync()
        {
            var response = await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.StopTimer")
                .ConfigureAwait(false);
            return response == null ? null : (double?)Convert.ToDouble(response.ToString(), culture.NumberFormat);
        }

        /// <summary>
        /// Resume timer. Returns number of milliseconds of timer remained.
        /// <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public async Task<double?> ResumeTimerAsync()
        {
            var response = await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.ResumeTimer")
                .ConfigureAwait(false);
            return response == null ? null : (double?)Convert.ToDouble(response.ToString(), culture.NumberFormat);
        }

        /// <summary>
        /// Toggle timer. Returns number of milliseconds of timer remained.
        /// <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public async Task<double?> ToggleTimerAsync()
        {
            var response = await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.ToggleTimer")
                .ConfigureAwait(false);
            return response == null ? null : (double?)Convert.ToDouble(response.ToString(), new NumberFormatInfo());
        }

        /// <summary>
        /// Check if timer is running. Returns true if timer is running, and false is timer is paused / stopped.
        /// <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        public async Task<bool?> IsTimerRunningAsync()
        {
            var response = await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.IsTimerRunning")
                .ConfigureAwait(false);
            return response == null ? null : (bool?)Convert.ToBoolean(response.ToString(), culture.NumberFormat);
        }

        /// <summary>
        /// Increase timer. Returns number of milliseconds of an updated timer.
        /// <para>If `timer` parameter isn't set, returns null.</para>
        /// </summary>
        /// <param name="n">The number of milliseconds to add to the currect timer</param>
        public async Task<double?> IncreaseTimerAsync(double n)
        {
            var response = await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.IncreaseTimer", n)
                .ConfigureAwait(false);
            return response == null ? null : (double?)Convert.ToDouble(response.ToString(), culture.NumberFormat);
        }

        /// <summary>
        /// Provide an array of SweetAlert2 parameters to show multiple modals, one modal after another.
        /// </summary>
        /// <param name="steps">The steps' configuration.</param>
        public async Task<SweetAlertQueueResult> QueueAsync(IEnumerable<SweetAlertOptions> steps)
        {
            var requestId = Guid.NewGuid();
            var tcs = new TaskCompletionSource<SweetAlertQueueResult>();
            PendingQueueRequests.Add(requestId, tcs);
            var tuples = steps.Select(s => (RequestId: Guid.NewGuid(), Step: s)).ToList();
            foreach (var (RequestId, Step) in tuples)
            {
                AddCallbackToDictionaries(Step, RequestId);
            }

            await jSRuntime.InvokeAsync<object>(
                $"{JS_NAMESPACE}.Queue",
                requestId,
                tuples.Select(t => t.RequestId).ToArray(),
                tuples.Select(t => t.Step.ToPOCO()).ToArray(),
                (int)theme,
                colorSchemeThemes)
                .ConfigureAwait(false);
            return await tcs.Task.ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the index of current modal in queue. When there's no active queue, null will be returned.
        /// </summary>
        public Task<string> GetQueueStepAsync()
        {
            return jSRuntime.InvokeAsync<string>($"{JS_NAMESPACE}.GetQueueStep").AsTask();
        }

        /// <summary>
        /// Inserts a modal in the queue.
        /// </summary>
        /// <param name="step">The step configuration (same object as in the Swal.fire() call).</param>
        /// <param name="index">The index to insert the step at. By default a modal will be added to the end of a queue.</param>
        public Task<double> InsertQueueStepAsync(SweetAlertOptions step, double? index = null)
        {
            if (step == null)
            {
                throw new ArgumentNullException(nameof(step));
            }

            var requestId = Guid.NewGuid();
            AddCallbackToDictionaries(step, requestId);
            return jSRuntime.InvokeAsync<double>($"{JS_NAMESPACE}.InsertQueueStep", requestId, step.ToPOCO(), index).AsTask();
        }

        /// <summary>
        /// Deletes the modal at the specified index in the queue.
        /// </summary>
        /// <param name="index">The modal index in the queue.</param>
        public async Task DeleteQueueStepAsync(double index)
        {
            await jSRuntime.InvokeAsync<object>($"{JS_NAMESPACE}.DeleteQueueStep", index)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Determines if a given parameter name is valid.
        /// </summary>
        /// <param name="paramName">The parameter to check.</param>
        /// <returns></returns>
        public Task<bool> IsValidParameterAsync(string paramName)
        {
            return jSRuntime.InvokeAsync<bool>($"{JS_NAMESPACE}.IsValidParameter", paramName).AsTask();
        }

        /// <summary>
        /// Determines if a given parameter name is valid for Swal.update() method.
        /// </summary>
        /// <param name="paramName">The parameter to check.</param>
        /// <returns></returns>
        public Task<bool> IsUpdatableParameterAsync(string paramName)
        {
            return jSRuntime.InvokeAsync<bool>($"{JS_NAMESPACE}.IsUpdatableParameter", paramName).AsTask();
        }

        /// <summary>
        /// Normalizes the arguments you can give to Swal.fire() in an object of type SweetAlertOptions.
        /// </summary>
        /// <param name="paramaters">The array of arguments to normalize.</param>
        /// <exception cref="ArgumentException">Thrown if parameters is not 1, 2, or 3 elements long.</exception>
        public SweetAlertOptions ArgsToParams(IEnumerable<string> paramaters)
        {
            if (paramaters == null)
            {
                throw new ArgumentNullException(nameof(paramaters));
            }
            int paramLength = paramaters.Count();
            if (paramLength > 3 || paramLength < 1)
            {
                throw new ArgumentException("Parameters can only be 1, 2, or 3 elements long.");
            }
            var paramEnum = paramaters.GetEnumerator();
            paramEnum.MoveNext();
            var optionsToReturn = new SweetAlertOptions
            {
                Title = paramEnum.Current
            };

            if (paramEnum.MoveNext())
            {
                optionsToReturn.Html = paramEnum.Current;
            }
            if (paramEnum.MoveNext())
            {
                optionsToReturn.Icon = paramEnum.Current;
            }

            return optionsToReturn;
        }

        [JSInvokable]
        public static Task<string> ReceivePreConfirmInput(string requestId, string inputValue)
        {
            var requestIdGuid = Guid.Parse(requestId);
            PreConfirmCallbacks.TryGetValue(requestIdGuid, out PreConfirmCallback callback);
            PreConfirmCallbacks.Remove(requestIdGuid);
            return callback.InvokeAsync(inputValue);
        }

        [JSInvokable]
        public static Task<IEnumerable<string>> ReceivePreConfirmQueueInput(string requestId, IEnumerable<string> inputValue)
        {
            var requestIdGuid = Guid.Parse(requestId);
            PreConfirmCallbacks.TryGetValue(requestIdGuid, out PreConfirmCallback callback);
            return callback.InvokeAsync(inputValue);
        }

        [JSInvokable]
        public static Task<string> ReceiveInputValidatorInput(string requestId, string inputValue)
        {
            var requestIdGuid = Guid.Parse(requestId);
            InputValidatorCallbacks.TryGetValue(requestIdGuid, out InputValidatorCallback callback);
            return callback.InvokeAsync(inputValue);
        }

        [JSInvokable]
        public static async Task ReceiveOnOpenInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            OnOpenCallbacks.TryGetValue(requestIdGuid, out SweetAlertCallback callback);
            OnOpenCallbacks.Remove(requestIdGuid);
            await callback.InvokeAsync().ConfigureAwait(false);
        }

        [JSInvokable]
        public static async Task ReceiveOnCloseInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            OnCloseCallbacks.TryGetValue(requestIdGuid, out SweetAlertCallback callback);
            OnCloseCallbacks.Remove(requestIdGuid);
            await callback.InvokeAsync().ConfigureAwait(false);
        }

        [JSInvokable]
        public static async Task ReceiveOnRenderInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            OnRenderCallbacks.TryGetValue(requestIdGuid, out SweetAlertCallback callback);
            await callback.InvokeAsync().ConfigureAwait(false);
        }

        [JSInvokable]
        public static async Task ReceiveOnBeforeOpenInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            OnBeforeOpenCallbacks.TryGetValue(requestIdGuid, out SweetAlertCallback callback);
            OnBeforeOpenCallbacks.Remove(requestIdGuid);
            await callback.InvokeAsync().ConfigureAwait(false);
        }

        [JSInvokable]
        public static async Task ReceiveOnAfterCloseInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            OnAfterCloseCallbacks.TryGetValue(requestIdGuid, out SweetAlertCallback callback);
            OnAfterCloseCallbacks.Remove(requestIdGuid);
            await callback.InvokeAsync().ConfigureAwait(false);
        }

        [JSInvokable]
        public static async Task ReceiveOnDestroyInput(string requestId)
        {
            var requestIdGuid = Guid.Parse(requestId);
            OnDestroyCallbacks.TryGetValue(requestIdGuid, out SweetAlertCallback callback);
            OnDestroyCallbacks.Remove(requestIdGuid);
            await callback.InvokeAsync().ConfigureAwait(false);
        }
    }

    /// <summary>
    /// An enum of possible reasons that can explain an alert dismissal.
    /// </summary>
    public enum DismissReason
    {
        Cancel,
        Backdrop,
        Close,
        Esc,
        Timer
    }
}
