using System.Collections.Generic;
using Orion.Framework.Domains;
using Orion.Framework.Validations.Aspects;

namespace Orion.Framework.DataLayer.Stores.Operations {
   
    public interface IAdd<in TEntity, in TKey> where TEntity : class, IKey<TKey> {
      
        void Add( [Valid] TEntity entity );

        void AddOrUpdate([Valid] TEntity entity);
        void Add( [Valid] IEnumerable<TEntity> entities );


    }
}