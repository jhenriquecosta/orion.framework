using Syncfusion.Blazor.Inputs;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.FWorks.Blazor.Sf.Inputs
{
    public class FwBaseText : FwBaseInput<string>
    {
        protected SfTextBox objEditText { get; set; }
        
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (this.Value == null) this.Value = string.Empty;
        }

    }
}
