using System;
using System.Net;
using System.Threading.Tasks;

namespace Orion.Framework.Web.Clients {
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpRequest : IRequest<IHttpRequest> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        IHttpRequest OnSuccess( Action<string> action );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        IHttpRequest OnSuccess( Action<string, HttpStatusCode> action );
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        Task<TResult> ResultFromJsonAsync<TResult>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IHttpRequest<TResult> : IRequest<IHttpRequest<TResult>> where TResult : class {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action">,</param>
        /// <param name="convertAction">，</param>
        IHttpRequest<TResult> OnSuccess( Action<TResult> action, Func<string, TResult> convertAction = null );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action">,，</param>
        /// <param name="convertAction">，</param>
        IHttpRequest<TResult> OnSuccess( Action<TResult, HttpStatusCode> action, Func<string, TResult> convertAction = null );
        /// <summary>
        /// 
        /// </summary>
        Task<TResult> ResultFromJsonAsync();
    }
}
