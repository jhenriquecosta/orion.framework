using Microsoft.AspNetCore.Mvc;
using Orion.Framework.Logs;
using Orion.Framework.Properties;
using Orion.Framework.Sessions;
using Orion.Framework.Web.Mvc.Filters;

namespace Orion.Framework.Web.Mvc.Controllers
{

    [Route( "api/[controller]" )]
    [ExceptionHandler]
    [ErrorLog]
    [TraceLog]
    public abstract class WebApiControllerBase : Controller {
     
        private ILog _log;

       
        public ILog Log => _log ?? ( _log = GetLog() );

       
        protected virtual ILog GetLog() {
            try {
                return Orion.Framework.Logs.Log.GetLog( this );
            }
            catch {
                return Orion.Framework.Logs.Log.Null;
            }
        }

        public virtual ISession Session =>  Sessions.Session.Instance;

     
        protected virtual IActionResult Success( dynamic data = null, string message = null ) {
            if( message == null )
                message = R.Success;
            var ok = new Result( StateCode.Ok, message, data );
            return ok;
        }

        protected virtual IActionResult Fail( string message ) {
            var fail = new Result( StateCode.Fail, message );
            return fail;
        }
    }
}