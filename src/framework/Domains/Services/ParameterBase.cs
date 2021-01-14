using System.Linq;
using Orion.Framework.Exceptions;
using Orion.Framework.Validations;

namespace Orion.Framework.Domains.Services {
    /// <summary>
    /// 
    /// </summary>
    public abstract class ParameterBase : IValidation {
        /// <summary>
        /// 
        /// </summary>
        public virtual ValidationResultCollection Validate() {
            var result = DataAnnotationValidation.Validate( this );
            if( result.IsValid )
                return ValidationResultCollection.Success;
            throw new Warning( result.First().ErrorMessage );
        }
    }
}
