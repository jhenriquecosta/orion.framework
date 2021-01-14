using Orion.Framework.Infrastructurelications;
using Orion.Framework.Web.Applications.Services.Contracts;
using Orion.Framework.Logs;
using Orion.Framework.Sessions;
using Orion.Framework.Applications.Services.Contracts;

namespace Orion.Framework.Web.Applications.Services
{
    
    public abstract class ServiceBase : IService 
    {
     
        private ILog _log;

        
        public ILog Log => _log ?? (_log = GetLog() );

      
        protected virtual ILog GetLog() {
            try {
                return Orion.Framework.Logs.Log.GetLog( this );
            }
            catch {
                return Orion.Framework.Logs.Log.Null;
            }
        }
       
        public virtual ISession Session => Sessions.Session.Instance;
    }
}
