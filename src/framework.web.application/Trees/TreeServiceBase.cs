using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orion.Framework.Infrastructurelications.Trees;
using Orion.Framework.DataLayer.Queries.Trees;
using Orion.Framework.DataLayer.Stores;
using Orion.Framework.DataLayer.UnitOfWorks;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Domains;
using Orion.Framework.Domains.Trees;
using TypeConvert = Orion.Framework.Helpers.TypeConvert;

namespace Orion.Framework.Web.Applications.Services.Trees
{

    public abstract class TreeServiceBase<TEntity, TDto, TQueryParameter>
        : TreeServiceBase<TEntity, TDto, TQueryParameter, Guid, Guid?>, ITreeService<TDto, TQueryParameter>
        where TEntity : class, IParentId<Guid?>, IPath, IEnabled, ISortId, IKey<Guid>, IVersion, new()
        where TDto : class, ITreeNode, new()
        where TQueryParameter : class, ITreeQueryParameter {
       
        private readonly IStore<TEntity, Guid> _store;

     
        protected TreeServiceBase(  IStore<TEntity, Guid> store ) : base(  store ) {
            _store = store;
        }

     
        protected override IQueryable<TEntity> Filter( IQueryable<TEntity> queryable, TQueryParameter parameter ) {
            return queryable.Where( new TreeCriteria<TEntity>( parameter ) );
        }

      
        protected override async Task<List<TEntity>> GetChildren( TQueryParameter parameter ) {
            return await _store.FindAllAsync( t => t.ParentId == parameter.ParentId );
        }
    }

  
    public abstract class TreeServiceBase<TEntity, TDto, TQueryParameter, TKey, TParentId>
        : DeleteServiceBase<TEntity, TDto, TQueryParameter, TKey>, ITreeService<TDto, TQueryParameter, TParentId>
        where TEntity : class, IParentId<TParentId>, IPath, IEnabled, ISortId, IKey<TKey>, IVersion, new()
        where TDto : class, ITreeNode, new()
        where TQueryParameter : class, ITreeQueryParameter<TParentId> {
      
        private readonly IUnitOfWork _unitOfWork;
      
        private readonly IStore<TEntity, TKey> _store;

       
        protected TreeServiceBase(  IStore<TEntity, TKey> store ) : base(  store ) {
            //_unitOfWork = unitOfWork;
            _store = store;
        }

       
        protected override IQueryable<TEntity> Filter( IQueryable<TEntity> queryable, TQueryParameter parameter ) {
            return queryable.Where( new TreeCriteria<TEntity, TParentId>( parameter ) );
        }

      
        public virtual async Task<List<TDto>> FindByIdsAsync( string ids ) {
            var entities = await _store.FindByIdsNoTrackingAsync( ids );
            return entities.Select( ToDto ).ToList();
        }

      
        public virtual async Task EnableAsync( string ids ) {
            await Enable( TypeConvert.ToList<TKey>( ids ), true );
        }

       
        private async Task Enable( List<TKey> ids, bool enabled ) {
            if( ids == null || ids.Count == 0 )
                return;
            var entities = await _store.FindByIdsAsync( ids );
            if( entities == null )
                return;
            foreach ( var entity in entities ) {
                if( enabled && await AllowEnable( entity ) == false )
                    return;
                if( enabled == false && await AllowDisable( entity ) == false )
                    return;
                entity.Enabled = enabled;
                await _store.UpdateAsync( entity );
            }
           // _unitOfWork.Commit();
            WriteLog( entities, enabled );
        }

      
        protected virtual Task<bool> AllowEnable( TEntity entity ) {
            return Task.FromResult( true );
        }

       
        protected virtual Task<bool> AllowDisable( TEntity entity ) {
            return Task.FromResult( true );
        }

       
        private void WriteLog( List<TEntity> entities, bool enabled ) {
            AddLog( entities );
            WriteLog( $"{( enabled ? "" : "" )}" );
        }

     
        public virtual Task DisableAsync( string ids ) {
            return Enable( TypeConvert.ToList<TKey>( ids ), false );
        }

      
        public virtual async Task SwapSortAsync( int id, int swapId ) {
            var entity = await _store.FindAsync( id );
            var swapEntity = await _store.FindAsync( swapId );
            if( entity == null || swapEntity == null )
                return;
            entity.SwapSort( swapEntity );
            await _store.UpdateAsync( entity );
            await _store.UpdateAsync( swapEntity );
           // await _unitOfWork.CommitAsync();
        }

      
        public virtual async Task FixSortIdAsync( TQueryParameter parameter ) {
            var children = await GetChildren( parameter );
            if ( children == null )
                return;
            var list = children.OrderBy( t => t.SortId ).ToList();
            for ( int i = 0; i < children.Count; i++ )
                children[i].SortId = i + 1;
            await _store.UpdateAsync( list );
         //   await _unitOfWork.CommitAsync();
        }

      
        protected abstract Task<List<TEntity>> GetChildren( TQueryParameter parameter );
    }
}
