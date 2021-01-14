using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IFindByIdsNoTrackingAsync<TEntity, in TKey> where TEntity : class, IKey<TKey> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        Task<List<TEntity>> FindByIdsNoTrackingAsync( params TKey[] ids );
        /// <summary>
        /// ,
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        Task<List<TEntity>> FindByIdsNoTrackingAsync( IEnumerable<TKey> ids, CancellationToken cancellationToken = default( CancellationToken ) );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids">，："1,2"</param>
        Task<List<TEntity>> FindByIdsNoTrackingAsync( string ids );
    }
}
