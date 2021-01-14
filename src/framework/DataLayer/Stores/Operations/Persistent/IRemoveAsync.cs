using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
  
    public interface IRemoveAsync<in TEntity, in TKey> where TEntity : class, IKey<TKey> {
      
        Task RemoveAsync( object id, CancellationToken cancellationToken = default( CancellationToken ) );
     
        Task RemoveAsync( TEntity entity, CancellationToken cancellationToken = default( CancellationToken ) );
       
        Task RemoveAsync( IEnumerable<TKey> ids, CancellationToken cancellationToken = default( CancellationToken ) );
      
        Task RemoveAsync( IEnumerable<TEntity> entities, CancellationToken cancellationToken = default( CancellationToken ) );
    }
}