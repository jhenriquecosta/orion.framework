using Blazorise;
using Microsoft.AspNetCore.Components;
using Orion.Framework.Ui.Blazor.Components;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Ui.FWorks.Blazor.Sf
{
    public abstract class FwBaseComponent : FWorkBlazorComponent
    {
        [Parameter] public IFluentColumn Column { get; set; } = ColumnSize.IsFull.OnDesktop.IsFull.OnMobile;
        [Parameter] public IFluentSpacing Margin { get; set; } = Blazorise.Margin.Is2.OnY;
        [Parameter] public FloatLabelType FloatLabel { get; set; } = FloatLabelType.Always;
        [Parameter] public bool ShowClearButton { get; set; } = true;
        [Parameter] public string Locale { get; set; } = "pt-BR";
        
    }
    public abstract class FwBaseComponent<TValue> : FwBaseComponent
    {
        [Parameter] public TValue Value { get; set; }

    }
}
