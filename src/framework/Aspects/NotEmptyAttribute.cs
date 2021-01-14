using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Orion.Framework.Aspects.Base;

namespace Orion.Framework.Aspects {
    /// <summary>
    /// 
    /// </summary>
    public class NotEmptyAttribute : ParameterInterceptorBase {
        /// <summary>
        /// 
        /// </summary>
        public override Task Invoke( ParameterAspectContext context, ParameterAspectDelegate next ) {
            if( string.IsNullOrWhiteSpace( context.Parameter.Value.SafeString() ) )
                throw new ArgumentNullException( context.Parameter.Name );
            return next( context );
        }
    }
}
