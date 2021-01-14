using System;
using System.Linq.Expressions;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
  
    public interface ISingle<TEntity, in TKey> where TEntity : class, IKey<TKey> {
      
        TEntity Single( Expression<Func<TEntity, bool>> predicate );
    }
}