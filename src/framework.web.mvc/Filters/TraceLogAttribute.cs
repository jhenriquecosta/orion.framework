using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Orion.Framework.Helpers;
using Orion.Framework.Logs;
using Orion.Framework.Logs.Extensions;

namespace Orion.Framework.Web.Mvc.Filters
{

    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method )]
    public class TraceLogAttribute : ActionFilterAttribute {
       
        public const string TraceLogName = "ApiTraceLog";

       
        public bool Ignore { get; set; }

      
        public override async Task OnActionExecutionAsync( ActionExecutingContext context, ActionExecutionDelegate next ) {
            if( context == null )
                throw new ArgumentNullException( nameof( context ) );
            if( next == null )
                throw new ArgumentNullException( nameof( next ) );
            var log = GetLog();
            OnActionExecuting( context );
            await OnActionExecutingAsync( context, log );
            if( context.Result != null )
                return;
            var executedContext = await next();
            OnActionExecuted( executedContext );
            OnActionExecuted( executedContext, log );
        }

     
        private ILog GetLog() {
            try {
                return Log.GetLog( TraceLogName );
            }
            catch {
                return Log.Null;
            }
        }

       
        protected virtual async Task OnActionExecutingAsync( ActionExecutingContext context,ILog log ) {
            if( Ignore )
                return;
            if( log.IsTraceEnabled == false )
                return;
            await WriteLog( context, log );
        }

        
        private async Task WriteLog( ActionExecutingContext context, ILog log ) {
            log.Caption( "WebApi" )
                .Class( context.Controller.SafeString() )
                .Method( context.ActionDescriptor.DisplayName );
            await AddRequestInfo( context,log );
            log.Trace();
        }

     
        private async Task AddRequestInfo( ActionExecutingContext context, ILog log ) {
            var request = context.HttpContext.Request;
            log.Params( "Http", request.Method );
            if( string.IsNullOrWhiteSpace( request.ContentType ) == false )
                log.Params( "ContentType", request.ContentType );
            await AddFormParams( request, log );
            AddCookie( request, log );
        }

       
        private async Task AddFormParams( Microsoft.AspNetCore.Http.HttpRequest request, ILog log ) {
            if( IsMultipart( request.ContentType ) )
                return;
            //request.EnableRewind();
            var result = await File.ToStringAsync( request.Body, isCloseStream: false );
            if( string.IsNullOrWhiteSpace( result ) )
                return;
            log.Params( ":" ).Params( result );
        }

       
        private static bool IsMultipart( string contentType ) {
            if( string.IsNullOrWhiteSpace( contentType ) )
                return false;
            return contentType.IndexOf( "multipart/", StringComparison.OrdinalIgnoreCase ) >= 0;
        }

        private void AddCookie( Microsoft.AspNetCore.Http.HttpRequest request, ILog log ) {
            log.Params( "Cookie:" );
            foreach( var key in request.Cookies.Keys )
                log.Params( key, request.Cookies[key] );
        }

       
        protected virtual void OnActionExecuted( ActionExecutedContext context, ILog log ) {
            if( Ignore )
                return;
            if( log.IsTraceEnabled == false )
                return;
            WriteLog( context, log );
        }

      
        private void WriteLog( ActionExecutedContext context, ILog log ) {
            log.Caption( "WebApi" )
                .Class( context.Controller.SafeString() )
                .Method( context.ActionDescriptor.DisplayName );
            AddResponseInfo( context, log );
            AddResult( context, log );
            log.Trace();
        }

      
        private void AddResponseInfo( ActionExecutedContext context, ILog log ) {
            var response = context.HttpContext.Response;
            if( string.IsNullOrWhiteSpace( response.ContentType ) == false )
                log.Content( $"ContentType: {response.ContentType}" );
            log.Content( $"Http: {response.StatusCode}" );
        }

     
        private void AddResult( ActionExecutedContext context, ILog log ) {
            if( !( context.Result is Result result ) )
                return;
            log.Content($": { result.Message}").Content("Conteudo:").Content( $"{Json.Json.ToJson( result.Data )}" );
        }
    }
}
