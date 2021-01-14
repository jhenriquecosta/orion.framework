using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Orion.Framework.Ui.Blazor.Builders;
using Orion.Framework.Ui.Blazor.Enums;
using Syncfusion.EJ2.Blazor.Inputs;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public abstract class OxSfInputBase<TValue> : OxComponentBase
    {
        [Parameter] public FloatLabelType FloatLabel { get; set; } = FloatLabelType.Auto;
        [Parameter] public bool ShowClearButton { get; set; } = true;
        [Parameter] public string Locale { get; set; } = "pt-BR";
        [Parameter] public TValue Value { get; set; }
       
    }
}
