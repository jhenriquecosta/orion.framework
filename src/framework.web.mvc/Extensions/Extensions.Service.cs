using Microsoft.Extensions.DependencyInjection;
using Orion.Framework.Web.Mvc.Razors;

namespace Orion.Framework.Web.Mvc.Extensions {
   
    public static partial class Extensions {
    
        public static IServiceCollection AddRazorHtml( this IServiceCollection services ) {
            services.AddScoped<IRouteAnalyzer, RouteAnalyzer>();
            services.AddScoped<IRazorHtmlGenerator, DefaultRazorHtmlGenerator>();
            return services;
        }
    }
}
