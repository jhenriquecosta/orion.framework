using System.Collections.Generic;
using Orion.Framework.Domains;
using Orion.Framework.Validations.Aspects;

namespace Orion.Framework.DataLayer.Stores.Operations {
  
    public interface IUpdate<in TEntity, in TKey> where TEntity : class, IKey<TKey> {
      
        void Update( [Valid] TEntity entity );
      
        void Update( [Valid] IEnumerable<TEntity> entities );
    }
}