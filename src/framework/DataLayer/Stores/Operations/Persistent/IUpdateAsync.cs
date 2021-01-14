using System.Collections.Generic;
using System.Threading.Tasks;
using Orion.Framework.Domains;
using Orion.Framework.Validations.Aspects;

namespace Orion.Framework.DataLayer.Stores.Operations {
   
    public interface IUpdateAsync<in TEntity, in TKey> where TEntity : class, IKey<TKey> {
    
        Task UpdateAsync( [Valid] TEntity entity );
        Task MergeAsync([Valid] TEntity entity);

        Task UpdateAsync( [Valid] IEnumerable<TEntity> entities );
    }
}