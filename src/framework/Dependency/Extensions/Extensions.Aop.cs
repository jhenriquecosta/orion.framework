using System;
using AspectCore.Configuration;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Extensions.AspectScope;
using AspectCore.Extensions.Autofac;
using Autofac;
using Orion.Framework.Helpers;

namespace Orion.Framework.Dependency {
    
    public static partial class Extensions {
      
        public static void EnableAop( this ContainerBuilder builder,Action<IAspectConfiguration> configAction = null )
        {
            builder.RegisterDynamicProxy(config =>
            {
 
                config.EnableParameterAspect();
                config.NonAspectPredicates.AddNamespace("*DataLayer*");
                configAction?.Invoke(config);
            });
            builder.EnableAspectScoped();
        }
              
	    public static void EnableAspectScoped( this ContainerBuilder builder )
        {
            builder.AddScoped<IAspectScheduler, ScopeAspectScheduler>();
            builder.AddScoped<IAspectBuilderFactory, ScopeAspectBuilderFactory>();
            builder.AddScoped<IAspectContextFactory, ScopeAspectContextFactory>();
         
        }
    }
}
