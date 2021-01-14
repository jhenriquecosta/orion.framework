using System;
using System.Threading.Tasks;
using Orion.Framework.Domains;
using Orion.Framework.Logs.Extensions;

namespace Orion.Framework.Applications.Services
{

    public abstract partial class CrudServiceBase<TEntity, TDto, TRequest, TCreateRequest, TUpdateRequest, TQueryParameter, TKey>
    {


      

        protected virtual void CreateBefore( TEntity entity )
        {
            BeginTransaction();
        }
       
        protected virtual void CreateAfter( TEntity entity )
        {
            CommitAsync().Wait();
            AddLog( entity );
        

        }
       
        protected virtual void SaveBefore(TRequest request)
        {

        }
        protected virtual void SaveBefore(TEntity entity)
        {
        }

        protected virtual Task CreateBeforeAsync(TEntity entity)
        {
            CreateBefore(entity);
            return Task.CompletedTask;
        }
        protected virtual void SaveAfter()
        {
            WriteLog($"{EntityDescription}");
        }


        public void CommitAfter()
        {
            SaveAfter();
        }

        protected virtual  Task CreateAfterAsync(TEntity entity)
        {
            CreateAfter(entity);
            return Task.CompletedTask;
        }
     
        #region FIND
        protected virtual TEntity FindOldEntity(TKey id)
        {
            return _repository.Find((object)id,true);
        }
        protected virtual async Task<TEntity> FindOldEntityAsync(TKey id)
        {   
            var entity = await _repository.FindAsync((object)id,true);
            return entity;
        }
        #endregion

        #region CREATE
        public virtual string Create(TCreateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var entity = ToEntityFromCreateRequest(request);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Create(entity);
            return entity.Id.ToString();
        }

        protected void Create(TEntity entity)
        {
            CreateBefore(entity);
            entity.Init();
            _repository.Add(entity);
            CreateAfter(entity);
        }
        public virtual async Task<string> CreateAsync( TCreateRequest request ) {
            if( request == null )
                throw new ArgumentNullException( nameof( request ) );
            var entity = ToEntityFromCreateRequest( request );
            if( entity == null )
                throw new ArgumentNullException( nameof( entity ) );
            await CreateAsync( entity );
            return entity.Id.ToString();
        }

        protected async Task CreateAsync( TEntity entity ) {
          //  CreateBefore( entity );
            await CreateBeforeAsync( entity );
            entity.Init();
            await _repository.AddAsync( entity );
         //   CreateAfter( entity );
            await CreateAfterAsync( entity );
        }
#endregion

        #region UPDATE
        public virtual void Update( TUpdateRequest request ) 
        {
            if( request == null )
                throw new ArgumentNullException( nameof( request ) );
            var entity = ToEntityFromUpdateRequest( request );
            if( entity == null )
                throw new ArgumentNullException( nameof( entity ) );
            Update( entity );
        }

    
        protected void Update( TEntity entity ) 
        {
            var oldEntity = FindOldEntity( entity.Id );
            if( oldEntity == null )
                throw new ArgumentNullException( nameof( oldEntity ) );
            var changes = oldEntity.GetChanges( entity );
            UpdateBefore( entity );
            _repository.Update( entity );
            UpdateAfter( entity, changes );
        }

        public virtual async Task UpdateAsync( TUpdateRequest request )
        {
            if( request == null )
                throw new ArgumentNullException( nameof( request ) );
            var entity = ToEntityFromUpdateRequest( request );
            if( entity == null )
                throw new ArgumentNullException( nameof( entity ) );
            await UpdateAsync( entity );
        }

      
        protected async Task UpdateAsync( TEntity entity ) 
        {
            try
            {
                var oldEntity = await FindOldEntityAsync(entity.Id);
              
                if (oldEntity == null)
                    throw new ArgumentNullException(nameof(oldEntity));
                var changes = oldEntity.GetChanges(entity);

                await UpdateBeforeAsync(entity);            

                await _repository.UpdateAsync(entity);   
               // 
                await UpdateAfterAsync(entity);

            }
            catch(Exception )
            {
                throw;
            }
        }

        protected virtual Task UpdateBeforeAsync( TEntity entity ) 
        {
            
            UpdateBefore(entity);
            BeginTransaction();
            return Task.CompletedTask;
        }

    
        protected virtual Task UpdateAfterAsync( TEntity entity, ChangeValueCollection changeValues=null)
        {
            CommitAsync().Wait();
            UpdateAfter(entity, changeValues);

            return Task.CompletedTask;
        }
        protected virtual void UpdateAfter(TEntity entity, ChangeValueCollection changeValues = null)
        {

            Log.BusinessId(entity.Id.SafeString()).Content(changeValues.SafeString());
            AddLog(entity);

        }
        protected virtual void UpdateBefore(TEntity entity)
        {

        }
        #endregion

        #region SAVE
        public virtual void Save(TEntity entity)
        {
            //if (entity.IsTransient())
            //{
            //    Create(entity);
            //}
            //else
            //{
            //    Update(entity);
            //}

            SaveAsync(entity).Wait();
        }
        public virtual async Task SaveAsync(TEntity entity)
        {


            if (entity.IsTransient())
            {
                await CreateAsync(entity);
            }
            else
            {
                await UpdateAsync(entity);
            }

        }
        public virtual void Save( TRequest request )
        {
            SaveAsync(request).Wait();
            //if( request == null )
            //    throw new ArgumentNullException( nameof( request ) );
            //SaveBefore( request );
            //var entity = ToEntity( request );
            //if( entity == null )
            //    throw new ArgumentNullException( nameof( entity ) );
            //if( IsNew( request, entity ) ) {
            //    Create( entity );
            //    request.Id = entity.Id.ToString();
            //}
            //else
            //    Update( entity );
        }
       




        public virtual async Task SaveAsync( TRequest request ) 
        {
            try
            {
                if (request == null) throw new ArgumentNullException(nameof(request));
                SaveBefore(request);
                var entity = ToEntity(request);
                if (entity == null) throw new ArgumentNullException(nameof(entity));
                if (entity.IsTransient())
                { 
                    await CreateAsync(entity);
                    request.Id = entity.Id.ToString();
                }
                else
                    await UpdateAsync(entity);
            }
            catch (Exception )
            {
                throw;
            }
        }
        #endregion

        protected virtual bool IsNew(TRequest request, TEntity entity)
        {
            var isTransient = entity.IsTransient();
            var result =  string.IsNullOrWhiteSpace(request.Id) || entity.Id.Equals(default(TKey));
            return result;
        }


       
    }
}
