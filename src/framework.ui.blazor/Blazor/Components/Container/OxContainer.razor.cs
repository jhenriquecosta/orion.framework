using Microsoft.AspNetCore.Components;
 
namespace Orion.Framework.Ui.Blazor.Components
{
    public abstract class OxContainerBase : OxServiceBase
    {
        [Inject] public ILoadingService LoadingService { get; set; }
    }
}

 
