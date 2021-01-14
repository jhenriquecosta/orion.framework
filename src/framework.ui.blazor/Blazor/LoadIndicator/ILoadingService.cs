using Orion.Framework.Web.Blazor.Components;
using System;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Components
{
    public interface ILoadingService
    {
        //Type DefaultTemplateType { get; set; }
        Task StartTaskAsync(Func<ITaskStatus, Task> action, string context = "", string maintext = null, string subtext = null);
        void SubscribeToTaskProgressChanged(string context, Func<ITaskStatus, Task> action);
        void UnsubscribeFromTaskProgressChanged(string context, Func<ITaskStatus, Task> action);
        IndicatorOptions Options { get; }
    }
}