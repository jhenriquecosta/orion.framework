using System;
using System.Linq.Expressions;

namespace Orion.Framework.Domains.Repositories
{
  
    public interface ICriteria<TEntity>
    {
        Expression<Func<TEntity, bool>> GetPredicate();
    }
}
