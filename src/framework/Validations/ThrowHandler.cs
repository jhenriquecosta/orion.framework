using System.Linq;
using Orion.Framework.Exceptions;

namespace Orion.Framework.Validations {
    /// <summary>
    /// 
    /// </summary>
    public class ThrowHandler : IValidationHandler{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="results"></param>
        public void Handle( ValidationResultCollection results ) {
            if ( results.IsValid )
                return;
            throw new Warning( results.First().ErrorMessage );
        }
    }
}
