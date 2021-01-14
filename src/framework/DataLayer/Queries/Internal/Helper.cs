using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using Orion.Framework.Domains.Repositories;
using Orion.Framework.Helpers;
using Orion.Framework.Properties;

namespace Orion.Framework.DataLayer.Queries.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public static class Helper {
       
        public static Expression<Func<TEntity, bool>> GetWhereIfNotEmptyExpression<TEntity>( Expression<Func<TEntity, bool>> predicate ) where TEntity : class {
            if ( predicate == null )
                return null;
            if( Lambda.GetConditionCount( predicate ) > 1 )
                throw new InvalidOperationException( string.Format( LibraryResource.OnlyOnePredicate, predicate ) );
            var value = predicate.Value();
            if( string.IsNullOrWhiteSpace( value.SafeString() ) )
                return null;
            return predicate;
        }

      
        public static void InitOrder<TEntity>( IQueryable<TEntity> source, IPager pager ) {
            if( string.IsNullOrWhiteSpace( pager.Order ) == false )
                return;
            if( source.Expression.SafeString().Contains( ".OrderBy(" ) )
                return;
            pager.Order = "Id";
        }

      
        public static IOrderedQueryable<TEntity> GetOrderedQueryable<TEntity>( IQueryable<TEntity> source, IPager pager ) {
            if( string.IsNullOrWhiteSpace( pager.Order ) )
                return source as IOrderedQueryable<TEntity>;
            return source.OrderBy( pager.Order );
        }
    }
}
