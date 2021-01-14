using System;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.JsInterop
{
    public class PromiseCallbackHandler<TResult> : IPromiseCallbackHandler
    {
        private readonly TaskCompletionSource<TResult> _tcs;

        public PromiseCallbackHandler(TaskCompletionSource<TResult> tcs)
        {
            _tcs = tcs;
        }

        public void SetResult(string json)
        {
            TResult result = Json.Json.ToObject<TResult>(json);
            _tcs.SetResult(result);
        }

        public void SetError(string error)
        {
            var exception = new Exception(error);
            _tcs.SetException(exception);
        }
    }
}
