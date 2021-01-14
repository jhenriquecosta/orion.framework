
using Microsoft.AspNetCore.Components;
using Syncfusion.EJ2.Blazor.Inputs;
using Syncfusion.EJ2.Blazor.Popups;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public class OxModalBase : OxDialogBase
    {
       
        protected bool isVisible = true;
        protected EjsDialog ejsWinModal;
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
