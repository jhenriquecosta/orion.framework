using Blazorise;
using Microsoft.AspNetCore.Components;

namespace Orion.Framework.Ui.Blazor.Components.OXBlazorise
{
    public abstract class OXBlazoriseLayoutBase : FWorkBlazorComponent
    {
        [Parameter] public IFluentColumn SizeColumn { get; set; } = ColumnSize.IsFull.OnDesktop.IsFull.OnMobile;
        [Parameter] public IFluentSpacing SizeMargin { get; set; } = Margin.Is2.OnY;
    }
}
