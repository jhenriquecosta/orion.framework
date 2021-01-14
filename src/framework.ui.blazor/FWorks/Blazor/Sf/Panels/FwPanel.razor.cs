
using Blazorise;
using Microsoft.AspNetCore.Components;
using Orion.Framework.Ui.Blazor.Components;

namespace Orion.Framework.Ui.FWorks.Blazor.Panels
{
    public class FwBasePanel : FWorkBlazorComponent
    {
        [Parameter] public IFluentColumn SizeColumn { get; set; } = ColumnSize.IsFull.OnDesktop.IsFull.OnMobile;
        [Parameter] public IFluentSpacing SizeMargin { get; set; } = Margin.Is1.FromTop;
        [Parameter] public string Title { get; set; }
        [Parameter] public string SubTitle { get; set; }
        [Parameter] public string UrlImage { get; set; } = "";
        [Parameter] public RenderFragment Top { get; set; }
        [Parameter] public RenderFragment Bottom { get; set; }
    }
}

 