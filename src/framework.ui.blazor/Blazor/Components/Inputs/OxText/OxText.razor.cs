
using Microsoft.AspNetCore.Components;
using Syncfusion.EJ2.Blazor.Inputs;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public class OxTextBase : OxInputBase<string>
    {
        protected EjsTextBox objEditText { get; set; }
        
        protected override async Task OnParametersSetAsync()
        {
            if (this.Value == null) this.Value = string.Empty;
            await base.OnParametersSetAsync();
        }

    }
}
