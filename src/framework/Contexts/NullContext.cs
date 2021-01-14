namespace Orion.Framework.Contexts {
  
    public class NullContext : IContext {
     
        public static readonly NullContext Instance = new NullContext();

       
        public string TraceId => string.Empty;

       
        public void Add<T>( string key, T value ) {
        }

        
        public T Get<T>( string key ) {
            return default( T );
        }

  
        public void Remove( string key ) {
        }
    }
}
