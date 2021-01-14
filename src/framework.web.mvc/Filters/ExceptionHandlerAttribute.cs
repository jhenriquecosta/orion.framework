using Microsoft.AspNetCore.Mvc.Filters;

namespace Orion.Framework.Web.Mvc.Filters
{

    public class ExceptionHandlerAttribute : ExceptionFilterAttribute {
      
        public override void OnException( ExceptionContext context ) {
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            context.Result = new Result( StateCode.Fail, context.Exception.GetPrompt() );
        }
    }
}
