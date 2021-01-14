using Microsoft.AspNetCore.Components;

namespace Orion.Framework.Ui.Blazor.Components
{
    public abstract class OXDialogBase : OxBlazoriseDialogBase
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string SubTitle { get; set; }
        [Parameter] public string UrlImage { get; set; } = "";
    }
}
