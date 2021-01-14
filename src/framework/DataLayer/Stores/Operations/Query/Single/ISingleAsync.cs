using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
  
    public interface ISingleAsync<TEntity, in TKey> where TEntity : class, IKey<TKey> {
     
        Task<TEntity> SingleAsync( Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default( CancellationToken ) );
    }
}