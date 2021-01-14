using System;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Framework.Domains {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class KeyListComparator<TKey> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newList"></param>
        /// <param name="oldList"></param>
        public KeyListCompareResult<TKey> Compare( IEnumerable<TKey> newList, IEnumerable<TKey> oldList ) {
            if( newList == null )
                throw new ArgumentNullException( nameof( newList ) );
            if( oldList == null )
                throw new ArgumentNullException( nameof( oldList ) );
            var newEntities = newList.ToList();
            var oldEntities = oldList.ToList();
            var createList = GetCreateList( newEntities, oldEntities );
            var updateList = GetUpdateList( newEntities, oldEntities );
            var deleteList = GetDeleteList( newEntities, oldEntities );
            return new KeyListCompareResult<TKey>( createList, updateList, deleteList );
        }

        /// <summary>
        /// 
        /// </summary>
        private List<TKey> GetCreateList( List<TKey> newList, List<TKey> oldList ) {
            var result = newList.Except( oldList );
            return result.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        private List<TKey> GetUpdateList( List<TKey> newList, List<TKey> oldList ) {
            return newList.FindAll( id => oldList.Exists( t => t.Equals( id ) ) );
        }

        /// <summary>
        /// 
        /// </summary>
        private List<TKey> GetDeleteList( List<TKey> newList, List<TKey> oldList ) {
            var result = oldList.Except( newList );
            return result.ToList();
        }
    }
}
