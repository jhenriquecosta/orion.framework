using System.Linq;
using Orion.Framework.Domains.Attributes;
using Orion.Framework.Exceptions;
using Orion.Framework.Validations;

namespace Orion.Framework.Applications.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    [Model]
    public abstract class RequestBase : IRequest {
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
