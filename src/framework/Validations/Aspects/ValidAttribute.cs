using System.Collections.Generic;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Orion.Framework.Aspects.Base;
using Orion.Framework.Helpers;

namespace Orion.Framework.Validations.Aspects {
    /// <summary>
    /// 
    /// </summary>
    public class ValidAttribute : ParameterInterceptorBase {
        /// <summary>
        /// 
        /// </summary>
        public override async Task Invoke( ParameterAspectContext context, ParameterAspectDelegate next ) {
            Validate( context.Parameter );
            await next( context );
        }

        /// <summary>
        /// 
        /// </summary>
        private void Validate( Parameter parameter ) {
            if ( Reflection.IsGenericCollection( parameter.RawType ) ) {
                ValidateCollection( parameter );
                return;
            }
            IValidation validation = parameter.Value as IValidation;
            validation?.Validate();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ValidateCollection( Parameter parameter ) {
            if ( !( parameter.Value is IEnumerable<IValidation> validations ) )
                return;
            foreach ( var validation in validations )
                validation.Validate();
        }
    }
}
