using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Orion.Framework.Web.Mvc
{
   
    public class Result : JsonResult 
    {
      
        public StateCode Code { get; }
       
        public string Message { get; }
        
        public dynamic Data { get; }

      
        public Result( StateCode code, string message, dynamic data = null ) : base( null ) {
            Code = code;
            Message = message;
            Data = data;
        }

      
        public override Task ExecuteResultAsync( ActionContext context ) {
            this.Value = new {
                Code = Code.Value(),
                Message = Message,
                Data = Data
            };
            return base.ExecuteResultAsync( context );
        }
    }
}
