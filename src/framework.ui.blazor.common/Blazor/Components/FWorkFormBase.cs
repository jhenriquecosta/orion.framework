using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Orion.Framework.Ui.Blazor.Builders;
using System.Collections.Generic;
using System.Net.Http;

namespace Orion.Framework.Ui.Blazor.Components
{
    public abstract class FWorkFormBase : OwningComponentBase
    {
        [Inject] public IToastService Toast { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public HttpClient httpClient { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public virtual void OnChanged()
        {
            InvokeAsync(StateHasChanged);
        }

        ~FWorkFormBase()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
