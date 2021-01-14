using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Orion.Framework.Validations {
    /// <summary>
    /// 
    /// </summary>
    public static class DataAnnotationValidation {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public static ValidationResultCollection Validate( object target ) {
            if( target == null )
                throw new ArgumentNullException( nameof( target ) );
            var result = new ValidationResultCollection();
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext( target, null, null );
            var isValid = Validator.TryValidateObject( target, context, validationResults, true );
            if ( !isValid )
                result.AddList( validationResults );
            return result;
        }
    }
}
