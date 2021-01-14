using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Orion.Framework.Aspects.Base;

namespace Orion.Framework.Aspects {
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage( AttributeTargets.Parameter )]
    public class NotNullAttribute : ParameterInterceptorBase {
        /// <summary>
        /// 
        /// </summary>
        public override Task Invoke( ParameterAspectContext context, ParameterAspectDelegate next ) {
            if( context.Parameter.Value == null )
                throw new ArgumentNullException( context.Parameter.Name );
            return next( context );
        }
    }
}
