using System;
using System.Linq.Expressions;

using DapperExtensions;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Dapper.Filters.Query
{
    public interface IDapperQueryFilterExecuter
    {
        IPredicate ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity<TPrimaryKey>;

        PredicateGroup ExecuteFilter<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>;
    }
}
