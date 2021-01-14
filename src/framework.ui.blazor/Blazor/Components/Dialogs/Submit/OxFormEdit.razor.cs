
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Orion.Framework.Ui.Blazor.Enums;
using Syncfusion.EJ2.Blazor.Inputs;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public class OxFormEditBase : OxSubmitBase
    {
        [Parameter] public bool IsModal { get; set; } = true;

    }
}
