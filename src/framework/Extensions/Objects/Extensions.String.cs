using Orion.Framework.Helpers;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Enum = System.Enum;
using Regex = System.Text.RegularExpressions.Regex;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

namespace Orion.Framework
{

    public static partial class Extensions
    {
        public const string WhiteSpace = " ";
        // <summary>
        /// Parses a property name and returns a friendly name. If <c>inString</c> is empty, or white space, returns <c>String.Empty</c>.
        /// </summary>
        /// <param name="inString">The string to get words from.</param>
        /// <returns>String with words parsed from in string with a space added between upper case characters.</returns>
        public static string GetWords(this string inString)
        {
            if (string.IsNullOrWhiteSpace(inString))
            {
                return string.Empty;
            }
            var sb = new System.Text.StringBuilder(256);
            Boolean foundUpper = false;

            foreach (char c in inString)
            {
                if (foundUpper)
                {
                    if (char.IsUpper(c))
                    {
                        sb.Append(WhiteSpace);
                        sb.Append(c);
                    }
                    else if (char.IsLetterOrDigit(c))
                    {
                        sb.Append(c);
                    }
                }
                else if (char.IsUpper(c))
                {
                    foundUpper = true;
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
        public static string ToNormalize(this string input)
        {
            return input.Trim().Normalize().ToUpperInvariant();
        }

        /// <summary>
        /// Extension method that receives an incoming pascal case string and separates the words.
        /// </summary>
        /// <param name="pascalCaseString">Incoming pascal case word</param>
        /// <returns>The seperated string.</returns>
        /// <example>
        /// String str = "PascalCaseWord";
        /// string result = str.SeparatePascalCaseWords ();
        /// -- result = "Pascal Case Word";
        /// </example>
        public static string SeparatePascalCaseWords(this string pascalCaseString)
        {
            var arry = pascalCaseString.ToCharArray();
            var sb = new StringBuilder();
            foreach (var ch in arry)
            {
                if (char.IsUpper(ch) && sb.Length > 0)
                {
                    sb.Append(' ');
                }
                sb.Append(ch);
            }

            var newstr = sb.ToString();

            return newstr;
        }

        /// <summary>
        /// Replaces all instances of a 'key' (e.g. {foo} or {foo:SomeFormat}) in a string with an optionally formatted value, and returns the result.
        /// </summary>
        /// <param name="formatString">The string containing the key; unformatted ({foo}), or formatted ({foo:SomeFormat})</param>
        /// <param name="key">The key name (foo)</param>
        /// <param name="replacementValue">The replacement value; if null is replaced with an empty string</param>
        /// <returns>The input string with any instances of the key replaced with the replacement value</returns>
        public static string InjectSingleValue(this string formatString, string key, object replacementValue)
        {
            var result = formatString;

            //regex replacement of key with value, where the generic key format is:
            //Regex foo = new Regex("{(foo)(?:}|(?::(.[^}]*)}))");
            var attributeRegex = new Regex("{(" + key + ")(?:}|(?::(.[^}]*)}))"); //for key = foo, matches {foo} and {foo:SomeFormat}

            //loop through matches, since each key may be used more than once (and with a different format string)
            foreach (Match m in attributeRegex.Matches(formatString))
            {
                string replacement;
                if (m.Groups[2].Length > 0)
                {
                    //do a double string.Format - first to build the proper format string, and then to format the replacement value
                    var attributeFormatString = string.Format(CultureInfo.InvariantCulture, "{{0:{0}}}", m.Groups[2]);
                    replacement = string.Format(CultureInfo.CurrentCulture, attributeFormatString, replacementValue);
                }
                else
                {
                    replacement = (replacementValue ?? string.Empty).ToString();
                }

                //perform replacements, one match at a time
                result = result.Replace(m.ToString(), replacement); //attributeRegex.Replace(result, replacement, 1);
            }

            return result;
        }

        /// <summary>
        /// Splits the string the into distinct words.
        /// </summary>
        /// <param name="phrase">The given string.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of strings.</returns>
        public static IEnumerable<string> SplitIntoDistinctWords(this string phrase)
        {
            var distinctWords =
                phrase.ToLower().Split(default(string[]), StringSplitOptions.RemoveEmptyEntries).ToList().Distinct();
            return distinctWords;
        }

        /// <summary>
        /// Extension method that replaces keys in a string with the values of matching hashtable entries.
        /// <remarks>Uses <see cref="string.Format(string,object)"/> internally; custom formats should match those used for that method.</remarks>
        /// </summary>
        /// <param name="formatString">The format string, containing keys like {foo} and {foo:SomeFormat}.</param>
        /// <param name="attributes">A <see cref="IDictionary"/> with keys and values to inject into the string</param>
        /// <returns>A version of the formatString string with hastable keys replaced by (formatted) key values.</returns>
        public static string Inject(this string formatString, IDictionary attributes)
        {
            var result = formatString;
            if (attributes == null || formatString == null)
            {
                return result;
            }

            foreach (string attributeKey in attributes.Keys)
            {
                result = result.InjectSingleValue(attributeKey, attributes[attributeKey]);
            }
            return result;
        }

#if !SILVERLIGHT
        /// <summary>
        /// Extension method that replaces keys in a string with the values of matching object properties.
        /// </summary>
        /// <param name="formatString">The format string, containing keys like {foo} and {foo:SomeFormat}.</param>
        /// <param name="injectionObject">The object whose properties should be injected in the string</param>
        /// <returns>A version of the formatString string with keys replaced by (formatted) key values.</returns>
        public static string Inject(this string formatString, object injectionObject)
        {
            return formatString.Inject(GetPropertyHash(injectionObject));
        }

        /// <summary>
        /// Creates a HashTable based on current object state.
        /// <remarks>Copied from the MVCToolkit HtmlExtensionOrion.Frameworkity class</remarks>
        /// </summary>
        /// <param name="properties">The object from which to get the properties</param>
        /// <returns>A <see cref="Hashtable"/> containing the object instance's property names and their values</returns>
        private static IDictionary GetPropertyHash(object properties)
        {
            IDictionary values = null;
            if (properties != null)
            {
                values = new Dictionary<string, object>();
                var props = TypeDescriptor.GetProperties(properties);
                foreach (PropertyDescriptor prop in props)
                {
                    values.Add(prop.Name, prop.GetValue(properties));
                }
            }
            return values;
        }
#endif

        /// <summary>
        /// Removes the non alphanumeric characters.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>A string without alphanumeric char.</returns>
        public static string RemoveNonAlphanumericChar(this string phoneNumber)
        {
            //var arr = phoneNumber.ToCharArray ();

            //arr = System.Array.FindAll<char> ( arr, ( c => ( char.IsDigit ( c ) ) ) );
            //return new string ( arr );

            return Regex.Replace(phoneNumber, "[^0-9]", string.Empty);
        }


        public static string RemoveEnd(this string value, string removeValue)
        {
            return Helpers.String.RemoveEnd(value, removeValue);
        }
        public static string EnsureEndsWith(this string str, char c)
        {
            return EnsureEndsWith(str, c, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Adds a char to end of given string if it does not ends with the char.
        /// </summary>
        public static string EnsureEndsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.EndsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        ///     Adds a char to end of given string if it does not ends with the char.
        /// </summary>
        public static string EnsureEndsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.EndsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        ///     Adds a char to beginning of given string if it does not starts with the char.
        /// </summary>
        public static string EnsureStartsWith(this string str, char c)
        {
            return EnsureStartsWith(str, c, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Adds a char to beginning of given string if it does not starts with the char.
        /// </summary>
        public static string EnsureStartsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.StartsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return c + str;
        }

        /// <summary>
        ///     Adds a char to beginning of given string if it does not starts with the char.
        /// </summary>
        public static string EnsureStartsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.StartsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }

            return c + str;
        }

        /// <summary>
        ///     Indicates whether this string is null or an System.String.Empty string.
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        ///     indicates whether this string is null, empty, or consists only of white-space characters.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        ///     Gets a substring of a string from beginning of the string.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="len" /> is bigger that string's length</exception>
        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }

        /// <summary>
        ///     Converts line endings in the string to <see cref="Environment.NewLine" />.
        /// </summary>
        public static string NormalizeLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        /// <summary>
        ///     Gets index of nth occurence of a char in a string.
        /// </summary>
        /// <param name="str">source string to be searched</param>
        /// <param name="c">Char to search in <see cref="str" /></param>
        /// <param name="n">Count of the occurence</param>
        public static int NthIndexOf(this string str, char c, int n)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            var count = 0;
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] != c)
                {
                    continue;
                }

                if ((++count) == n)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        ///     Removes first occurrence of the given postfixes from end of the given string.
        ///     Ordering is important. If one of the postFixes is matched, others will not be tested.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="postFixes">one or more postfix.</param>
        /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty)
            {
                return string.Empty;
            }

            if (postFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var postFix in postFixes)
            {
                if (str.EndsWith(postFix))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }

        /// <summary>
        ///     Removes first occurrence of the given prefixes from beginning of the given string.
        ///     Ordering is important. If one of the preFixes is matched, others will not be tested.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="preFixes">one or more prefix.</param>
        /// <returns>Modified string or the same string if it has not any of given prefixes</returns>
        public static string RemovePreFix(this string str, params string[] preFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty)
            {
                return string.Empty;
            }

            if (preFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var preFix in preFixes)
            {
                if (str.StartsWith(preFix))
                {
                    return str.Right(str.Length - preFix.Length);
                }
            }

            return str;
        }

        /// <summary>
        ///     Gets a substring of a string from end of the string.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="len" /> is bigger that string's length</exception>
        public static string Right(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        ///     Uses string.Split method to split given string by given separator.
        /// </summary>
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        ///     Uses string.Split method to split given string by given separator.
        /// </summary>
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        /// <summary>
        ///     Uses string.Split method to split given string by <see cref="Environment.NewLine" />.
        /// </summary>
        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        /// <summary>
        ///     Uses string.Split method to split given string by <see cref="Environment.NewLine" />.
        /// </summary>
        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        /// <summary>
		///     Uses string.Split method to split given string by <see cref="Environment.NewLine" />.
		/// </summary>
		public static string[] Explode(this string str)
        {
            return str.Split(",");
        }

        /// <summary>
        ///     Converts PascalCase string to camelCase string.
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <param name="invariantCulture">Invariant culture</param>
        /// <returns>camelCase of the string</returns>
        public static string ToCamelCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return invariantCulture ? str.ToLowerInvariant() : str.ToLower();
            }

            return (invariantCulture ? char.ToLowerInvariant(str[0]) : char.ToLower(str[0])) + str.Substring(1);
        }

        /// <summary>
        ///     Converts PascalCase string to camelCase string in specified culture.
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <param name="culture">An object that supplies culture-specific casing rules</param>
        /// <returns>camelCase of the string</returns>
        public static string ToCamelCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToLower(culture);
            }

            return char.ToLower(str[0], culture) + str.Substring(1);
        }

        /// <summary>
        ///     Converts given PascalCase/camelCase string to sentence (by splitting words by space).
        ///     Example: "ThisIsSampleSentence" is converted to "This is a sample sentence".
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <param name="invariantCulture">Invariant culture</param>
        public static string ToSentenceCase(this string str, string replChar = " ", bool invariantCulture = false)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return Regex.Replace(
                str,
                "[a-z][A-Z]",
                m => m.Value[0] + replChar + (invariantCulture ? char.ToLowerInvariant(m.Value[1]) : char.ToLower(m.Value[1]))
            );
        }

        /// <summary>
        ///     Converts given PascalCase/camelCase string to sentence (by splitting words by space).
        ///     Example: "ThisIsSampleSentence" is converted to "This is a sample sentence".
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <param name="culture">An object that supplies culture-specific casing rules.</param>
        public static string ToSentenceCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1], culture));
        }

        /// <summary>
        ///     Converts string to enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        ///     Converts string to enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <param name="ignoreCase">Ignore case</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static string ToMd5(this string str)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(str);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        ///     Converts camelCase string to PascalCase string.
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <param name="invariantCulture">Invariant culture</param>
        /// <returns>PascalCase of the string</returns>
        public static string ToPascalCase(this string str, CultureInfo culture, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return invariantCulture ? str.ToUpperInvariant() : str.ToUpper();
            }

            return (invariantCulture ? char.ToUpperInvariant(str[0]) : char.ToUpper(str[0])) + str.Substring(1);
        }

        /// <summary>
        ///     Converts camelCase string to PascalCase string in specified culture.
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <param name="culture">An object that supplies culture-specific casing rules</param>
        /// <returns>PascalCase of the string</returns>
        public static string ToPascalCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToUpper(culture);
            }

            return char.ToUpper(str[0], culture) + str.Substring(1);
        }
        public static string ToPascalCaseWithUndescore(this string input)
        {
            var arry = input.ToCharArray();
            var sb = new StringBuilder();
            foreach (var ch in arry)
            {
                if (char.IsUpper(ch) && sb.Length > 0)
                {
                    sb.Append('_');
                }
                sb.Append(ch);
            }

            return sb.ToString();

        }

        public static string ToPascalCase(this string name, bool removeSep = false)
        {
            string notStarting_SC = "";
            //    if (_tablePrefixRemoval != null)
            //        notStarting_SC = Regex.Replace(name, _tablePrefixRemoval, _tablePrefixSubstitue);
            //    else
            notStarting_SC = name;
            string notStartingAlpha = Regex.Replace(notStarting_SC, "^[^a-zA-Z]+", "");
            string workingString = ToLowerExceptCamelCase(notStartingAlpha);
            workingString = RemoveSeparatorAndCapNext(workingString, removeSep);
            return workingString;
        }
        public static string RemoveSeparatorAndCapNext(string input, bool removeSep = false)
        {
            string dashUnderscore = "-_";
            string workingString = input;
            char[] chars = workingString.ToCharArray();
            int under = workingString.IndexOfAny(dashUnderscore.ToCharArray());
            while (under > -1)
            {
                chars[under + 1] = Char.ToUpper(chars[under + 1], CultureInfo.InvariantCulture);
                workingString = new System.String(chars);
                under = workingString.IndexOfAny(dashUnderscore.ToCharArray(), under + 1);
            }
            chars[0] = Char.ToUpper(chars[0], CultureInfo.InvariantCulture);
            workingString = new string(chars);
            return removeSep ? Regex.Replace(workingString, "[-_]", "") : workingString;
        }
        public static string ToLowerExceptCamelCase(string input)
        {
            char[] chars = input.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int left = (i > 0 ? i - 1 : i);
                int right = (i < chars.Length - 1 ? i + 1 : i);
                if (i != left && i != right)
                {
                    if (Char.IsUpper(chars[i]) && Char.IsLetter(chars[left]) && Char.IsUpper(chars[left]))
                    {
                        chars[i] = Char.ToLower(chars[i], CultureInfo.InvariantCulture);
                    }
                    else if (Char.IsUpper(chars[i]) && Char.IsLetter(chars[right]) && Char.IsUpper(chars[right]))
                    {
                        chars[i] = Char.ToLower(chars[i], CultureInfo.InvariantCulture);
                    }
                    else if (Char.IsUpper(chars[i]) && !Char.IsLetter(chars[right]))
                    {
                        chars[i] = Char.ToLower(chars[i], CultureInfo.InvariantCulture);
                    }
                }
            }
            chars[chars.Length - 1] = Char.ToLower(chars[chars.Length - 1], CultureInfo.InvariantCulture);
            return new string(chars);
        }

        /// <summary>
        ///     Gets a substring of a string from beginning of the string if it exceeds maximum length.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
        public static string Truncate(this string str, int maxLength)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            return str.Left(maxLength);
        }

        /// <summary>
        ///     Gets a substring of a string from beginning of the string if it exceeds maximum length.
        ///     It adds a "..." postfix to end of the string if it's truncated.
        ///     Returning string can not be longer than maxLength.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
        public static string TruncateWithPostfix(this string str, int maxLength)
        {
            return TruncateWithPostfix(str, maxLength, "...");
        }

        /// <summary>
        ///     Gets a substring of a string from beginning of the string if it exceeds maximum length.
        ///     It adds given <paramref name="postfix" /> to end of the string if it's truncated.
        ///     Returning string can not be longer than maxLength.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str" /> is null</exception>
        public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty || maxLength == 0)
            {
                return string.Empty;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            if (maxLength <= postfix.Length)
            {
                return postfix.Left(maxLength);
            }

            return str.Left(maxLength - postfix.Length) + postfix;
        }
        public static string Repeat(this string input,int value)
        {
           return string.Concat(Enumerable.Repeat(input, value));
        }
        public static string RemoveChars(this string input, params char[] chars)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (!chars.Contains(input[i]))
                    sb.Append(input[i]);
            }
            return sb.ToString();
        }

    }
}
