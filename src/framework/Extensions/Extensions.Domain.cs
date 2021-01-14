using System;
using System.Collections.Generic;
using Orion.Framework.Domains;

namespace Orion.Framework {
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="newList"></param>
        /// <param name="oldList"></param>
        public static ListCompareResult<TEntity, Guid> Compare<TEntity>( this IEnumerable<TEntity> newList, IEnumerable<TEntity> oldList )
            where TEntity : IKey<Guid> {
            return Compare<TEntity,Guid>( newList, oldList );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey">>
        /// <param name="newList"></param>
        /// <param name="oldList"></param>
        public static ListCompareResult<TEntity, TKey> Compare<TEntity, TKey>( this IEnumerable<TEntity> newList, IEnumerable<TEntity> oldList )
            where TEntity : IKey<TKey> {
            var comparator = new ListComparator<TEntity, TKey>();
            return comparator.Compare( newList, oldList );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newList"></param>
        /// <param name="oldList"></param>
        public static KeyListCompareResult<Guid> Compare( this IEnumerable<Guid> newList, IEnumerable<Guid> oldList ) {
            var comparator = new KeyListComparator<Guid>();
            return comparator.Compare( newList, oldList );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newList"></param>
        /// <param name="oldList"></param>
        public static KeyListCompareResult<string> Compare( this IEnumerable<string> newList, IEnumerable<string> oldList ) {
            var comparator = new KeyListComparator<string>();
            return comparator.Compare( newList, oldList );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newList"></param>
        /// <param name="oldList"></param>
        public static KeyListCompareResult<int> Compare( this IEnumerable<int> newList, IEnumerable<int> oldList ) {
            var comparator = new KeyListComparator<int>();
            return comparator.Compare( newList, oldList );
        }

        /// <summary>
        /// </summary>
        /// <param name="newList"></param>
        /// <param name="oldList"></param>
        public static KeyListCompareResult<long> Compare( this IEnumerable<long> newList, IEnumerable<long> oldList ) {
            var comparator = new KeyListComparator<long>();
            return comparator.Compare( newList, oldList );
        }
    }
}
