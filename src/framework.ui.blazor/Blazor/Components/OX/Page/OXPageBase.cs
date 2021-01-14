using Microsoft.AspNetCore.Components;
using Orion.Framework.Exceptions;
using System;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{


    public abstract class OXPageBase : FWorkFormBase
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
