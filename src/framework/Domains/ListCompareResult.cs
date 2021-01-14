using System.Collections.Generic;

namespace Orion.Framework.Domains {
    /// <summary>
    /// 
    /// </summary>
    public class ListCompareResult<TEntity, TKey> where TEntity : IKey<TKey> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createList"></param>
        /// <param name="updateList"></param>
        /// <param name="deleteList"></param>
        public ListCompareResult( List<TEntity> createList, List<TEntity> updateList, List<TEntity> deleteList ) {
            CreateList = createList;
            UpdateList = updateList;
            DeleteList = deleteList;
        }

        /// <summary
        /// 
        /// </summary>
        public List<TEntity> CreateList { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<TEntity> UpdateList { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<TEntity> DeleteList { get; }
    }
}
