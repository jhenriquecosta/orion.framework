using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orion.Framework.DataLayer.Queries;
using Orion.Framework.DataLayer.Stores;
using Orion.Framework.Domains;
using Orion.Framework.Helpers;
using Orion.Framework.Logs.Extensions;
using Orion.Framework.Applications.Services.Contracts;
using Orion.Framework.Applications.Dtos;
using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;

namespace Orion.Framework.Applications.Services
{

    public abstract class DeleteServiceBase<TEntity, TDto, TQueryParameter> : DeleteServiceBase<TEntity, TDto, TQueryParameter, int>
        where TEntity : class, IKey<int>, IVersion, new()
        where TDto : IDto, new()
        where TQueryParameter : IQueryParameter
    {
      
        protected DeleteServiceBase( IStore<TEntity, int> store) : base( store )
        {
        }
    }

   
    public abstract class DeleteServiceBase<TEntity, TDto, TQueryParameter, TKey>
        : QueryServiceBase<TEntity, TDto, TQueryParameter, TKey>,IDeleteService<TDto, TQueryParameter>
        where TEntity : class, IKey<TKey>,IVersion, new()
        where TDto : new()
        where TQueryParameter : IQueryParameter {
       
        private readonly IUnitOfWork _unitOfWork;      
        private readonly IStore<TEntity, TKey> _store;
       
        protected DeleteServiceBase( IStore<TEntity, TKey> store ) : base( store ) 
        {

            // _unitOfWork =  store;
            var uowName = typeof(TEntity).FullName;
            var uow = DataStoreCache.Get<IUnitOfWork>(uowName);


            _unitOfWork = uow;
            _store = store;
         
            EntityDescription = Reflection.GetDisplayNameOrDescription<TEntity>();
        }


        protected string EntityDescription { get; }

      
        protected void WriteLog( string caption ) {
            Log.Class( typeof( TEntity ).FullName )
                .Caption( caption )
                .Info();
        }

       
        protected void WriteLog( string caption, TEntity entity ) {
            AddLog( entity );
            WriteLog( caption );
        }

     
        protected void WriteLog( string caption, IList<TEntity> entities ) {
            AddLog( entities );
            WriteLog( caption );
        }

     
        protected void AddLog( TEntity entity ) {
            Log.BusinessId( entity.Id.SafeString() );
            Log.Content( entity.ToString() );
        }

      
        protected void AddLog( IList<TEntity> entities ) {
            Log.BusinessId( entities.Select( t => t.Id ).Join() );
            foreach( var entity in entities )
                Log.Content( entity.ToString() );
        }

     
        public virtual void Delete( string ids ) {
            if( string.IsNullOrWhiteSpace( ids ) )
                return;
            var entities = _store.FindByIds( ids );
            if( entities?.Count == 0 )
                return;
            DeleteBefore( entities );

            _store.Remove( entities );
         //   _unitOfWork.Commit();
            DeleteAfter( entities );
        }

     
        protected virtual void DeleteBefore( List<TEntity> entities ) 
        {
           // _unitOfWork.BeginTransaction();
        }

  
        protected virtual void DeleteAfter( List<TEntity> entities ) {
            AddLog( entities );
            WriteLog( $"{EntityDescription}" );
        }

        //[Transactional]
        public virtual async Task DeleteAsync( string ids ) {
            if( string.IsNullOrWhiteSpace( ids ) )
                return;
            var entities = await _store.FindByIdsAsync( ids );
            if( entities?.Count == 0 )
                return;
            DeleteBefore( entities );
            await _store.RemoveAsync( entities );
           // await _unitOfWork.CommitAsync();
            DeleteAfter( entities );
        }
    }
}
