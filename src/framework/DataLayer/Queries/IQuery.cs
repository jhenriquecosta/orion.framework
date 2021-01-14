using System;
using System.Linq.Expressions;
using Orion.Framework.Domains.Repositories;

namespace Orion.Framework.DataLayer.Queries {
   
    public interface IQuery<TEntity> : IQuery<TEntity, int> where TEntity : class {
    }

    
    public interface IQuery<TEntity, TKey> : IQueryBase<TEntity> where TEntity : class {
       
        IQuery<TEntity, TKey> Where( Expression<Func<TEntity, bool>> predicate );
       
        IQuery<TEntity, TKey> Where( ICriteria<TEntity> criteria );
       
        IQuery<TEntity, TKey> WhereIf( Expression<Func<TEntity, bool>> predicate, bool condition );
     
        IQuery<TEntity, TKey> WhereIfNotEmpty( Expression<Func<TEntity, bool>> predicate );
     
        IQuery<TEntity, TKey> Between<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, int? min, int? max, Boundary boundary = Boundary.Both );
     
        IQuery<TEntity, TKey> Between<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, double? min, double? max, Boundary boundary = Boundary.Both );
       
        IQuery<TEntity, TKey> Between<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, decimal? min, decimal? max, Boundary boundary = Boundary.Both );
      
        IQuery<TEntity, TKey> Between<TProperty>( Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max,bool includeTime = true, Boundary? boundary = null );
      
        IQuery<TEntity, TKey> OrderBy<TProperty>( Expression<Func<TEntity, TProperty>> expression, bool desc = false );
        
        IQuery<TEntity, TKey> OrderBy( string propertyName, bool desc = false );
     
        IQuery<TEntity, TKey> And( Expression<Func<TEntity, bool>> predicate );
      
        IQuery<TEntity, TKey> And( IQuery<TEntity, TKey> query );
      
        IQuery<TEntity, TKey> Or( params Expression<Func<TEntity, bool>>[] predicates );
       
        IQuery<TEntity, TKey> Or( IQuery<TEntity, TKey> query );
    }
}
