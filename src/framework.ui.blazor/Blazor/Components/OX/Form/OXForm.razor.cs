
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Orion.Framework.Ui.Blazor.Enums;

namespace Orion.Framework.Ui.Blazor.Components
{
    public class OXFormBase : OXDialogBase
    {

        [Parameter] public System.Action FormSubmit { get; set; }
     
        [Parameter] public RenderFragment Buttons { get; set; }
        [Parameter] public FWorkActionModel ActionModel { get; set; }

        [Parameter] public System.Action OnResetClick { get; set; }
        [Parameter] public EventCallback OnSaveClick { get; set; }
        [Parameter] public EventCallback OnDeleteClick { get; set; }
        [Parameter] public string CloseButtonText { get; set; } = "CANCELAR";
        [Parameter] public string SaveButtonText { get; set; } = "CONFIRMAR";
        [Parameter] public string DeleteButtonText { get; set; } = "DELETAR";

        protected void OnSaveButtonClick(MouseEventArgs e) => OnSaveClick.InvokeAsync(e);
        protected void OnFormSubmit(System.EventArgs e) => FormSubmit?.Invoke();
        protected void OnResetButtonClick()
        {

            OnResetClick();

        }
      
       
    }
}
