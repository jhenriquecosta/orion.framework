using Microsoft.AspNetCore.Components;
using Orion.Framework.Exceptions;
using Orion.Framework.Ui.Blazor.Components;
using System;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.FWorks.Blazor.Sf.Forms
{


    public abstract class PageFormBase : FWorkFormBase
    {
       
        [Inject] private IExceptionHelper ExceptionHelper { get; set; }
      
        public string ErrorMessage { get; set; }
        public bool LoadFailed { get; set; }
        public bool IsVisible { get; set; } = false;

      
        protected void HandleException(Exception ex)
        {
            if (ex is UnauthorizedAccessException)
            {
                NavigationManager.NavigateTo("/unauthorized");
            }
            else
            {
                ExceptionHelper.StoreException(ex);
                NavigationManager.NavigateTo("/error");
            }
        }

        
    }
}
