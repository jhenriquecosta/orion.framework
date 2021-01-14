
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Orion.Framework.Ui.Blazor.Enums;
using Syncfusion.EJ2.Blazor.Inputs;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public class OxSubmitBase : OxDialogBase
    {
        [Parameter] public RenderFragment Buttons { get; set; }
        [Parameter] public OxActionModel ActionModel { get; set; }

        [Parameter] public System.Action FormSubmit { get; set; }
        [Parameter] public System.Action OnResetClick { get; set; }
        [Parameter] public EventCallback OnSaveClick { get; set; }
        [Parameter] public EventCallback OnDeleteClick { get; set; }
        [Parameter] public string CloseButtonText { get; set; } = "CANCELAR";
        [Parameter] public string SaveButtonText { get; set; } = "CONFIRMAR";
        [Parameter] public string DeleteButtonText { get; set; } = "DELETAR";

        protected void OnFormSubmit(System.EventArgs e) => FormSubmit?.Invoke();
        protected void OnSaveButtonClick(MouseEventArgs e) => OnSaveClick.InvokeAsync(e);
        protected void OnResetButtonClick()
        {

            OnResetClick();

        }

    }
}
