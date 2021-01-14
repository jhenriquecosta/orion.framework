using System.Collections.Generic;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
   
    public interface IFindByIds<TEntity, in TKey> where TEntity : class, IKey<TKey> {
      
        List<TEntity> FindByIds( params TKey[] ids );
     
        List<TEntity> FindByIds( IEnumerable<TKey> ids );
      
        List<TEntity> FindByIds( string ids );
    }
}
