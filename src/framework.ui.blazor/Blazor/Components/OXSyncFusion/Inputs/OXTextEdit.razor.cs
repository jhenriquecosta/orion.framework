using Syncfusion.Blazor.Inputs;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components.OXSyncFusion
{
    public class OXTextEditBase : FwBaseInput<string>
    {
        protected SfTextBox objEditText { get; set; }
        
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (this.Value == null) this.Value = string.Empty;
        }

    }
}
