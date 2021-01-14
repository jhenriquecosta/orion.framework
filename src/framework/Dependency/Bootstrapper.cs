using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AspectCore.Configuration;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Orion.Framework.HandleEvents.Handlers;
using Orion.Framework.Helpers;
using Orion.Framework.Reflections;

namespace Orion.Framework.Dependency
{

    public class Bootstrapper {
  
        private readonly IServiceCollection _services;
      
        private readonly IConfig[] _configs;
     
        private readonly IFind _finder;
      
        private static List<Assembly> _assemblies;
 
        private ContainerBuilder _builder;
     
        private readonly Action<IAspectConfiguration> _aopConfigAction;

    
        public Bootstrapper( IServiceCollection services, IConfig[] configs, Action<IAspectConfiguration> aopConfigAction, IFind finder ) {
            _services = services ?? new ServiceCollection();
            _configs = configs;
            _aopConfigAction = aopConfigAction;
            _finder = finder ?? new Finder();
        }

     
        public static IServiceProvider Run( IServiceCollection services = null, IConfig[] configs = null, 
                Action<IAspectConfiguration> aopConfigAction=null, IFind finder = null ) {
            return new Bootstrapper( services, configs, aopConfigAction, finder ).Bootstrap();
        }

      
        public static IServiceProvider Run( IServiceCollection services, params IConfig[] configs ) {
            return Run( services, configs, null );
        }

       
        public IServiceProvider Bootstrap() {
            _assemblies = _finder.GetAssemblies();
            return Ioc.DefaultContainer.Register( _services, RegisterServices, _configs );
        }

       
        private void RegisterServices( ContainerBuilder builder ) {
            _builder = builder;
            RegisterInfrastracture();
            RegisterEventHandlers();
            RegisterDependency();
        }

      
        private void RegisterInfrastracture() {
            EnableAop();
            RegisterFinder();
        }

     
        private void EnableAop() {
            _builder.EnableAop( _aopConfigAction );
        }

      
        private void RegisterFinder() {
            _builder.AddSingleton( _finder );
        }

       
        private void RegisterEventHandlers() {
            RegisterEventHandlers( typeof( IEventHandler<> ) );
        }

       
        private void RegisterEventHandlers( Type handlerType ) {
            var handlerTypes = GetTypes( handlerType );
            foreach( var handler in handlerTypes ) {
                _builder.RegisterType( handler ).As( handler.FindInterfaces(
                    ( filter, criteria ) => filter.IsGenericType && ( (Type)criteria ).IsAssignableFrom( filter.GetGenericTypeDefinition() )
                    , handlerType
                ) ).InstancePerLifetimeScope();
            }
        }

    
        private Type[] GetTypes( Type type ) {
            return _finder.Find( type, _assemblies ).ToArray();
        }

        public static IList<Assembly> GetAssemblies()
        {
            return _assemblies;
        }

        private void RegisterDependency() {
            RegisterSingletonDependency();
            RegisterScopeDependency();
            RegisterTransientDependency();
            ResolveDependencyRegistrar();
        }

     
        private void RegisterSingletonDependency() 
        {
            var _types = GetTypes<ISingletonDependency>();
            _builder.RegisterTypes( _types ).AsImplementedInterfaces().PropertiesAutowired().SingleInstance();
        }

     
        private Type[] GetTypes<T>() {
            return _finder.Find<T>( _assemblies ).ToArray();
        }

      
        private void RegisterScopeDependency() 
        {
            var _types = GetTypes<IScopeDependency>();
            var _typesNoInterface = _types.Where(f => f.IsImplementationOf(typeof(INoRegisterInterfaceDependency))).ToArray();

            _builder.RegisterTypes(_types ).AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
            if (_typesNoInterface.Any())
            {
                _builder.RegisterTypes(_typesNoInterface).PropertiesAutowired();
            }
        }

      
        private void RegisterTransientDependency() 
        {
            var _types = GetTypes<ITransientDependency>();
            _builder.RegisterTypes( _types).AsImplementedInterfaces().PropertiesAutowired().InstancePerDependency();
        }

      
        private void ResolveDependencyRegistrar() {
            var types = GetTypes<IDependencyRegistrar>();
            types.Select( type => Reflection.CreateInstance<IDependencyRegistrar>( type ) ).ToList().ForEach( t => t.Register( _services ) );
        }
    }
}
