using System.Text.RegularExpressions;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Helpers
{
    /// <summary>
    /// Provides methods to work with naming conventions
    /// </summary>
    internal class NamingHelper
    {
        #region Public Methods

        /// <summary>
        /// Converts a given text to pascal case
        /// </summary>
        /// <param name="input">The text to convert</param>
        /// <returns>The text in pascal case format</returns>
        public static string ToPascalCase(string input)
        {
            return !string.IsNullOrEmpty(input) ? input.Underscore().Pascalize() : input;
        }

        /// <summary>
        /// Converts a given text to camel case
        /// </summary>
        /// <param name="input">The text to convert</param>
        /// <returns>The text in camel case format</returns>
        public static string ToCamelCase(string input)
        {
            return !string.IsNullOrEmpty(input) ? input.Underscore().Camelize() : input;
        }

        /// <summary>
        /// Converts a given text to uppercase underscore separated format
        /// </summary>
        /// <param name="input">The text to convert</param>
        /// <returns>The text in uppercase underscore separated format</returns>
        public static string ToUppercase(string input)
        {
            return !string.IsNullOrEmpty(input) ? input.Underscore().ToUpper() : input;
        }

        /// <summary>
        /// Converts a given text to lowercase underscore separated format
        /// </summary>
        /// <param name="input">The text to convert</param>
        /// <returns>The text in lowercase underscore separated format</returns>
        internal static string ToLowercase(string input)
        {
            return !string.IsNullOrEmpty(input) ? input.Underscore().ToLower() : input;
        }

        #endregion

    }
}
