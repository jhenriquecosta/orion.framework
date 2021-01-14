using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Ui.Blazor.Components.OXSyncFusion
{
    public abstract class OXSFBase : FWorkBlazorComponent
    {
        [Parameter] public FloatLabelType FloatLabel { get; set; } = FloatLabelType.Auto;
        [Parameter] public bool ShowClearButton { get; set; } = true;
        [Parameter] public string Locale { get; set; } = "pt-BR";
        
    }
    public abstract class OXSFBase<TValue> : OXSFBase
    {
        [Parameter] public TValue Value { get; set; }

    }
}
