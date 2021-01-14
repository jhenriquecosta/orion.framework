using System.Threading;
using System.Threading.Tasks;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
   
    public interface IFindByIdAsync<TEntity, in TKey> where TEntity : class, IKey<TKey> {
        Task<TEntity> FindAsync(object id,bool detached=false, CancellationToken cancellationToken = default(CancellationToken));
    }
}