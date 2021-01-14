using System.Collections.Generic;
using System.Threading.Tasks;
using Orion.Framework.Domains;
using Orion.Framework.Validations.Aspects;

namespace Orion.Framework.DataLayer.Stores.Operations {

    public interface IInsertOrUpdateAsync<TEntity, in TKey> where TEntity : class, IKey<TKey>
    {

        Task InsertOrUpdateAsync([Valid] TEntity entity);
        Task InsertOrUpdateAsync([Valid] IEnumerable<TEntity> entities);
        Task<TEntity> InsertOrUpdateAndGetIdAsync([Valid] TEntity entity);

    }

}