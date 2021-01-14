using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public abstract class OxInputBase<TValue> : OxSfInputBase<TValue>
    {
        [Parameter] public string Format { get; set; } = string.Empty;
        [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
        [Parameter] public EventCallback<Microsoft.AspNetCore.Components.Web.FocusEventArgs> OnBlurFired { get; set; }
        protected virtual async Task OnValueChanged(Microsoft.AspNetCore.Components.ChangeEventArgs e)
        {
            this.Value = e.Value.To<TValue>();
            await ValueChanged.InvokeAsync(this.Value);
        }
        protected virtual async Task OnValueChanged(InputEventArgs e)
        {
            this.Value = e.Value.To<TValue>();
            await ValueChanged.InvokeAsync(this.Value);
        }
        protected virtual async Task OnValueChanged(ChangedEventArgs<TValue> e)
        {
            this.Value = e.Value.To<TValue>();
            await ValueChanged.InvokeAsync(this.Value);
        }
    }
}
