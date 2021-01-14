using System;

namespace Orion.Framework.Dependency {
   
    public interface IScope : IDisposable 
    {
     
        T Create<T>();
        T Resolve<T>();

        bool IsRegistered(Type type);
        object Create( Type type );
    }
}
