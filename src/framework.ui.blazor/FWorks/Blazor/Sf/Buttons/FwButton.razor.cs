
using Microsoft.AspNetCore.Components;
using Orion.Framework.Helpers;
using Orion.Framework.Ui.Blazor.Components;
using Orion.Framework.Ui.Blazor.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.FWorks.Blazor.Sf.Buttons
{
     public class FwBaseButton : FWorkBlazorComponent
    {

        string varMaterialDesignIcon;
        string varIcon = "mdi-send";

        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public FWorkStyleButton Style { get; set; } = FWorkStyleButton.None;

        [Parameter] public string Tooltip { get; set; } = "";
        [Parameter] public bool IsSubmit { get; set; } = false;
        [Parameter] public bool IsDisable { get; set; } = false;
        [Parameter] public bool IsPrimary { get; set; }
        [Parameter] public bool IsToogle { get; set; } = false;
        [Parameter] public bool UseMaterialIcon { get; set; } = true;
        [Parameter] public string Icon { get; set; }

        protected override Task OnParametersSetAsync()
        {
            base.OnParametersSetAsync();
            HtmlAttributes = new Dictionary<string, object>();
            if (UseMaterialIcon)
            {
                if (Icon.IsEmpty()) Icon = varIcon;
                varMaterialDesignIcon = $"mdi {Icon} mdi-18px";
                CssIcon = varMaterialDesignIcon;
            }
            if (IsSubmit)
            {
                HtmlAttributes.Add("type", "submit");

            }
            if (!Tooltip.IsEmpty())
            {
                HtmlAttributes.Add("title", Tooltip);
            }
            if (this.Style != FWorkStyleButton.None)
            {
                var _style = HelperEnum.GetDescription(typeof(FWorkStyleButton), Style);
                CssClass = _style;
            }
            return Task.CompletedTask;
        }
    }
}
