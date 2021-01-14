using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Orion.Framework.Helpers;
using Orion.Framework.Reflections;

namespace Orion.Framework.Dependency {
   
    internal class Container : IContainer {
      
        private Autofac.IContainer _container;

        public List<T> CreateList<T>( string name = null ) {
            var result = CreateList( typeof( T ), name );
            if( result == null )
                return new List<T>();
            return ( (IEnumerable<T>)result ).ToList();
        }

        public Autofac.IContainer GetContainer()
        {
            return _container;
        }
        public object CreateList( Type type, string name = null ) {
            Type serviceType = typeof( IEnumerable<> ).MakeGenericType( type );
            return Create( serviceType, name );
        }
        public dynamic Create(string name)
        {

            var _services = _container.ComponentRegistry.Registrations.SelectMany(f => f.Services).OfType<IServiceWithType>();
            foreach(var item in _services)
            {
             //   Trace.WriteLine(item.ServiceType.Name);
                if (item.ServiceType.Name.Equals(name))
                {
                    return _container.Resolve(item.ServiceType);
                }
            };
            return null;
        }

        public T Create<T>( string name = null )
        {
            return (T)Create( typeof( T ), name );
        }

       
        public object Create( Type type, string name = null ) {
            return WebHttp.HttpContext?.RequestServices != null ? GetServiceFromHttpContext( type, name ) : GetService( type, name );
        }

      
        private object GetServiceFromHttpContext( Type type, string name ) {
            var serviceProvider = WebHttp.HttpContext.RequestServices;
            if( name == null )
                return serviceProvider.GetService( type );
            var context = serviceProvider.GetService<IComponentContext>();
            return context.ResolveNamed( name, type );
        }

       
        private object GetService( Type type, string name ) {
            if (name == null)
                return  _container.Resolve(type)  ;
            return _container.ResolveNamed( name, type );
        }

     
        public IScope BeginScope() {
            return new Scope( _container.BeginLifetimeScope() );
        }

    
        public void Register( params IConfig[] configs )
        {
            Register( null, null, configs );
        }

       
        public IServiceProvider Register( IServiceCollection services, params IConfig[] configs ) {
            return Register( services, null, configs );
        }

      
        public IServiceProvider Register( IServiceCollection services, Action<ContainerBuilder> actionBefore, params IConfig[] configs ) {
            var builder = CreateBuilder( services, actionBefore, configs );
            _container = builder.Build();
            return new AutofacServiceProvider( _container );
        }
        public ContainerBuilder CreateBuilder(IServiceCollection services, Action<ContainerBuilder> actionBefore, params IConfig[] configs)
        {
            var builder = new ContainerBuilder();
            actionBefore?.Invoke(builder);
            if (configs != null)
            {
                foreach (var config in configs)
                    builder.RegisterModule(config);
            }
            if (services == null)
            {
                services = new ServiceCollection();
                builder.AddSingleton(services);
            }
            builder.Populate(services);
            return builder;
        }

        //public ContainerBuilder CreateBuilder( IServiceCollection services, Action<ContainerBuilder> actionBefore, params IConfig[] configs ) {
        //    var builder = new ContainerBuilder();
        //    actionBefore?.Invoke( builder );
        //    if ( configs != null )
        //    {
        //        foreach ( var config in configs )
        //            builder.RegisterModule( config );
        //    }
        //    if( services != null )
        //        builder.Populate( services );
        //    return builder;
        //}


        public void Dispose()
        {
            _container.Dispose();
        }
    }
}