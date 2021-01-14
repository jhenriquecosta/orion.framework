using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Framework.Web.Blazor.Components.Helpers;

namespace Framework
{

    public static partial class JSRuntimeExtensions
    {
        private const string XT_NAMESPACE = "xtapp";
        private static readonly ConcurrentDictionary<string, IPromiseCallbackHandler> CallbackHandlers =
          new ConcurrentDictionary<string, IPromiseCallbackHandler>();

        [JSInvokable]
        public static void PromiseCallback(string callbackId, string result)
        {
            if (CallbackHandlers.TryGetValue(callbackId, out IPromiseCallbackHandler handler))
            {
                handler.SetResult(result);
                CallbackHandlers.TryRemove(callbackId, out IPromiseCallbackHandler _);
            }
        }

        [JSInvokable]
        public static void PromiseError(string callbackId, string error)
        {
            if (CallbackHandlers.TryGetValue(callbackId, out IPromiseCallbackHandler handler))
            {
                handler.SetError(error);
                CallbackHandlers.TryRemove(callbackId, out IPromiseCallbackHandler _);
            }
        }
       
        public static async Task<TResult> ExecuteAsync<TResult>(this IJSRuntime jsRuntime, string fnName, object data = null)
        {
            var tcs = new TaskCompletionSource<TResult>();

            string callbackId = Guid.NewGuid().ToString();
            if (CallbackHandlers.TryAdd(callbackId, new PromiseCallbackHandler<TResult>(tcs)))
            {
                if (data == null)
                {
                    await jsRuntime.InvokeAsync<bool>(GetName("runFunction"), callbackId,GetName(fnName));
                }
                else
                {
                   await jsRuntime.InvokeAsync<bool>(GetName("runFunction"), callbackId, GetName(fnName), data);
                }

                return await tcs.Task;
            }

            throw new Exception("An entry with the same callback id already existed, really should never happen");

        }
        private static string GetName(string function)
        {
            return $"{XT_NAMESPACE}.{function}";
        }
    }
}
