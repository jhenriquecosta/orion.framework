using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Orion.Framework.Web.Mvc.Extensions {
   
    public static partial class Extensions {
     
        public static IApplicationBuilder UseSwaggerX( this IApplicationBuilder builder,Action<SwaggerUIOptions> swaggerUiSetup = null ) {
            builder.UseSwagger();
            builder.UseSwaggerUI( options => {
                options.IndexStream = () => typeof( Extensions ).GetTypeInfo().Assembly.GetManifestResourceStream( "Orion.Framework.Web.Mvc.Swaggers.index.html" );
                if ( swaggerUiSetup == null ) {
                    options.SwaggerEndpoint( "/swagger/v1/swagger.json", "api v1" );
                    return;
                }
                swaggerUiSetup( options );
            } );
            return builder;
        }
    }
}
