using System;
using System.Linq.Expressions;
using Orion.Framework.DataLayer.Queries.Criterias;
using Orion.Framework.DataLayer.Queries.Internal;

using Orion.Framework.Domains.Repositories;
using Orion.Framework.Helpers;

namespace Orion.Framework.DataLayer.Queries {
  
    public class Query<TEntity> : Query<TEntity, int>, IQuery<TEntity> where TEntity : class {
       
        public Query() {
        }

       
        public Query( IQueryParameter queryParam ) : base( queryParam ) {
        }
    }

   
    public class Query<TEntity, TKey> : IQuery<TEntity, TKey> where TEntity : class {
   
        private readonly IQueryParameter _parameter;
   
        private Expression<Func<TEntity, bool>> _predicate;
     
        private readonly OrderByBuilder _orderByBuilder;

    
        public Query() : this( new QueryParameter() ) {
        }

       
        public Query( IQueryParameter parameter ) {
            _parameter = parameter;
            _orderByBuilder = new OrderByBuilder();
            OrderBy( parameter.Order );
        }

       
        public Expression<Func<TEntity, bool>> GetPredicate() {
            return _predicate;
        }

     
        public string GetOrder() {
            return _orderByBuilder.Generate();
        }

       
        public IPager GetPager() {
            return new Pager( _parameter.Page, _parameter.PageSize, _parameter.TotalCount, GetOrder() );
        }

        
        public IQuery<TEntity, TKey> Where( Expression<Func<TEntity, bool>> predicate ) {
            return And( predicate );
        }

       
        public IQuery<TEntity, TKey> Where( ICriteria<TEntity> criteria ) {
            return And( criteria.GetPredicate() );
        }

      
        public IQuery<TEntity, TKey> WhereIf( Expression<Func<TEntity, bool>> predicate, bool condition ) {
            if( condition == false )
                return this;
            return Where( predicate );
        }

    
        public IQuery<TEntity, TKey> WhereIfNotEmpty( Expression<Func<TEntity, bool>> predicate ) {
            predicate = Helper.GetWhereIfNotEmptyExpression( predicate );
            if( predicate == null )
                return this;
            return And( predicate );
        }

        public IQuery<TEntity, TKey> Between<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, int? min, int? max, Boundary boundary = Boundary.Both ) {
            return Where( new IntSegmentCriteria<TEntity, TProperty>( propertyExpression, min, max, boundary ) );
        }

      
        public IQuery<TEntity, TKey> Between<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, double? min, double? max, Boundary boundary = Boundary.Both ) {
            return Where( new DoubleSegmentCriteria<TEntity, TProperty>( propertyExpression, min, max, boundary ) );
        }

       
        public IQuery<TEntity, TKey> Between<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, decimal? min, decimal? max, Boundary boundary = Boundary.Both ) {
            return Where( new DecimalSegmentCriteria<TEntity, TProperty>( propertyExpression, min, max, boundary ) );
        }

       
        public IQuery<TEntity, TKey> Between<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max, bool includeTime = true, Boundary? boundary = null ) {
            if( includeTime )
                return Where( new DateTimeSegmentCriteria<TEntity, TProperty>( propertyExpression, min, max, boundary ?? Boundary.Both ) );
            return Where( new DateSegmentCriteria<TEntity, TProperty>( propertyExpression, min, max, boundary ?? Boundary.Left ) );
        }

        
        public IQuery<TEntity, TKey> OrderBy<TProperty>( Expression<Func<TEntity, TProperty>> expression, bool desc = false ) {
            return OrderBy( Lambda.GetName( expression ), desc );
        }

      
        public IQuery<TEntity, TKey> OrderBy( string propertyName, bool desc = false ) {
            _orderByBuilder.Add( propertyName, desc );
            return this;
        }

        
        public IQuery<TEntity, TKey> And( Expression<Func<TEntity, bool>> predicate ) {
            _predicate = _predicate.And( predicate );
            return this;
        }

        public IQuery<TEntity, TKey> And( IQuery<TEntity, TKey> query ) {
            And( query.GetPredicate() );
            OrderBy( query.GetOrder() );
            return this;
        }

      
        public IQuery<TEntity, TKey> Or( params Expression<Func<TEntity, bool>>[] predicates ) {
            if ( predicates == null )
                return this;
            foreach ( var item in predicates ) {
                var predicate = Helper.GetWhereIfNotEmptyExpression( item );
                if( predicate == null )
                    continue;
                _predicate = _predicate.Or( predicate );
            }
            return this;
        }

      
        public IQuery<TEntity, TKey> Or( IQuery<TEntity, TKey> query ) {
            _predicate = _predicate.Or( query.GetPredicate() );
            OrderBy( query.GetOrder() );
            return this;
        }
    }
}
