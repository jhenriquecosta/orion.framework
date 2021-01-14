using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
   
    public interface IFindAll<TEntity, in TKey> where TEntity : class, IKey<TKey>
    {
      
        List<TEntity> FindAll( Expression<Func<TEntity, bool>> predicate = null );
    }
}