using System;
using System.Linq.Expressions;

using DapperExtensions;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Dapper.Filters.Query
{
    public interface IDapperQueryFilter
    {
        string FilterName { get; }

        bool IsEnabled { get; }

        IFieldPredicate ExecuteFilter<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>;

        Expression<Func<TEntity, bool>> ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity<TPrimaryKey>;
    }
}
