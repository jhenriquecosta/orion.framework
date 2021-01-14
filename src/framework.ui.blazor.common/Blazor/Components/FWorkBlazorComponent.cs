using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Orion.Framework.Ui.Blazor.Builders;
using System.Collections.Generic;

namespace Orion.Framework.Ui.Blazor.Components
{
    public abstract class FWorkBlazorComponent : OwningComponentBase
    {
        #region Members
        private string elementId;
        #endregion
        protected ElementReference ElementRef { get; set; }
        protected string ElementId
        {
            get
            {
                // generate ID only on first use
                if (elementId == null)
                    elementId = IDGenerator.Instance.Generate;
                return elementId;
            }
        }
         
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public Dictionary<string, object> HtmlAttributes { get; set; }
        [Parameter] public string Caption { get; set; } = string.Empty;
        [Parameter] public bool AllowEdit { get; set; } = true;
        [Parameter] public bool Disable { get; set; } = false;

        [Parameter] public string Message { get; set; } = string.Empty;
        [Parameter] public string CssClass { get; set; } = "e-outline";
        [Parameter] public string CssIcon { get; set; }
        [Parameter] public string Width { get; set; } = string.Empty;

        [Inject] public IToastService Toast { get; set; }
        

        public virtual void OnChanged()
        {
            InvokeAsync(StateHasChanged);
        }

        ~FWorkBlazorComponent()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
        }

    }
}
