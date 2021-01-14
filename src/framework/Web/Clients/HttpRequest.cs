using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Orion.Framework.Helpers;

namespace Orion.Framework.Web.Clients {
   
    public class HttpRequest : HttpRequestBase<IHttpRequest>, IHttpRequest {
        private Action<string> _successAction;
        private Action<string, HttpStatusCode> _successStatusCodeAction;

      
        public HttpRequest( HttpMethod httpMethod, string url ) : base( httpMethod, url ) {
        }

     
        public IHttpRequest OnSuccess( Action<string> action ) {
            _successAction = action;
            return this;
        }

     
        public IHttpRequest OnSuccess( Action<string, HttpStatusCode> action ) {
            _successStatusCodeAction = action;
            return this;
        }

     
        protected override void SuccessHandler( string result, HttpStatusCode statusCode, string contentType ) {
            _successAction?.Invoke( result );
            _successStatusCodeAction?.Invoke( result, statusCode );
        }

        
        public async Task<TResult> ResultFromJsonAsync<TResult>() {
            return Orion.Framework.Json.Json.ToObject<TResult>( await ResultAsync() );
        }
    }

   
    public class HttpRequest<TResult> : HttpRequestBase<IHttpRequest<TResult>>, IHttpRequest<TResult> where TResult : class {
       
        private Action<TResult> _successAction;
       
        private Action<TResult, HttpStatusCode> _successStatusCodeAction;
       
        private Func<string, TResult> _convertAction;

       
        public HttpRequest( HttpMethod httpMethod, string url ) : base( httpMethod,url ){
        }

        
        public IHttpRequest<TResult> OnSuccess( Action<TResult> action, Func<string, TResult> convertAction = null ) {
            _successAction = action;
            _convertAction = convertAction;
            return this;
        }

      
        public IHttpRequest<TResult> OnSuccess( Action<TResult, HttpStatusCode> action, Func<string, TResult> convertAction = null ) {
            _successStatusCodeAction = action;
            _convertAction = convertAction;
            return this;
        }

        
        protected override void SuccessHandler( string result, HttpStatusCode statusCode, string contentType ) {
            TResult successResult = ConvertTo( result, contentType );
            _successAction?.Invoke( successResult );
            _successStatusCodeAction?.Invoke( successResult, statusCode );
        }

       
        private TResult ConvertTo( string result, string contentType ) {
            if( typeof( TResult ) == typeof( string ) )
                return Orion.Framework.Helpers.TypeConvert.To<TResult>( result );
            if( _convertAction != null )
                return _convertAction( result );
            if( contentType.SafeString().ToLower() == "application/json" )
                return Json.Json.ToObject<TResult>( result );
            return null;
        }

       
        public async Task<TResult> ResultFromJsonAsync() {
            return Orion.Framework.Json.Json.ToObject<TResult>( await ResultAsync() );
        }
    }
}