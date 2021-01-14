using System.Text.RegularExpressions;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Extensions
{
     /// <summary>
    /// Provides extension methods fot the Type class
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Surronds a text with brakets
        /// </summary>
        /// <param name="input">The input text</param>
        /// <returns>The surronded text</returns>
        internal static string AddSquareBrackets(this string input)
        {
            return !string.IsNullOrEmpty(input) && !Regex.IsMatch(input, @"\[\S*\]") ? string.Format("[{0}]", input) : input;
        }

    }
}
