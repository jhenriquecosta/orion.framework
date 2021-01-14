
using Microsoft.AspNetCore.Components;

namespace Orion.Framework.Ui.Blazor.Components.OXBlazorise
{
    public class OXPanelBase : OxBlazoriseDialogBase
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string SubTitle { get; set; }
        [Parameter] public string UrlImage { get; set; } = "";
        [Parameter] public RenderFragment Top { get; set; }
        [Parameter] public RenderFragment Bottom { get; set; }
    }
}

 