using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Orion.Framework.Domains;
using Orion.Framework.Domains.Repositories;

namespace Orion.Framework.DataLayer.Stores.Operations {
  
    public interface IFindQueryable<TEntity, in TKey> where TEntity : class, IKey<TKey> {

       
        IQueryable<TEntity> FindAsNoTracking();

        IQueryable<TEntity> Find();
        IQueryable<TEntity> Find(ICriteria<TEntity> criteria);
       
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);


    }
}
