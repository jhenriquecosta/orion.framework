using Orion.Framework.Dependency;

namespace Orion.Framework.Contexts {

    public interface IContext : ISingletonDependency {
        /// <summary>
     
        /// </summary>
        string TraceId { get; }
      
      
        void Add<T>( string key, T value );
     
        T Get<T>( string key );
      
        void Remove( string key );
    }
}
