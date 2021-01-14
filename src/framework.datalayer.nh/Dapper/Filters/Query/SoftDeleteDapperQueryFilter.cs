using System;
using System.Linq.Expressions;
using System.Reflection;

using DapperExtensions;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Dependency;
using Orion.Framework.Domains;
using Orion.Framework.Settings;
using Orion.Framework.Utilities;

namespace Orion.Framework.DataLayer.Dapper.Filters.Query
{
    public class SoftDeleteDapperQueryFilter : IDapperQueryFilter, ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SoftDeleteDapperQueryFilter(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public bool IsDeleted => false;

        public string FilterName { get; } = XTConstants.Filters.SoftDelete;

        public bool IsEnabled => _unitOfWorkManager.Current.IsFilterEnabled(FilterName);

        public IFieldPredicate ExecuteFilter<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>
        {
            IFieldPredicate predicate = null;
            if (IsFilterable<TEntity, TPrimaryKey>())
            {
                predicate = Predicates.Field<TEntity>(f => (f as ISoftDelete).IsDeleted, Operator.Eq, IsDeleted);
            }

            return predicate;
        }

        public Expression<Func<TEntity, bool>> ExecuteFilter<TEntity, TPrimaryKey>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity<TPrimaryKey>
        {
            if (IsFilterable<TEntity, TPrimaryKey>())
            {
                PropertyInfo propType = typeof(TEntity).GetProperty(nameof(ISoftDelete.IsDeleted));
                if (predicate == null)
                {
                    predicate = ExpressionUtil.MakePredicate<TEntity>(nameof(ISoftDelete.IsDeleted), IsDeleted, propType.PropertyType);
                }
                else
                {
                    ParameterExpression paramExpr = predicate.Parameters[0];
                    MemberExpression memberExpr = Expression.Property(paramExpr, nameof(ISoftDelete.IsDeleted));
                    BinaryExpression body = Expression.AndAlso(
                        predicate.Body,
                        Expression.Equal(memberExpr, Expression.Constant(IsDeleted, propType.PropertyType)));
                    predicate = Expression.Lambda<Func<TEntity, bool>>(body, paramExpr);
                }
            }
            return predicate;
        }

        private bool IsFilterable<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>
        {
	        return typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)) && IsEnabled;
        }
    }
}
