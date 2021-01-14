using System.Collections.Generic;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {

    public interface IRemove<in TEntity, in TKey> where TEntity : class, IKey<TKey> {
     
        void Remove( object id );
     
        void Remove( TEntity entity );
      
        void Remove( IEnumerable<TKey> ids );
     
        void Remove( IEnumerable<TEntity> entities );
    }
}