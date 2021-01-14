using Microsoft.AspNetCore.Builder;
using Orion.Framework.Web.Mvc.Middlewares;

namespace Orion.Framework.Web.Mvc.Extensions {
  
    public static partial class Extensions {
    
        public static IApplicationBuilder UseErrorLog( this IApplicationBuilder builder ) {
            return builder.UseMiddleware<ErrorLogMiddleware>();
        }

        
        public static IApplicationBuilder UseRealIp(this IApplicationBuilder builder) {
            return builder.UseMiddleware<RealIpMiddleware>();
        }
    }
}
