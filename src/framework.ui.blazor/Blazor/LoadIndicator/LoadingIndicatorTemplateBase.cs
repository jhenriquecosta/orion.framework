
using Microsoft.AspNetCore.Components;
using Orion.Framework.Web.Blazor.Components;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public abstract class LoadingIndicatorTemplateBase : ComponentBase
    {
        [Parameter]
        public ITaskStatus CurrentTask { protected get; set; }

        public Task CallStateHasChanged() => InvokeAsync(StateHasChanged);
    }
}
