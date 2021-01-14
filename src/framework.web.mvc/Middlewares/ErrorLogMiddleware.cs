using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Orion.Framework.Logs;
using Orion.Framework.Logs.Extensions;

namespace Orion.Framework.Web.Mvc.Middlewares {
    /// <summary>
    /// 
    /// </summary>
    public class ErrorLogMiddleware {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ErrorLogMiddleware( RequestDelegate next ) {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public async Task Invoke( HttpContext context ) {
            try {
                await _next( context );
            }
            catch( Exception ex ) {
                WriteLog( context, ex );
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteLog( HttpContext context, Exception ex ) {
            if( context == null )
                return;
            var log = (ILog)context.RequestServices.GetService(typeof(ILog));
            log.Caption( " - " ).Content( $"：{context.Response.StatusCode}" );
            ex.Log( log );
        }
    }
}
