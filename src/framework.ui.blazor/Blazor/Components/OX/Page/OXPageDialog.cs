using Microsoft.AspNetCore.Components;
using Orion.Framework.Ui.Blazor.Components;
using Orion.Framework.Utilities;
using System;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{

    public abstract class OXPageDialog : OXPageBase
    {
        [Inject] public SweetAlertService Swal { get; set; }
        [Inject] public ILoadingService LoadingTaskService { get; set; }


        protected async Task<Result<T>> GetResultAsync<T>(Result<T> result)
        {
            if (result.IsError)
            {
                await WarningAsync(result.Message);
                result.IsOk = false;
                return result;
            }
            return result;
        }
        public async Task InfoAsync(string mensagem)
        {
            var alert = new SweetAlertOptions
            {
                Title = "Informação",
                Text = mensagem,
                Icon = SweetAlertIcon.Success,
                ShowConfirmButton = false,
                Timer = 1500
            };
            await Swal.FireAsync(alert);
        }
        public void ShowToast(string mensagem,string heading)
        {
            Toast.ShowInfo(mensagem, heading);
        }
        public async Task ErrorAsync(string mensagem)
        {
            await Swal.FireAsync("Atenção", mensagem, "error");
        }
        public async Task WarningAsync(string mensagem)
        {
            await Swal.FireAsync("Atenção", mensagem, "warning");
        }
        public void ExecuteAsync(Func<Task> task)
        {
            AsyncUtil.RunSync(async () => await task.Invoke());
        }
        public async Task<bool> QuestionAsync(string mensagem)
        {
            var objResult = false;
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = mensagem,
                Text = "Verifique os dados corretamente!",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = "Confirmar",
                CancelButtonText = "Cancelar"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                objResult = true;
            }
            else if (result.Dismiss == DismissReason.Cancel)
            {
                await Swal.FireAsync(
                  "Cancelada",
                  "Operação CANCELADA pelo USUÁRIO!",
                  SweetAlertIcon.Error
                  );
            }
            return objResult;

        }
    } 
 
}
