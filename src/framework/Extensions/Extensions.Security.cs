using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Orion.Framework.Security.Core.Principals;

namespace Orion.Framework {
    /// <summary>
  
    /// </summary>
    public static partial class Extensions {
        /// <summary>
    
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="type"></param>
        public static string GetValue( this ClaimsIdentity identity, string type ) {
            var claim = identity.FindFirst( type );
            if( claim == null )
                return string.Empty;
            return claim.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public static ClaimsIdentity GetIdentity( this HttpContext context ) {
            if( context == null )
                return UnauthenticatedIdentity.Instance;
            if( !(context.User is ClaimsPrincipal principal) )
                return UnauthenticatedIdentity.Instance;
            if( principal.Identity is ClaimsIdentity identity )
                return identity;
            return UnauthenticatedIdentity.Instance;
        }
    }
}
