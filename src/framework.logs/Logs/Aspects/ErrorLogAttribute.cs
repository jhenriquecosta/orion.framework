using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using Orion.Framework.Aspects.Base;
using Orion.Framework.Logs.Extensions;

namespace Orion.Framework.Logs.Aspects {
    /// <summary>
    /// 
    /// </summary>
    public class ErrorLogAttribute : InterceptorBase {
        /// <summary>
        /// 
        /// </summary>
        public override async Task Invoke( AspectContext context, AspectDelegate next ) {
            var methodName = GetMethodName( context );
            var log = Log.GetLog( methodName );
            try {
                await next( context );
            }
            catch ( Exception ex ) {
                log.Class( context.ServiceMethod.DeclaringType.FullName ).Method( methodName ).Exception( ex );
                foreach ( var parameter in context.GetParameters() )
                    parameter.AppendTo( log );
                log.Error();
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetMethodName( AspectContext context ) {
            return $"{context.ServiceMethod.DeclaringType.FullName}.{context.ServiceMethod.Name}";
        }
    }
}
