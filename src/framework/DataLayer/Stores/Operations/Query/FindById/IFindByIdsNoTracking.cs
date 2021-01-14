using System.Collections.Generic;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IFindByIdsNoTracking<TEntity, in TKey> where TEntity : class, IKey<TKey> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        List<TEntity> FindByIdsNoTracking( params TKey[] ids );
        /// <summary>
        /// ,
        /// </summary>
        /// <param name="ids"></param>
        List<TEntity> FindByIdsNoTracking( IEnumerable<TKey> ids );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids">，："1,2"</param>
        List<TEntity> FindByIdsNoTracking( string ids );
    }
}
