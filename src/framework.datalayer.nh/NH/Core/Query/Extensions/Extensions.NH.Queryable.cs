using Orion.Framework.DataLayer.Queries.Internal;
using Orion.Framework.Domains.Repositories;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Orion.Framework 
{

    public static partial class Extensions 
    {
        private static MethodInfo orderByMethod;
        private static MethodInfo orderByDescMethod;

        private static MethodInfo OrderBy(System.Type source, System.Type key)
        {
            if (orderByMethod == null)
            {
                orderByMethod = new Func<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(
                    Queryable.OrderBy<object, object>
                ).GetMethodInfo().GetGenericMethodDefinition();
            }
            return orderByMethod.MakeGenericMethod(source, key);
        }

        private static MethodInfo OrderByDescending(System.Type source, System.Type key)
        {
            if (orderByDescMethod == null)
            {
                orderByDescMethod = new Func<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(
                    Queryable.OrderByDescending<object, object>
                ).GetMethodInfo().GetGenericMethodDefinition();
            }
            return orderByDescMethod.MakeGenericMethod(source, key);
        }

        public static IOrderedQueryable<TSource> AddOrderBy<TSource>(
            this IQueryable<TSource> queryable,
            string propertyName,
            bool isAsc
        )
        {
            return isAsc ? OrderBy(queryable, propertyName)
                : OrderByDescending(queryable, propertyName);
        }

        public static IOrderedQueryable<TSource> OrderBy<TSource>(
            this IQueryable<TSource> queryable,
            string propertyName
        )
        {
            var expr = CreatePropertyAccessExpression<TSource>(propertyName);
            var result = queryable.Provider.CreateQuery<TSource>(
                Expression.Call(
                    null,
                    OrderBy(typeof(TSource), expr.ReturnType),
                    queryable.Expression,
                    Expression.Quote(expr)
                )
            );
            return result as IOrderedQueryable<TSource>;
        }

        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(
            this IQueryable<TSource> queryable,
            string propertyName
        )
        {
            var expr = CreatePropertyAccessExpression<TSource>(propertyName);
            var result = queryable.Provider.CreateQuery<TSource>(
                Expression.Call(
                    null,
                    OrderByDescending(typeof(TSource), expr.ReturnType),
                    queryable.Expression,
                    Expression.Quote(expr)
                )
            );
            return result as IOrderedQueryable<TSource>;
        }

        /// <summary>
        /// create lambda : x => x.PropertyName
        /// </summary>
        private static LambdaExpression CreatePropertyAccessExpression<TSource>(
            string propertyName
        )
        {
            var parameter = Expression.Parameter(typeof(TSource), "x");
            var propertyInfo = typeof(TSource).GetProperty(propertyName);
            var funcType = typeof(Func<,>).MakeGenericType(typeof(TSource), propertyInfo.PropertyType);
            var access = Expression.MakeMemberAccess(parameter, propertyInfo);
            var result = Expression.Lambda(funcType, access, parameter);
            return result;
        }


        public static async Task<PagerList<TEntity>> ToPagerListAsync<TEntity>( this IQueryable<TEntity> source, IPager pager ) {
            if( source == null )
                throw new ArgumentNullException( nameof( source ) );
            if( pager == null )
                throw new ArgumentNullException( nameof( pager ) );
            source = await source.PageAsync( pager );
            return new PagerList<TEntity>( pager, await source.ToListAsync() );
        }

       
        public static async Task<IQueryable<TEntity>> PageAsync<TEntity>( this IQueryable<TEntity> source, IPager pager ) {
            if( source == null )
                throw new ArgumentNullException( nameof( source ) );
            if( pager == null )
                throw new ArgumentNullException( nameof( pager ) );
            Helper.InitOrder( source, pager );
            if( pager.TotalCount <= 0 )
                pager.TotalCount = await source.CountAsync();
            var orderedQueryable = Helper.GetOrderedQueryable( source, pager );
            if( orderedQueryable == null )
                throw new ArgumentException( "" );
            return orderedQueryable.Skip( pager.GetSkipCount() ).Take( pager.PageSize );
        }
    }
}
