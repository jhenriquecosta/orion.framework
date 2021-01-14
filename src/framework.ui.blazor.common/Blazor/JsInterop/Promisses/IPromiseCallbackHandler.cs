namespace Orion.Framework.Ui.Blazor.JsInterop
{
    public interface IPromiseCallbackHandler
    {
        void SetResult(string json);
        void SetError(string error);
    }
}