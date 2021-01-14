using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Orion.Framework
{
    public static partial class Extensions {


        /// <summary>
        /// Deletes the specified item from the item list.
        /// Throws exception if the item is not found in the list.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="itemList">The item list.</param>
        /// <param name="item">The item to be deleted.</param>
        public static void Delete<T>(this IList<T> itemList, T item)
        {
            if (!itemList.Remove(item))
            {
                throw new Exception(string.Format("{0} not found in the list.", item.GetType().Name));
            }
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        ///     Adds an item to the collection if it's not already in the collection.
        /// </summary>
        /// <param name="source">Collection</param>
        /// <param name="item">Item to check and add</param>
        /// <typeparam name="T">Type of the items in the collection</typeparam>
        /// <returns>Returns True if added, returns False if not.</returns>
        public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, string field, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }
    }
}
