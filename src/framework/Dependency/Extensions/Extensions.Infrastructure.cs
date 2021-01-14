using System;
using System.IO;
using System.Text;
using AspectCore.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orion.Framework.Dependency;
using Orion.Framework.Sessions;
using Orion.Framework.Settings;

namespace Orion.Framework
{

    public static partial class Extensions 
    {
 

        public static IServiceProvider AddOrionFramework( this IServiceCollection services, params IConfig[] configs ) {
            return AddOrionFramework( services, null, configs );
        }

      
        public static IServiceProvider AddOrionFramework( this IServiceCollection services, Action<IAspectConfiguration> aopConfigAction, params IConfig[] configs ) {
            services.AddHttpContextAccessor();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            services.AddSingleton<ISession, Session>();
            return Bootstrapper.Run( services, configs, aopConfigAction );
        }
    }
}
