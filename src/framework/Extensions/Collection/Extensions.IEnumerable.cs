using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Orion.Framework
{
    public static partial class Extensions
    {
        
        public static void Update<T>(this ISet<T> source, Func<T, bool> action, T update)
        {
            var objItem = source.FirstOrDefault(action);
            if (objItem != null)
            {
                source.Remove(objItem);
                source.Add(update);
            }
        }

        public static void Update<T>(this IEnumerable<T> source, Predicate<T> action, T update)
        {
            var index = source.ToList().FindIndex(action);
            if (index != -1)
            {
                source.ToList()[index] = update;
            }
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of the items in <paramref name="source"/> that are distict by the
        /// field returned by the <paramref name="keySelector"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of item stored in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <typeparam name="TKey">The key property.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> of items that are filtered by the method.</param>
        /// <param name="keySelector">A delegate that receives a <typeparamref name="TSource"/> and should return the key property.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the items from <paramref name="source"/> that are distict according
        /// to the property returned by the <paramref name="keySelector"/>.</returns>
        [DebuggerStepThrough]
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var knownKeys = new HashSet<TKey>();
            return source.Where(element => knownKeys.Add(keySelector(element)));
        }

        /// <summary>
        /// Iterates over the elements of the <see cref="IEnumerable{T}"/> and applies the provided <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/>.</param>
        /// <param name="action">The action that should be applied.</param>
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                await Task.Run(() => { action(item); }).ConfigureAwait(false);
        }

        public static Task ForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
            return Task.WhenAll(from partition in Partitioner.Create(source).GetPartitions(dop)
                                select Task.Run(async delegate
                                {
                                    using (partition)
                                        while (partition.MoveNext())
                                            await body(partition.Current).ConfigureAwait(false);
                                }));
        }

        /// <summary>
        /// Iterates over the elements of an <see cref="IEnumerable{T}"/> and creates a single string by
        /// calling <see cref="object.ToString"/> on the elements and separating them with the provided <paramref name="delimiter"/>.
        /// </summary>
        /// <typeparam name="T">Type type of element in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="enumerable">The items to join together.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>A string containing the string representation of the elements in the <see cref="IEnumerable{T}"/> separated by the <paramref name="delimiter"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if given a null <see cref="IEnumerable{T}"/>.
        /// </exception>
        public static string ToDelimitedString<T>(this IEnumerable<T> enumerable, string delimiter)
        {
            if (delimiter == null)
            {
                throw new ArgumentNullException("delimiter");
            }

            var ret = string.Empty;
            var builder = new StringBuilder();
            if (enumerable.Count() > 0)
            {
                foreach (var item in enumerable)
                {
                    builder.Append(item + delimiter);
                }

                ret = builder.ToString();

                // remove last comma
                ret = ret.Substring(0, ret.Length - delimiter.Length);
            }

            return ret;
        }
        public static IEnumerable<T> Flatten<T, R>(this IEnumerable<T> source, Func<T, R> recursion) where R : IEnumerable<T>
        {
            return source.SelectMany(x => (recursion(x) != null && recursion(x).Any()) ? recursion(x).Flatten(recursion) : null)
                         .Where(x => x != null);
        }
        public static IEnumerable<TKey> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return source.GroupBy(selector).Select(x => x.Key);
        }
        private static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>(comparer);
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        #region FWork - News Methods
        /// <summary>
        ///     Concatenates the members of a constructed <see cref="IEnumerable{T}" /> collection of type System.String, using the
        ///     specified separator between each member.
        ///     This is a shortcut for string.Join(...)
        /// </summary>
        /// <param name="source">A collection that contains the strings to concatenate.</param>
        /// <param name="separator">
        ///     The string to use as a separator. separator is included in the returned string only if values
        ///     has more than one element.
        /// </param>
        /// <returns>
        ///     A string that consists of the members of values delimited by the separator string. If values has no members,
        ///     the method returns System.String.Empty.
        /// </returns>
        public static string JoinAsString([NotNull] this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        ///     Concatenates the members of a collection, using the specified separator between each member.
        ///     This is a shortcut for string.Join(...)
        /// </summary>
        /// <param name="source">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">
        ///     The string to use as a separator. separator is included in the returned string only if values
        ///     has more than one element.
        /// </param>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <returns>
        ///     A string that consists of the members of values delimited by the separator string. If values has no members,
        ///     the method returns System.String.Empty.
        /// </returns>
        public static string JoinAsString<T>([NotNull] this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        ///     Filters a <see cref="IEnumerable{T}" /> by given predicate if given condition is true.
        /// </summary>
        /// <param name="source">Enumerable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the enumerable</param>
        /// <returns>Filtered or not filtered enumerable based on <paramref name="condition" /></returns>
        public static IEnumerable<T> WhereIf<T>([NotNull] this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }

        /// <summary>
        ///     Filters a <see cref="IEnumerable{T}" /> by given predicate if given condition is true.
        /// </summary>
        /// <param name="source">Enumerable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the enumerable</param>
        /// <returns>Filtered or not filtered enumerable based on <paramref name="condition" /></returns>
        public static IEnumerable<T> WhereIf<T>([NotNull] this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }
        #endregion
    }
}
       