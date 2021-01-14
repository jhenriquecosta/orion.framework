namespace Orion.Framework.Helpers {
    /// <summary>
    /// 
    /// </summary>
    public class Validation {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">输入值</param>        
        public static bool IsNumber( string input ) {
            if( input.IsEmpty() )
                return false;
            const string pattern = @"^(-?\d*)(\.\d+)?$";
            return Regex.IsMatch( input, pattern );
        }
    }
}
