using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orion.Framework.DataLayer.Queries;
using Orion.Framework.Domains;
using Orion.Framework.Domains.Repositories;
using System.Linq.Dynamic.Core;
using Orion.Framework.DataLayer.Stores;
using Orion.Framework.Maps;
using NHibernate.Linq;
using Orion.Framework.Web.Applications.Services.Contracts;
using Orion.Framework.Applications.Services.Contracts;

namespace Orion.Framework.Web.Applications.Services
{

    public abstract class QueryServiceBase<TEntity, TDto, TQueryParameter> : QueryServiceBase<TEntity, TDto, TQueryParameter, int>
        where TEntity : class, IKey<int>, IVersion
        where TDto : new()
        where TQueryParameter : IQueryParameter {
       
        protected QueryServiceBase( IQueryStore<TEntity, int> store ) : base( store ) {
        }
    }

   
    public abstract class QueryServiceBase<TEntity, TDto, TQueryParameter, TKey> : ServiceBase, IQueryService<TDto, TQueryParameter>
        where TEntity : class, IKey<TKey>, IVersion
        where TDto : new()
        where TQueryParameter : IQueryParameter {
      
        private readonly IQueryStore<TEntity, TKey> _store;

       
        protected QueryServiceBase( IQueryStore<TEntity, TKey> store ) {
            _store = store ?? throw new ArgumentNullException( nameof( store ) );
        }

      
        protected virtual TDto ToDto( TEntity entity ) 
        {
            return entity.MapTo<TDto>();
        }

       
    
        public virtual List<TDto> GetAll() 
        {
            return _store.FindAll().Select( ToDto ).ToList();
        }

        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _store.FindAllAsync();
            return entities.Select( ToDto ).ToList();
        }

        public virtual TDto GetById( object id ) {
            var key = Orion.Framework.Helpers.TypeConvert.To<TKey>( id );
            return ToDto(_store.Find( (object)key ) );
        }

       
        public virtual async Task<TDto> GetByIdAsync( object id ) {
            var key = Orion.Framework.Helpers.TypeConvert.To<TKey>( id );
            return ToDto( await _store.FindAsync( (object)key ) );
        }

      
        public virtual List<TDto> GetByIds( string ids ) {
            return _store.FindByIds( ids ).Select( ToDto ).ToList();
        }

     
        public virtual async Task<List<TDto>> GetByIdsAsync( string ids ) {
            var entities = await _store.FindByIdsAsync( ids );
            return entities.Select( ToDto ).ToList();
        }

     
        public virtual async Task<List<TDto>> QueryAsync( TQueryParameter parameter ) {
            if( parameter == null )
                return new List<TDto>();
            return ( await ExecuteQuery( parameter ).ToListAsync() ).Select( ToDto ).ToList();
        }

      
        public virtual List<TDto> Query( TQueryParameter parameter ) {
            if( parameter == null )
                return new List<TDto>();
            return ExecuteQuery( parameter ).ToList().Select( ToDto ).ToList();
        }

      
        private IQueryable<TEntity> ExecuteQuery( TQueryParameter parameter ) {
            var query = CreateQuery( parameter );
            var queryable = Filter( query );
            queryable = Filter( queryable, parameter );
            var order = query.GetOrder();
            return string.IsNullOrWhiteSpace( order ) ? queryable : queryable.OrderBy( order );
        }

      
        protected virtual IQueryBase<TEntity> CreateQuery( TQueryParameter parameter ) {
            return new Query<TEntity,TKey>( parameter );
        }

       
        private IQueryable<TEntity> Filter( IQueryBase<TEntity> query ) {
            return IsTracking ? _store.Find().Where( query ) : _store.FindAsNoTracking().Where( query );
        }

       
        protected virtual bool IsTracking => false;

     
        protected virtual IQueryable<TEntity> Filter( IQueryable<TEntity> queryable, TQueryParameter parameter )
        {
            queryable = queryable.EagerFetchAll();
            return queryable;
        }

        public virtual PagerList<TDto> PagerQuery( TQueryParameter parameter ) {
            if( parameter == null )
                return new PagerList<TDto>();
            var query = CreateQuery( parameter );
            var queryable = Filter( query );
            queryable = Filter( queryable, parameter );
            return queryable.ToPagerList( query.GetPager() ).Convert( ToDto );
        }

        public virtual async Task<PagerList<TDto>> PagerQueryAsync( TQueryParameter parameter ) {
            if( parameter == null )
                return new PagerList<TDto>();
            var query = CreateQuery( parameter );
            var queryable = Filter( query );
            queryable = Filter( queryable, parameter );
            var list = queryable.ToList();
            return ( await queryable.ToPagerListAsync( query.GetPager() ) ).Convert( ToDto );
        }

     
    }
}
