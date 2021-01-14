namespace Orion.Framework.Validations.Validators {
    /// <summary>
    /// 
    /// </summary>
    public static class ValidatePattern {
        /// <summary>
        /// 
        /// </summary>
        public static string MobilePhonePattern = "^1[0-9]{10}$";
        /// <summary>
        /// 
        /// </summary>
        public static string IdCardPattern = @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)";
    }
}
