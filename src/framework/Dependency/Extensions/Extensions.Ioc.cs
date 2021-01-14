using Autofac;
using Autofac.Builder;

namespace Orion.Framework.Dependency {
    
    public static partial class Extensions {
      
        public static IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            AddTransient<TService, TImplementation>( this ContainerBuilder builder, string name = null ) where TService : class where TImplementation : class, TService {
            if( name == null )
                return builder.RegisterType<TImplementation>().As<TService>().InstancePerDependency();
            return builder.RegisterType<TImplementation>().Named<TService>( name ).InstancePerDependency();
        }

        
        public static IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            AddScoped<TService, TImplementation>( this ContainerBuilder builder, string name = null ) where TService : class where TImplementation : class, TService {
            if( name == null )
                return builder.RegisterType<TImplementation>().As<TService>().InstancePerLifetimeScope();
            return builder.RegisterType<TImplementation>().Named<TService>( name ).InstancePerLifetimeScope();
        }

       
        public static IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            AddScoped<TImplementation>( this ContainerBuilder builder ) where TImplementation : class  {
            return builder.RegisterType<TImplementation>().InstancePerLifetimeScope();
        }

       
        public static IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            AddSingleton<TService, TImplementation>( this ContainerBuilder builder, string name = null ) where TService : class where TImplementation : class, TService {
            if( name == null )
                return builder.RegisterType<TImplementation>().As<TService>().SingleInstance();
            return builder.RegisterType<TImplementation>().Named<TService>( name ).SingleInstance();
        }

        
        public static IRegistrationBuilder<TService, SimpleActivatorData, SingleRegistrationStyle>
            AddSingleton<TService>( this ContainerBuilder builder, TService instance ) where TService : class {
            return builder.RegisterInstance( instance ).As<TService>().SingleInstance();
        }
    }
}
