using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using Orion.Framework.Aspects.Base;
using Orion.Framework.Logs.Extensions;

namespace Orion.Framework.Logs.Aspects {
    /// <summary>
    /// 
    /// </summary>
    public abstract class LogAttributeBase : InterceptorBase {
        /// <summary>
        /// 
        /// </summary>
        public override async Task Invoke( AspectContext context, AspectDelegate next ) {
            var methodName = GetMethodName( context );
            var log = Log.GetLog( methodName );
            if( !Enabled( log ) )
                return;
            ExecuteBefore( log, context, methodName );
            await next( context );
            ExecuteAfter( log, context, methodName );
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetMethodName( AspectContext context ) {
            return $"{context.ServiceMethod.DeclaringType?.FullName}.{context.ServiceMethod?.Name}";
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual bool Enabled( ILog log ) {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ExecuteBefore( ILog log, AspectContext context, string methodName ) {
            log.Caption( $"{context.ServiceMethod.Name}" )
                .Class( context.ServiceMethod.DeclaringType?.FullName )
                .Method( methodName );
            foreach( var parameter in context.GetParameters() )
                parameter.AppendTo( log );
            WriteLog( log );
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void WriteLog( ILog log );

        /// <summary>
        /// 
        /// </summary>
        private void ExecuteAfter( ILog log, AspectContext context, string methodName ) {
            var parameter = context.GetReturnParameter();
            log.Caption( $"{context.ServiceMethod.Name}" )
                .Method( methodName )
                .Content( $": {parameter.ParameterInfo.ParameterType.FullName},: {parameter.Value.SafeString()}" );
            WriteLog( log );
        }
    }
}
