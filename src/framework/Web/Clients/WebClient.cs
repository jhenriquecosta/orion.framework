using System.Net.Http;

namespace Orion.Framework.Web.Clients {
    /// <summary>
   
    /// </summary>
    public class WebClient {
        /// <summary>
    
        /// </summary>
        /// <param name="url"></param>
        public IHttpRequest Get( string url ) {
            return new HttpRequest( HttpMethod.Get, url );
        }

        /// <summary>
    
        /// </summary>
        /// <param name="url"></param>
        public IHttpRequest Post( string url ) {
            return new HttpRequest( HttpMethod.Post, url );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public IHttpRequest Put( string url ) {
            return new HttpRequest( HttpMethod.Put, url );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public IHttpRequest Delete( string url ) {
            return new HttpRequest( HttpMethod.Delete, url );
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class WebClient<TResult>  where TResult : class  {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public IHttpRequest<TResult> Get( string url ) {
            return new HttpRequest<TResult>( HttpMethod.Get, url );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public IHttpRequest<TResult> Post( string url ) {
            return new HttpRequest<TResult>( HttpMethod.Post, url );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public IHttpRequest<TResult> Put( string url ) {
            return new HttpRequest<TResult>( HttpMethod.Put, url );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public IHttpRequest<TResult> Delete( string url ) {
            return new HttpRequest<TResult>( HttpMethod.Delete, url );
        }
    }
}