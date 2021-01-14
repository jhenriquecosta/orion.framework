using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components.OXSyncFusion
{
    public abstract class FwBaseInput<TValue> :     OXSFBase<TValue>
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
        
    }
}
