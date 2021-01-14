using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using DapperExtensions;

using Orion.Framework.DataLayer.Dapper.Expressions;
using Orion.Framework.Dependency;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Dapper.Filters.Query
{
    public class DapperQueryFilterExecuter : IDapperQueryFilterExecuter, ITransientDependency
    {
        private readonly IEnumerable<IDapperQueryFilter> queryFilters;

        public DapperQueryFilterExecuter(IEnumerable<IDapperQueryFilter> queryFilters)
        {
            this.queryFilters = queryFilters;
        }

        public IPredicate ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity<TPrimaryKey>
        {
            ICollection<IDapperQueryFilter> filters = queryFilters.ToList();

            foreach (IDapperQueryFilter filter in filters)
            {
                predicate = filter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            }

            IPredicate pg = predicate.ToPredicateGroup<TEntity, TPrimaryKey>();
            return pg;
        }

        public PredicateGroup ExecuteFilter<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>
        {
            ICollection<IDapperQueryFilter> filters = queryFilters.ToList();
            var groups = new PredicateGroup
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
            };

            foreach (IDapperQueryFilter filter in filters)
            {
                IFieldPredicate predicate = filter.ExecuteFilter<TEntity, TPrimaryKey>();
                if (predicate != null)
                {
                    groups.Predicates.Add(predicate);
                }
            }

            return groups;
        }
    }
}
