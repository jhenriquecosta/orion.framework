using System.Linq;
using System.Runtime.Serialization;
using Orion.Framework.Exceptions;
using Orion.Framework.Validations;

namespace Orion.Framework.Views {
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public abstract class ViewModelBase : IValidation {
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
