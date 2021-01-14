using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Orion.Framework.Domains;
using Orion.Framework.Validations.Aspects;

namespace Orion.Framework.DataLayer.Stores.Operations {
  
    public interface IAddAsync<in TEntity, in TKey> where TEntity : class, IKey<TKey>
    {
        Task AddAsync( [Valid] TEntity entity, CancellationToken cancellationToken = default( CancellationToken ) );
        Task AddOrUpdateAsync([Valid] TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task AddAsync( [Valid] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default( CancellationToken ) );
    }
}