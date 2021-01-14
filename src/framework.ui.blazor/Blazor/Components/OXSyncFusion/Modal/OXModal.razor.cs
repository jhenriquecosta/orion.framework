
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

namespace Orion.Framework.Ui.Blazor.Components.OXSyncFusion
{
    public class OXModalBase : OXDialogBase
    {
       
        protected bool isVisible = true;
        protected SfDialog ejsWinModal;
        protected DialogAnimationSettings dialogAnnimation { get; set; } = new DialogAnimationSettings { Effect = DialogEffect.Zoom };
        [Parameter] public RenderFragment Body { get; set; }
        [Parameter] public RenderFragment Footer { get; set; }
        [Parameter] public string Header { get; set; } = "";
        [Parameter] public string Target { get; set; } = "#target";
        [Parameter] public bool IsVisible { get; set; } = false;
        [Parameter] public bool ShowBtnOkCancel { get; set; } = false;
        [Parameter] public bool ShowBtnOk { get; set; } = false;
        [Parameter] public bool ShowBtnCancel { get; set; } = false;

        [Parameter] public EventCallback OnBtnOkClick { get; set; }
        [Parameter] public EventCallback OnBtnCancelClick { get; set; }


        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                this.JsRuntime.Sf().LoadLocaleData("wwwroot/locale/pt.json")
                 .LoadCldrData("wwwroot/locale/cldr-data/main/pt/numbers.json",
                 "wwwroot/locale/cldr-data/main/pt/timeZoneNames.json",
                 "wwwroot/locale/cldr-data/main/pt/ca-gregorian.json",
                 "wwwroot/locale/cldr-data/main/pt/currencies.json",
                 "wwwroot/locale/cldr-data/supplemental/numberingSystems.json")
                 .SetCulture("pt").SetCurrencyCode("BRL");
            }

        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (Width.IsNullOrEmpty()) Width = "600px";
        }
        public void Show()
        {
            this.IsVisible = true;
            this.ejsWinModal.Show();
            this.OnChanged();

        }
        public void Hide()
        {
            this.IsVisible = false;
            this.ejsWinModal.Hide();
            this.OnChanged();
        }

    }

}
