using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
   
    public interface IFindByIdsAsync<TEntity, in TKey> where TEntity : class, IKey<TKey> {
       
        Task<List<TEntity>> FindByIdsAsync( params TKey[] ids );
       
        Task<List<TEntity>> FindByIdsAsync( IEnumerable<TKey> ids, CancellationToken cancellationToken = default( CancellationToken ) );
        
        Task<List<TEntity>> FindByIdsAsync( string ids );
    }
}