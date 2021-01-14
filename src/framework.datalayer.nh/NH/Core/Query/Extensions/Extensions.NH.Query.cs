using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NHibernate.Metadata;
using NHibernate.SqlCommand;
using Orion.Framework.DataLayer.NH.Helpers;
using Orion.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Expression = System.Linq.Expressions.Expression;

namespace Orion.Framework
{

    /// <summary>
    /// CriteriaExtensions class.
    /// </summary>
    public static class NHExtensions
    {
        #region Public CRITERIA

        /// <summary>
        /// Sets the fetch mode.
        /// </summary>
        /// <typeparam name="T">Type setting fetch mode for.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="mode">The mode to set.</param>
        /// <returns>A <see cref="ICriteria"/></returns>
        public static ICriteria SetFetchMode<T, TProperty> (
            this ICriteria criteria, Expression<Func<T, TProperty>> associationPropertyExpression, FetchMode mode )
        {
            var associationPath = PropertyHelper.ExtractPropertyName ( associationPropertyExpression );
            criteria.Fetch ( associationPath );
            return criteria;
        }

        #endregion

        #region Public DETACHEDCRITERIA

        /// <summary>
        /// Adds the orders.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="orders">The orders.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria AddOrders(this DetachedCriteria criteria, IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                criteria.AddOrder(order);
            }

            return criteria;
        }

        /// <summary>
        /// Creates the criteria.
        /// </summary>
        /// <typeparam name="T">Type creating criteria for.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="joinType">Type of the join.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria CreateCriteria<T, TProperty>(
            this DetachedCriteria criteria, Expression<Func<T, TProperty>> associationPropertyExpression, JoinType joinType)
        {
            var associationPath = PropertyHelper.ExtractPropertyName(associationPropertyExpression);
            criteria.CreateCriteria(associationPath, joinType);
            return criteria;
        }

        /// <summary>
        /// Creates the criteria.
        /// </summary>
        /// <typeparam name="T">Type creating criteria for.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="joinType">Type of the join.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria CreateCriteria<T>(
            this DetachedCriteria criteria, Expression<Func<T, object>> associationPropertyExpression, JoinType joinType)
        {
            return CreateCriteria<T, object>(criteria, associationPropertyExpression, joinType);
        }

        /// <summary>
        /// Sets the fetch mode.
        /// </summary>
        /// <typeparam name="T">Type setting fetch mode for.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="mode">The mode to set.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria SetFetchMode<T, TProperty>(
            this DetachedCriteria criteria, Expression<Func<T, TProperty>> associationPropertyExpression, FetchMode mode)
        {
            var associationPath = PropertyHelper.ExtractPropertyName(associationPropertyExpression);
            // criteria.SetFetchMode(associationPath, mode );
            criteria.Fetch(associationPath);
            return criteria;
        }

        /// <summary>
        /// Sets the fetch mode.
        /// </summary>
        /// <typeparam name="T">Type setting fetch mode for.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="mode">The mode to set.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria SetFetchMode<T>(this DetachedCriteria criteria, Expression<Func<T, object>> associationPropertyExpression, FetchMode mode)
        {
            return SetFetchMode<T, object>(criteria, associationPropertyExpression, mode);
        }

        #endregion

        #region Public PROJECTIONS

        /// <summary>
        /// Adds the specified projection list.
        /// </summary>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="projectionList">The projection list.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>A <see cref="ProjectionList"/></returns>
        public static ProjectionList Add<TTo, TProperty>(
            this ProjectionList projectionList,
            IProjection projection,
            Expression<Func<TTo, TProperty>> alias)
        {
            return projectionList.Add(projection, PropertyHelper.ExtractPropertyName(alias));
        }

        /// <summary>
        /// Adds the specified projection list.
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="projectionList">The projection list.</param>
        /// <param name="projectionExpression">The projection expression.</param>
        /// <param name="aliasExpression">The alias expression.</param>
        /// <returns>A <see cref="ProjectionList"/></returns>
        public static ProjectionList Add<TFrom, TTo, TProperty>(
            this ProjectionList projectionList,
            Expression<Func<TFrom, object>> projectionExpression,
            Expression<Func<TTo, TProperty>> aliasExpression)
        {
            var projection = Projections.Property(projectionExpression);
            return projectionList.Add(projection, PropertyHelper.ExtractPropertyName(aliasExpression));
        }

        /// <summary>
        /// Adds the specified projection list.
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="projectionList">The projection list.</param>
        /// <param name="projectionExpression">The projection expression.</param>
        /// <param name="aliasExpression">The alias expression.</param>
        /// <returns>A <see cref="ProjectionList"/></returns>
        public static ProjectionList Add<TFrom, TTo, TProperty>(
            this ProjectionList projectionList,
            Expression<Func<TFrom, TProperty>> projectionExpression,
            Expression<Func<TTo, TProperty>> aliasExpression)
        {
            var projection =
                Projections.Property(ExpressionUtil.AddBox(projectionExpression));
            return projectionList.Add(projection, PropertyHelper.ExtractPropertyName(aliasExpression));
        }

        #endregion

        #region EAGERFETCH
        public static IQueryable<TEntity> EagerFetchAll<TEntity>(this IQueryable<TEntity> query)
        {
            // hack the session reference out of the provider - or is
            // there a better way to do this?
            //IStatelessSession session = (IStatelessSession)query.Provider.GetType().GetProperty("Session").GetValue(query.Provider);

            //ISession session = (ISession)query.Provider.GetType()
            //                             .GetProperty("Session", System.Reflection.BindingFlags.Instance
            //                                                   | System.Reflection.BindingFlags.NonPublic)
            //                             .GetValue(query.Provider);

            var entityType = typeof(TEntity);
            var sessionFactory = Ioc.Create<ISessionFactory>();
            IClassMetadata metaData = sessionFactory.GetClassMetadata(entityType);

            for (int i = 0; i < metaData.PropertyNames.Length; i++)
            {
                global::NHibernate.Type.IType propType = metaData.PropertyTypes[i];

                // get eagerly mapped associations to other entities
                if (propType.IsAssociationType && !metaData.PropertyLaziness[i])
                {

                    ParameterExpression par = Expression.Parameter(entityType, "p");

                    Expression propExp = Expression.Property(par, metaData.PropertyNames[i]);



                    Type relatedType = null;
                    LambdaExpression lambdaExp;
                    string methodName;
                    if (propType.ReturnedClass.IsGenericCollection())
                    {
                        relatedType = propType.ReturnedClass.GetGenericArguments()[0];
                        var funcType = typeof(Func<,>).MakeGenericType(entityType, typeof(IEnumerable<>).MakeGenericType(relatedType));
                        lambdaExp = Expression.Lambda(funcType, propExp, par);
                        methodName = "FetchMany";
                    }
                    else
                    {
                        relatedType = propType.ReturnedClass;
                        lambdaExp = Expression.Lambda(propExp, par);
                        methodName = "Fetch";
                    }

                    var fetchManyMethodImpl = typeof(EagerFetchingExtensionMethods).GetMethod(methodName).MakeGenericMethod(entityType, relatedType);
                    Expression callExpr = Expression.Call(null,
                        fetchManyMethodImpl,
                        // first parameter is the query, second is property access expression
                        query.Expression, lambdaExp
                    );

                    LambdaExpression expr = Expression.Lambda(callExpr, par);
                    Type fetchGenericType = typeof(NhFetchRequest<,>).MakeGenericType(entityType, propType.ReturnedClass);
                    query = (IQueryable<TEntity>)Activator.CreateInstance(fetchGenericType, query.Provider, callExpr);
                }
            }

            return query;
        }

        public static ICriteria FetchAllProperties<TEntity>(this ICriteria criteria, ISessionFactory sessionFactory)
        {
            var entityType = typeof(TEntity);
            var metaData = sessionFactory.GetClassMetadata(entityType);
            for (var i = 0; i < metaData.PropertyNames.Length; i++)
            {
                var propType = metaData.PropertyTypes[i];
                // get eagerly mapped associations to other entities
                if (propType.IsAssociationType && !metaData.PropertyLaziness[i])
                {
                    //criteria = criteria.SetFetchMode(propType.Name, FetchMode.Eager);
                    criteria = criteria.Fetch(SelectMode.JoinOnly, propType.Name);
                }
            }

            return criteria;
        }
        #endregion
        #region PAGINATION
        public static IList<dynamic> DynamicList(this IQuery query)
        {
            return query.SetResultTransformer(NHResultTransformerToObjectExpando.ExpandoObject)
                        .List<dynamic>();
        }

        public static IEnumerable<T> Page<T>(this IEnumerable<T> en, int pageSize, int page)
        {
            return en.Skip(page * pageSize).Take(pageSize);
        }
        public static IQueryable<T> Page<T>(this IQueryable<T> en, int pageSize, int page)
        {
            return en.Skip(page * pageSize).Take(pageSize);
        }
        public static IQueryOver<troot, tsubtype> Or<troot, tsubtype>(this IQueryOver<troot, tsubtype> input, params ICriterion[] criteria)
        {
            switch (criteria.Length)
            {
                case 0:
                    return input;
                case 1:
                    return input.Where(criteria[0]);
                default:
                    {
                        var or = Restrictions.Or(criteria[0], criteria[1]);
                        for (int i = 2; i < criteria.Length; i++)
                            or = Restrictions.Or(or, criteria[i]);

                        return input.Where(or);
                    }
            }
        }
      

        #endregion
    }
}
