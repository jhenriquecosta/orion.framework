using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orion.Framework.DataLayer.Queries;
using Orion.Framework.DataLayer.Queries.Criterias;
using Orion.Framework.DataLayer.Queries.Internal;
using Orion.Framework.Domains.Repositories;

namespace Orion.Framework
{
   
    public static partial class Extensions {

      
        public static IQueryable<TEntity> Where<TEntity>( this IQueryable<TEntity> source, ICriteria<TEntity> criteria ) where TEntity : class {
            if( source == null )
                throw new ArgumentNullException( nameof( source ) );
            if( criteria == null )
                throw new ArgumentNullException( nameof( criteria ) );
            var predicate = criteria.GetPredicate();
            if( predicate == null )
                return source;
            return source.Where( predicate );
        }
        public static TEntity FirstOrDefault<TEntity>(this IQueryable<TEntity> source, ICriteria<TEntity> criteria) where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));
            var predicate = criteria.GetPredicate();
            if (predicate == null)
                return source.FirstOrDefault();
            return source.FirstOrDefault(predicate);
        }

        public static IQueryable<TEntity> WhereIf<TEntity>( this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate, bool condition ) where TEntity : class {
            if( source == null )
                throw new ArgumentNullException( nameof( source ) );
            if( condition == false )
                return source;
            return source.Where( predicate );
        }

      
        public static IQueryable<TEntity> WhereIfNotEmpty<TEntity>( this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate ) where TEntity : class {
            if( source == null )
                throw new ArgumentNullException( nameof( source ) );
            predicate = Helper.GetWhereIfNotEmptyExpression( predicate );
            if( predicate == null )
                return source;
            return source.Where( predicate );
        }

     
        public static IQueryable<TEntity> Between<TEntity, TProperty>( this IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> propertyExpression, int? min, int? max, Boundary boundary = Boundary.Both ) where TEntity : class {
            if( source == null )
                throw new ArgumentNullException( nameof( source ) );
            return source.Where( new IntSegmentCriteria<TEntity, TProperty>( propertyExpression, min, max, boundary ) );
        }

   
        public static IQueryable<TEntity> Between<TEntity, TProperty>( this IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> propertyExpression, double? min, double? max, Boundary boundary = Boundary.Both ) where TEntity : class {
            if( source == null )
                throw new ArgumentNullException( nameof( source ) );
            return source.Where( new DoubleSegmentCriteria<TEntity, TProperty>( propertyExpression, min, max, boundary ) );
        }

        public static IQueryable<TEntity> Between<TEntity, TProperty>( this IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> propertyExpression, decimal? min, decimal? max, Boundary boundary = Boundary.Both ) where TEntity : class {
            if( source == null )
                throw new ArgumentNullException( nameof( source ) );
            return source.Where( new DecimalSegmentCriteria<TEntity, TProperty>( propertyExpression, min, max, boundary ) );
        }

        public static IQueryable<TEntity> Between<TEntity, TProperty>( this IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max, bool includeTime = true, Boundary? boundary = null ) where TEntity : class {
            if( source == null )
                throw new ArgumentNullException( nameof( source ) );
            if( includeTime )
                return source.Where( new DateTimeSegmentCriteria<TEntity, TProperty>( propertyExpression, min, max, boundary ?? Boundary.Both ) );
            return source.Where( new DateSegmentCriteria<TEntity, TProperty>( propertyExpression, min, max, boundary ?? Boundary.Left ) );
        }

      
        public static IQueryable<TEntity> Page<TEntity>( this IQueryable<TEntity> source, IPager pager ) {
            if( source == null )
                throw new ArgumentNullException( nameof( source ) );
            if( pager == null )
                throw new ArgumentNullException( nameof( pager ) );
            Helper.InitOrder( source, pager );
            if( pager.TotalCount <= 0 )
                pager.TotalCount = source.Count();
            var orderedQueryable = Helper.GetOrderedQueryable( source, pager );
            if( orderedQueryable == null )
                throw new ArgumentException("Erro");
            return orderedQueryable.Skip( pager.GetSkipCount() ).Take( pager.PageSize );
        }

     
        public static PagerList<TEntity> ToPagerList<TEntity>( this IQueryable<TEntity> source, IPager pager ) {
            if( source == null )
                throw new ArgumentNullException( nameof( source ) );
            if( pager == null )
                throw new ArgumentNullException( nameof( pager ) );
            return new PagerList<TEntity>( pager, source.Page( pager ).ToList() );
        }
    }
}
