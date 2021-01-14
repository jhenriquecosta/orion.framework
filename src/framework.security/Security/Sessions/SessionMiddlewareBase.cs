using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Orion.Framework.Security.Sessions {
    /// <summary>
    /// 
    /// </summary>
    public abstract class SessionMiddlewareBase {
        /// <summary>
   
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
      
        /// </summary>
        /// <param name="next"></param>
        protected SessionMiddlewareBase( RequestDelegate next ) {
            _next = next;
        }

        /// <summary>
    
        /// </summary>
        /// <param name="context"></param>
        public async Task Invoke( HttpContext context ) {
            await Authenticate( context );
            await _next( context );
        }

        /// <summary>
       
        /// </summary>
        protected virtual async Task Authenticate( HttpContext context ) {
            await AuthenticateBefore( context );
            if( IsAuthenticated( context ) == false )
                return;
            await LoadClaims( context, context.GetIdentity() );
            await AuthenticateAfter( context );
        }

        /// <summary>
      
        /// </summary>
        /// <param name="context"></param>
        protected virtual Task AuthenticateBefore( HttpContext context ) {
            return Task.CompletedTask;
        }

        /// <summary>
       
        /// </summary>
        protected virtual bool IsAuthenticated( HttpContext context ) {
            if( context.User == null )
                return false;
            if( context.User.Identity.IsAuthenticated == false )
                return false;
            return true;
        }

        /// <summary>
        
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identity"></param>
        protected abstract Task LoadClaims( HttpContext context, ClaimsIdentity identity );

        /// <summary>
    
        /// </summary>
        /// <param name="context"></param>
        protected virtual Task AuthenticateAfter( HttpContext context ) {
            return Task.CompletedTask;
        }

        /// <summary>
       
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="T"></typeparam>
        protected T GetService<T>( HttpContext context ) {
            return context.RequestServices.GetService<T>();
        }
    }
}
