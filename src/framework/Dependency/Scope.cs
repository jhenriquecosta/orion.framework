using System;
using Autofac;

namespace Orion.Framework.Dependency {
  
    internal class Scope : IScope {
      
        private readonly ILifetimeScope _scope;

     
        public Scope( ILifetimeScope scope ) {
            _scope = scope;
        }

       
        public T Create<T>() {
            return _scope.Resolve<T>();
        }

      
        public object Create( Type type ) {
            return _scope.Resolve( type );
        }

    
        public void Dispose() {
            _scope.Dispose();
        }

        public bool IsRegistered(Type type)
        {
            return _scope.IsRegistered(type);
        }

        public T Resolve<T>()
        {
            return this.Create<T>();
        }
    }
}
