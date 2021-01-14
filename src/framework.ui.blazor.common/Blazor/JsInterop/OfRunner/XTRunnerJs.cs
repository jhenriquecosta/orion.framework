using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Web;
using Framework.Web.Helpers;

namespace Framework.Web.Blazor.Components.Helpers
{

    public interface IXTRunnerJs
    {
      
        IJSRuntime JsCurrent { get; }
        Task AlertJs(string message);
        Task AlertJs(IJSRuntime run,string message);
    }
    public  class XTRunnerJs : IXTRunnerJs
    {
        protected readonly IJSRuntime runtime;

        protected const string XT_NAMESPACE = "xtapp";

        private static object CreateDotNetObjectRefSyncObj = new object();

        public XTRunnerJs(IJSRuntime _runtime)
        {
            this.runtime = _runtime;
        }

        public DotNetObjectReference<T> CreateDotNetObjectRef<T>(T value) where T : class
        {
            lock (CreateDotNetObjectRefSyncObj)
            {
                return DotNetObjectReference.Create(value);
            }
        }
        public void DisposeDotNetObjectRef<T>(DotNetObjectReference<T> value) where T : class
        {
            if (value != null)
            {
                lock (CreateDotNetObjectRefSyncObj)
                {
                    value.Dispose();
                }
            }
        }

        public IJSRuntime JsCurrent => runtime;
        public async  Task AlertJs(string message)
        {            
            await runtime.InvokeAsync<object>($"{XT_NAMESPACE}.AlertJs", message);
        }
        public async Task AlertJs(IJSRuntime runtime, string message)
        {
            await runtime.InvokeAsync<object>($"{XT_NAMESPACE}.AlertJs", message);
        }
        public async Task SetFocus(string element)
        {
            await runtime.InvokeAsync<object>($"{XT_NAMESPACE}.SetFocus", element);
        }
    }
}
