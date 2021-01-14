using System.Collections.Generic;

namespace Orion.Framework.Domains {
    /// <summary>
    /// 
    /// </summary>
    public class KeyListCompareResult<TKey> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createList"></param>
        /// <param name="updateList"></param>
        /// <param name="deleteList"></param>
        public KeyListCompareResult( List<TKey> createList, List<TKey> updateList, List<TKey> deleteList ) {
            CreateList = createList;
            UpdateList = updateList;
            DeleteList = deleteList;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<TKey> CreateList { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<TKey> UpdateList { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<TKey> DeleteList { get; }
    }
}
