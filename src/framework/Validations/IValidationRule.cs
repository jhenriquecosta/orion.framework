using System.ComponentModel.DataAnnotations;

namespace Orion.Framework.Validations {
    /// <summary>
    /// 
    /// </summary>
    public interface IValidationRule {
        /// <summary>
        /// 
        /// </summary>
        ValidationResult Validate();
    }
}
