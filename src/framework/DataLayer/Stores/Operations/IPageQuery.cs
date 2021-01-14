using System.Collections.Generic;

using Orion.Framework.Domains;
using Orion.Framework.Domains.Repositories;

namespace Orion.Framework.DataLayer.Stores.Operations {

    public interface IPageQuery<TEntity, in TKey> where TEntity : class, IKey<TKey> {
      
        List<TEntity> Query( IQueryBase<TEntity> query );
    
     
        PagerList<TEntity> PagerQuery( IQueryBase<TEntity> query );
       
       
    }
}