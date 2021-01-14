using System.Threading;
using System.Threading.Tasks;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
   
    public interface IFindByIdNoTrackingAsync<TEntity, in TKey> where TEntity : class, IKey<TKey> {
       
        Task<TEntity> FindNoTrackingAsync( TKey id, CancellationToken cancellationToken = default( CancellationToken ) );
    }
}