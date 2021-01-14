using Orion.Framework.Helpers;

namespace Orion.Framework.Contexts {

    public class WebContext : IContext {
      
        public string TraceId => WebHttp.HttpContext?.TraceIdentifier;

        
        public void Add<T>( string key, T value ) {
            if(WebHttp.HttpContext == null )
                return;
            WebHttp.HttpContext.Items[key] = value;
        }

    
        public T Get<T>( string key ) {
            if(WebHttp.HttpContext == null )
                return default( T );
            return Orion.Framework.Helpers.TypeConvert.To<T>(WebHttp.HttpContext.Items[key] );
        }

       
        public void Remove( string key ) {
            WebHttp.HttpContext?.Items.Remove( key );
        }
    }
}
