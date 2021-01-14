using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Orion.Framework.Helpers;
using Orion.Framework.Locks;
using Orion.Framework.Properties;

namespace Orion.Framework.Web.Mvc.Filters
{

    [AttributeUsage( AttributeTargets.Method )]
    public class AntiDuplicateRequestAttribute : ActionFilterAttribute {
       
        public string Key { get; set; }
        
        public LockType Type { get; set; } = LockType.User;
       
        public int Interval { get; set; }

     
        public override async Task OnActionExecutionAsync( ActionExecutingContext context, ActionExecutionDelegate next ) {
            if( context == null )
                throw new ArgumentNullException( nameof( context ) );
            if( next == null )
                throw new ArgumentNullException( nameof( next ) );
            var @lock = CreateLock();
            var key = GetKey( context );
            var isSuccess = false;
            try {
                isSuccess = @lock.Lock( key, GetExpiration() );
                if ( isSuccess == false ) {
                    context.Result = new Result( StateCode.Fail, GetFailMessage() );
                    return;
                }
                OnActionExecuting( context );
                if ( context.Result != null )
                    return;
                var executedContext = await next();
                OnActionExecuted( executedContext );
            }
            finally {
                if( isSuccess )
                    @lock.UnLock();
            }
        }

      
        private ILock CreateLock() {
            return Ioc.Create<ILock>() ?? NullLock.Instance;
        }

       
        protected virtual string GetKey( ActionExecutingContext context ) {
            var userId = string.Empty;
            if( Type == LockType.User )
                userId = $"{Sessions.Session.Instance.UserId}_";
            return string.IsNullOrWhiteSpace( Key ) ? $"{userId}{WebHttp.Request.Path}" : $"{userId}{Key}";
        }

      
        private TimeSpan? GetExpiration() {
            if ( Interval == 0 )
                return null;
            return TimeSpan.FromSeconds( Interval );
        }

        protected virtual string GetFailMessage() {
            if ( Type == LockType.User )
                return R.UserDuplicateRequest;
            return R.GlobalDuplicateRequest;
        }
    }

   
    public enum LockType {
       
        User = 0,
      
        Global = 1
    }
}
