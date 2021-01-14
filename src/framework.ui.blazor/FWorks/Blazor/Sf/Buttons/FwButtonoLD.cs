
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Orion.Framework.Helpers;
using Orion.Framework.Ui.Blazor.Components;
using Orion.Framework.Ui.Blazor.Enums;
using Syncfusion.Blazor.Buttons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.FWorks.Blazor.Sf.Buttons
{
    public class FwButton : FWorkBlazorComponent
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

        int seq = 0;
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            builder.OpenComponent<SfButton>(seq++);
            builder.AddAttribute(seq++, "Content", this.Caption);
            builder.AddAttribute(seq++, "IconCss", this.CssIcon);
            builder.AddAttribute(seq++, "CssClass", this.CssClass);
            builder.AddAttribute(seq++, "IsPrimary", this.IsPrimary);
            builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClick));
            builder.CloseComponent();
        }
    }

}
