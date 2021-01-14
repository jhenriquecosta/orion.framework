using System.Collections.Generic;
using Orion.Framework.Domains;
using Orion.Framework.Validations.Aspects;

namespace Orion.Framework.DataLayer.Stores.Operations {
   
    public interface IInsertOrUpdate<TEntity, in TKey> where TEntity : class, IKey<TKey> {

        void InsertOrUpdate( [Valid] TEntity entity );
        void InsertOrUpdate( [Valid] IEnumerable<TEntity> entities );
        TEntity InsertOrUpdateAndGetId([Valid] TEntity entity);

    }
}