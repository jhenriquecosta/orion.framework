using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Orion.Framework.Dependency {
   
    public interface IContainer : IDisposable {
       
        List<T> CreateList<T>( string name = null );

    
        object CreateList( Type type, string name = null );

   
        T Create<T>( string name = null );

    
        object Create( Type type, string name = null );

      
        IScope BeginScope();

       
        void Register( params IConfig[] configs );

       
        IServiceProvider Register( IServiceCollection services, params IConfig[] configs );
    }
}
