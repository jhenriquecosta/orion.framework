using Microsoft.AspNetCore.Mvc.Filters;
using Orion.Framework.Logs;
using Orion.Framework.Logs.Extensions;

namespace Orion.Framework.Web.Mvc.Filters {
   
    public class ErrorLogAttribute : ExceptionFilterAttribute {
       
        public override void OnException( ExceptionContext context ) {
            if( context == null )
                return;
            var log = Log.GetLog( context ).Caption( "webapi" );
            context.Exception.Log( log );
        }
    }
}