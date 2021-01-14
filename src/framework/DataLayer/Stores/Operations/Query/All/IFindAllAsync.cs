using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
   
    public interface IFindAllAsync<TEntity, in TKey> where TEntity : class, IKey<TKey> {
       
        Task<List<TEntity>> FindAllAsync( Expression<Func<TEntity, bool>> predicate = null );
    }
}