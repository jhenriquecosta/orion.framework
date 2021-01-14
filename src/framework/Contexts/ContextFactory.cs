using Orion.Framework.Helpers;

namespace Orion.Framework.Contexts {
  
    public static class ContextFactory {
     
      
        public static IContext Create() {
            if ( WebHttp.HttpContext == null )
                return NullContext.Instance;
            return new WebContext();
        }
    }
}
