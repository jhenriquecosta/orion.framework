using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Orion.Framework.Utilities
{
    /// <summary>
    /// Provides a series of static methods used to implement 'design-by-contract'.
    /// </summary>
    public static class Check
    {
        #region Public Methods

        /// <summary>
        /// Tests whether the given <paramref name="intValue"/> is in the list of integers in <paramref name="valueList"/>.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="intValue">The int value.</param>
        /// <param name="valueList">The value list.</param>
        /// <param name="propertyExpression">The property expression.</param>
        public static void IsInList<TProperty> ( int? intValue, IEnumerable<int> valueList, Expression<Func<TProperty>> propertyExpression )
        {
            var propertyName = PropertyHelper.ExtractPropertyName ( propertyExpression );

            if ( intValue.HasValue && !valueList.Contains ( intValue.Value ) )
            {
                throw new ArgumentException (
                    string.Format (
                        "{0} is does not have a valid value. Valid values are {1}.",
                        propertyName.SeparatePascalCaseWords (),
                        valueList.ToDelimitedString ( "," ) ) );
            }
        }

        /// <summary>
        /// Tests whether the given <paramref name="intValue"/> is between <paramref name="startValue"/> and <paramref name="endValue"/>.  If it
        /// is not then an <see cref="ArgumentException"/> is thrown.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="intValue">The int value.</param>
        /// <param name="startValue">The start value.</param>
        /// <param name="endValue">The end value.</param>
        /// <param name="propertyExpression">The property expression.</param>
        public static void IsInRange<TProperty> ( int? intValue, int startValue, int endValue, Expression<Func<TProperty>> propertyExpression )
        {
            var propertyName = PropertyHelper.ExtractPropertyName ( propertyExpression );

            if ( intValue.HasValue && ( intValue < startValue || intValue > endValue ) )
            {
                throw new ArgumentException (
                    string.Format (
                        "{0} is not in valid range. Valid range is {1} to {2}.", propertyName.SeparatePascalCaseWords (), startValue, endValue ) );
            }
        }

        /// <summary>
        /// Asserts that the given object 'is not null'.  If the object is null then an <see cref="ArgumentException"/> is thrown.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="obj">The object that is being compared to null.</param>
        /// <param name="propertyExpression">An expression that should return a property.  This is used in the text of the thrown exception to identify the name of
        /// a property that would have been set to the given object value.</param>
        /// <exception cref="ArgumentException">Thrown if the object is equal to null.</exception>
        public static void IsNotNull<TProperty> ( object obj, Expression<Func<TProperty>> propertyExpression )
        {
            var propertyName = PropertyHelper.ExtractPropertyName ( propertyExpression );
            if ( obj == null )
            {
                throw new ArgumentException ( string.Format ( "{0} is required.", propertyName.SeparatePascalCaseWords () ), propertyName );
            }
        }

        /// <summary>
        /// Asserts that the given object 'is not null'.  If the object is null then an <see cref="ArgumentException"/> is thrown.
        /// </summary>
        /// <param name="obj">The object that is being compared to null.</param>
        /// <param name="message">The message that will be added to the exception if thrown.</param>
        /// <exception cref="ArgumentException">Thrown if the object is equal to null.</exception>
        public static void IsNotNull ( object obj, string message )
        {
            if ( obj == null )
            {
                throw new ArgumentException ( message );
            }
        }

        /// <summary>
        /// Asserts that the given object 'is not null'.  If the object is null then an <see cref="ArgumentException"/> is thrown; otherwise it returns the non-null value.
        /// </summary>
        /// <typeparam name="T">The type of the object being checked.</typeparam>
        /// <param name="obj">The object that is being compared to null.</param>
        /// <param name="message">The message that will be added to the exception if thrown.</param>
        /// <returns>The non-null value of the object being checked.</returns>
        /// <exception cref="ArgumentException">Thrown if the object is equal to null.</exception>
        public static T IsNotNullAndAssign<T> ( T obj, string message ) where T : class
        {
            if ( obj == null )
            {
                throw new ArgumentException ( message );
            }
            return obj;
        }

        /// <summary>
        /// Asserts that the given string is not equal to null and does not contain 'all whitespace'.
        /// If this is not true then an <see cref="ArgumentException"/> is thrown.
        /// </summary>
        /// <typeparam name="TProperty">The type of property returned by the expression.</typeparam>
        /// <param name="str">The given string.</param>
        /// <param name="propertyExpression">An expression that should return a property.  This is used in the text of the thrown exception to identify the name of
        /// a property that would have been set to the given object value.</param>
        /// <exception cref="ArgumentException">Thrown if the given string is null or contains nothing but whitespace.</exception>
        public static void IsNotNullOrWhitespace<TProperty> ( string str, Expression<Func<TProperty>> propertyExpression )
        {
            var propertyName = PropertyHelper.ExtractPropertyName ( propertyExpression );
            if ( string.IsNullOrWhiteSpace ( str ) )
            {
                throw new ArgumentException ( string.Format ( "{0} is required.", propertyName.SeparatePascalCaseWords () ), propertyName );
            }
        }

        /// <summary>
        /// Asserts that the given string is not equal to null and does not contain 'all whitespace'.
        /// If this is not true then an <see cref="ArgumentException"/> is thrown.
        /// </summary>
        /// <param name="str">The given string.</param>
        /// <param name="message">The message that will be added to the exception if thrown.</param>
        /// <exception cref="ArgumentException">Thrown if the given string is null or contains nothing but whitespace.</exception>
        public static void IsNotNullOrWhitespace ( string str, string message )
        {
            if ( string.IsNullOrWhiteSpace ( str ) )
            {
                throw new ArgumentException ( message );
            }
        }

        /// <summary>
        /// Asserts that the given string is not equal to null and does not contain 'all whitespace'.
        /// If this is not true then an <see cref="ArgumentException"/> is thrown; otherwise, it returs the given string.
        /// </summary>
        /// <param name="str">The given string.</param>
        /// <param name="message">The message that will be added to the exception if thrown.</param>
        /// <returns>The string.</returns>
        /// <exception cref="ArgumentException">Thrown if the given string is null or contains nothing but whitespace.</exception>
        public static string IsNotNullOrWhitespaceAndAssign ( string str, string message )
        {
            if ( string.IsNullOrWhiteSpace ( str ) )
            {
                throw new ArgumentException ( message );
            }
            return str;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="longValue"/> is zero.
        /// </summary>
        /// <param name="longValue">The int value.</param>
        /// <param name="message">The message.</param>
        public static void IsZero ( long longValue, string message )
        {
            if ( longValue == 0 )
            {
                throw new ArgumentException ( message );
            }
        }

        #endregion

        #region NEWS METHODS
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(T value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (value == null)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(
            [NoEnumeration] T value,
            [InvokerParameterName] [NotNull] string parameterName,
            [NotNull] string propertyName)
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));
                NotEmpty(propertyName, nameof(propertyName));

                throw new ArgumentException(propertyName + " " + parameterName + " argument property is null!");
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotNullOrWhiteSpace(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (value.IsNullOrWhiteSpace())
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NullButNotEmpty(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (!ReferenceEquals(value, null) && value.Length == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName + "is empty!", parameterName);
            }

            return value;
        }

        public static IReadOnlyList<T> HasNoNulls<T>(IReadOnlyList<T> value, [InvokerParameterName] [NotNull] string parameterName)
            where T : class
        {
            NotNull(value, parameterName);

            if (value.Any(e => e == null))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName);
            }

            return value;
        }

        public static T IsDefined<T>(T value, [InvokerParameterName] [NotNull] string parameterName)
            where T : struct
        {
            if (!Enum.IsDefined(typeof(T), value))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName + " is an invalid Enum!", parameterName);
            }

            return value;
        }

        public static Type ValidEntityType(Type value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (!value.GetTypeInfo().IsClass)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName + " is an invalid entity type!", parameterName);
            }

            return value;
        }

        [ContractAnnotation("value:null => halt")]
        public static string NotEmpty(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            Exception exception = null;
            if (ReferenceEquals(value, null))
            {
                exception = new ArgumentNullException(parameterName);
            }
            else if (value.Trim().Length == 0)
            {
                exception = new ArgumentException(parameterName + " can not be empty!", parameterName);
            }

            if (exception != null)
            {
                NotEmpty(parameterName, nameof(parameterName));

                // ReSharper disable once UnthrowableException
                throw exception;
            }

            return value;
        }
        #endregion
        
    }
}
