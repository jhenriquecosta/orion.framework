using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orion.Framework.Applications.Services
{

    public abstract partial class CrudServiceBase<TEntity, TDto, TRequest, TCreateRequest, TUpdateRequest,TQueryParameter, TKey>
    {

        public virtual async Task<List<TDto>> SaveAsync(List<TDto> creationList, List<TDto> updateList,
            List<TDto> deleteList)
        {
            if (creationList == null && updateList == null && deleteList == null)
                return new List<TDto>();
            creationList = creationList ?? new List<TDto>();
            updateList = updateList ?? new List<TDto>();
            deleteList = deleteList ?? new List<TDto>();
            FilterList(creationList, updateList, deleteList);
            var addEntities = ToEntities(creationList);
            var updateEntities = ToEntities(updateList);
            var deleteEntities = ToEntities(deleteList);
            SaveBefore(addEntities, updateEntities, deleteEntities);
            await AddListAsync(addEntities);
            await UpdateListAsync(updateEntities);
            await DeleteListAsync(deleteEntities);
            await CommitAsync();
            SaveAfter(addEntities, updateEntities, deleteEntities);
            return GetResult(addEntities, updateEntities);
        }

        private void FilterList(List<TDto> creationList, List<TDto> updateList, List<TDto> deleteList)
        {
            FilterByDeleteList(creationList, deleteList);
            FilterByDeleteList(updateList, deleteList);
        }

        private void FilterByDeleteList(List<TDto> list, List<TDto> deleteList)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (deleteList.Any(d => d.Id == item.Id))
                    list.Remove(item);
            }
        }


        private List<TEntity> ToEntities(List<TDto> dtos)
        {
            return dtos.Select(ToEntityFromDto).Distinct().ToList();
        }

        protected virtual void SaveBefore(List<TEntity> creationList, List<TEntity> updateList,
            List<TEntity> deleteList)
        {
        }

        private async Task AddListAsync(List<TEntity> list)
        {
            if (list.Count == 0)
                return;
            Log.Content("：");
            foreach (var entity in list)
                await CreateAsync(entity);
        }

        private async Task UpdateListAsync(List<TEntity> list)
        {
            if (list.Count == 0)
                return;
            Log.Content("：");
            foreach (var entity in list)
                await UpdateAsync(entity);
        }


        private async Task DeleteListAsync(List<TEntity> list)
        {
            if (list.Count == 0)
                return;
            Log.Content("：");
            foreach (var entity in list)
                await DeleteChildsAsync(entity);
        }


        protected virtual async Task DeleteChildsAsync(TEntity parent)
        {
            await DeleteEntityAsync(parent);
        }

        protected async Task DeleteEntityAsync(TEntity entity)
        {
            await _repository.RemoveAsync(entity.Id);
            AddLog(entity);
        }

        private void BeginTransaction()
        {
            //_unitOfWork.BeginTransaction();
        }
        private async Task CommitAsync()
        {
           // await _unitOfWork.CommitAsync();
        }


        protected virtual void SaveAfter(List<TEntity> creationList, List<TEntity> updateList, List<TEntity> deleteList)
        {
            WriteLog($"{EntityDescription}");
        }


        protected virtual List<TDto> GetResult(List<TEntity> creationList, List<TEntity> updateList)
        {
            return creationList.Concat(updateList).Select(ToDto).ToList();
        }


    }
}

#region BACKUP
    //public virtual List<TDto> Save( List<TRequest> addList, List<TRequest> updateList, List<TRequest> deleteList )
    //{
    //    if( addList == null && updateList == null && deleteList == null )
    //        return new List<TDto>();
    //    addList = addList ?? new List<TRequest>();
    //    updateList = updateList ?? new List<TRequest>();
    //    deleteList = deleteList ?? new List<TRequest>();
    //    FilterList( addList, updateList, deleteList );
    //    var addEntities = ToEntities( addList );
    //    var updateEntities = ToEntities( updateList );
    //    var deleteEntities = ToEntities( deleteList );
    //    BeginTransaction();
    //    SaveBefore( addEntities, updateEntities, deleteEntities );
    //    AddList( addEntities );
    //    UpdateList( updateEntities );
    //    DeleteList( deleteEntities );
    //    Commit();
    //    SaveAfter( addEntities, updateEntities, deleteEntities );
    //    return GetResult( addEntities, updateEntities );
    //}
    //private List<TEntity> ToEntities( List<TRequest> dtos ) {
    //    return dtos.Select( ToEntity ).Distinct().ToList();
    //}
    //private void FilterList( List<TRequest> addList, List<TRequest> updateList, List<TRequest> deleteList ) {
    //    FilterByDeleteList( addList, deleteList );
    //    FilterByDeleteList( updateList, deleteList );
    //}
    //private void FilterByDeleteList( List<TRequest> list, List<TRequest> deleteList ) {
    //    for( int i = 0; i < list.Count; i++ ) {
    //        var item = list[i];
    //        if( deleteList.Any( d => d.Id == item.Id ) )
    //            list.Remove( item );
    //    }
    //}
    //protected virtual void SaveBefore( List<TEntity> addList, List<TEntity> updateList, List<TEntity> deleteList )
    //{
    //}
    //private void AddList( List<TEntity> list ) {
    //    if( list.Count == 0 )
    //        return;
    //    Log.Content( "：" );
    //    list.ForEach( Create );
    //}
    //private void UpdateList( List<TEntity> list ) {
    //    if( list.Count == 0 )
    //        return;
    //    Log.Content( "：" );
    //    list.ForEach( Update );
    //}
    //private void DeleteList( List<TEntity> list ) {
    //    if( list.Count == 0 )
    //        return;
    //    Log.Content( "：" );
    //    list.ForEach( DeleteChilds );
    //}
    //protected virtual void DeleteChilds( TEntity parent ) {
    //    DeleteEntity( parent );
    //}
    //protected void DeleteEntity( TEntity entity ) {
    //    _repository.Delete( entity.Id );
    //    AddLog( entity );
    //}
    //private void BeginTransaction()
    //{
    //    _unitOfWork.BeginTransaction();
    //}
    //private void Commit()
    //{
    //    _unitOfWork.Commit();
    //}
    //protected virtual void SaveAfter( List<TEntity> addList, List<TEntity> updateList, List<TEntity> deleteList ) {
    //    WriteLog( $"{EntityDescription}" );
    //}
    //protected virtual List<TDto> GetResult( List<TEntity> addList, List<TEntity> updateList ) {
    //    return addList.Concat( updateList ).Select( ToDto ).ToList();
    //}
    //public virtual async Task<List<TDto>> SaveAsync( List<TRequest> addList, List<TRequest> updateList,List<TRequest> deleteList ) {
    //    if( addList == null && updateList == null && deleteList == null )
    //        return new List<TDto>();
    //    addList = addList ?? new List<TRequest>();
    //    updateList = updateList ?? new List<TRequest>();
    //    deleteList = deleteList ?? new List<TRequest>();
    //    FilterList( addList, updateList, deleteList );
    //    var addEntities = ToEntities( addList );
    //    var updateEntities = ToEntities( updateList );
    //    var deleteEntities = ToEntities( deleteList );
    //    SaveBefore( addEntities, updateEntities, deleteEntities );
    //    await AddListAsync( addEntities );
    //    await UpdateListAsync( updateEntities );
    //    await DeleteListAsync( deleteEntities );
    //    await CommitAsync();
    //    SaveAfter( addEntities, updateEntities, deleteEntities );
    //    return GetResult( addEntities, updateEntities );
    //}
    //private async Task AddListAsync( List<TEntity> list ) {
    //    if( list.Count == 0 )
    //        return;
    //    Log.Content( "：" );
    //    foreach ( var entity in list )
    //        await CreateAsync( entity );
    //}
    //private async Task UpdateListAsync( List<TEntity> list ) {
    //    if( list.Count == 0 )
    //        return;
    //    Log.Content( "：" );
    //    foreach( var entity in list )
    //        await UpdateAsync( entity );
    //}
    //private async Task DeleteListAsync( List<TEntity> list ) {
    //    if( list.Count == 0 )
    //        return;
    //    Log.Content( "：" );
    //    foreach( var entity in list )
    //        await DeleteChildsAsync( entity );
    //}
    //protected virtual async Task DeleteChildsAsync( TEntity parent ) {
    //    await DeleteEntityAsync( parent );
    //}
    //protected async Task DeleteEntityAsync( TEntity entity ) {
    //    await _repository.DeleteAsync( entity.Id );
    //    AddLog( entity );
    //}
    //private async Task CommitAsync() {
    //    await _unitOfWork.CommitAsync();
    //}
    #endregion

