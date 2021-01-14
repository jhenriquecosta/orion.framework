namespace Orion.Framework.Validations {
    /// <summary>
    /// 
    /// </summary>
    public interface IValidationHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="results"></param>
        void Handle( ValidationResultCollection results );
    }
}
