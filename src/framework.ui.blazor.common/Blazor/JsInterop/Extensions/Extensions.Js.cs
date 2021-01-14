using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Framework.Web.Blazor;
using Framework.Web.Blazor.Components.Enums;

namespace Framework
{
    public static class JSInteropExtensions
    {
      
        private const string XT_NAMESPACE = "xtapp";
        private static string GetName(string function)
        {
            return $"{XT_NAMESPACE}.{function}";
        }
        public static async Task<bool> Confirm(this IJSRuntime runtime, string question)
        {
            var valueResult = await runtime.ExecuteAsync<string>("SweetConfirm", question);
            var valueReturn = valueResult.ToBool();
            return valueReturn;
        }
        public static async Task SetFocus(this IJSRuntime runtime,string element)
        {
           // await runtime.InvokeAsync<object>($"{XT_NAMESPACE}.SetFocus", element);
            await runtime.InvokeAsync<Action>(GetName("SetFocus"));
        }
        public static async Task Dialog(this IJSRuntime runtime, string question,XTTypeDialog dialog=XTTypeDialog.Info)
        {   
            var tipo = "info";
            if (dialog == XTTypeDialog.Sucesso) tipo = "success";
            if (dialog == XTTypeDialog.Info) tipo = "info";
            if (dialog == XTTypeDialog.Erro) tipo = "warning";
            if (dialog == XTTypeDialog.Warning) tipo = "error";
            await runtime.InvokeAsync<Action>(GetName("SweetDialog"), question,tipo);
        }
    }
}
