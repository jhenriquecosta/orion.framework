using NHibernate;
using NHibernate.Linq;
using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.DataLayer.NHibernate.DataLayer.UnitOfWorks;
using Framework.DataLayer.NHibernate.Domains;
using Framework.DataLayer.NHibernate.Exceptions;
using Framework.DataLayer.NHibernate.Helpers;
using Framework.DataLayer.NHibernate.Utility;

namespace Framework.DataLayer.NHibernate.DataOperation
{

    public partial class NhDataOperation<TEntity> : IDataOperation<TEntity> where TEntity : class
    {
        private IUnitOfWork unitOfWork;
        public ISession DbContext { get { return UnitOfWork.Session; } }
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork != null && !unitOfWork.Disposed)
                {
                    return unitOfWork;
                }

                throw new Warning(string.Format("Repository {0} has no assigned unit of work or it was disposed.", GetType().Name));
            }
        }
        public void Use(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            //Session = uow.ActiveSession;
            //SessionReadOnly = uow.ActiveSessionReadOnly;
        }
        public NhDataOperation()
        {
           Use(NHibernateHelper.Instance.GetUnitOfWork(typeof(TEntity)));
        }
           

        public TEntity UnProxy(TEntity entity)
        {
            INHibernateProxy proxy = entity as INHibernateProxy;
            if (proxy != null)
            {
                return (TEntity)proxy.HibernateLazyInitializer.GetImplementation();
            }

            return entity;
        }

        public virtual TEntity AsProxy(object id) 
        {
            return UnitOfWork.Session.Load<TEntity>(id, LockMode.None);
        }

        public virtual TEntity First(object id)
        {
            TEntity entity = FirstOrDefault(id);

            //if (entity == null)
            //{
            //    throw new EntityNotFoundException(typeof(TEntity), id);
            //}

            return entity;
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> filter)
        {
            TEntity entity = FirstOrDefault(filter);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), filter.ToString());
            }

            return entity;
        }

        #region FirstOrDefault
        public virtual TEntity FirstOrDefault(object id)
        {
            return AsyncUtil.RunSync(() => FirstOrDefaultAsync(id)); 
        }
        public virtual async Task<TEntity> FirstOrDefaultAsync(object id)
        {
            return await UnitOfWork.Session.GetAsync<TEntity>(id);  //AsQueryable().FirstOrDefault(f => f.Id == id);
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return AsyncUtil.RunSync(() => FirstOrDefaultAsync(filter));
        }
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await AsQueryable(filter).FirstOrDefaultAsync();
        }
        #endregion
        #region FindAll
        public virtual IList<TEntity> FindAll(Expression<Func<TEntity, bool>> filter =null)  
        {
            var result = AsyncUtil.RunSync(() => FindAllAsync(filter)); // Task.FromResult(FindAllAsync(filter));
            return result;

        }
        public virtual async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter = null)  
        {
            var query = AsQueryable();
            if (filter != null) query = query.Where(filter);
            return await query.ToListAsync();

        }
        #endregion

        public virtual IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> filter) 
        {
            return AsQueryable().Where(filter);
        }

        public virtual IQueryable<TEntity> AsQueryable() 
        {
            return UnitOfWork.Session.Query<TEntity>();  //.Where(f => !f.IsDeleted);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> filter)  
        {
            return AsQueryable().Where(filter).Any();
        }


        #region Save
        public virtual TEntity Save(TEntity save)
        {
            var result = AsyncUtil.RunSync(() => SaveAsync(save));
            return result;
        }
        public virtual async Task<TEntity> SaveAsync(TEntity save)
        {
            Detach(save);
            await UnitOfWork.Session.SaveAsync(save);
            return save;
        }

        #endregion
        #region SaveOrUpdate
        public virtual TEntity SaveOrUpdate(TEntity save) 
        {
            var result = AsyncUtil.RunSync(() => SaveOrUpdateAsync(save));
            return result;
        }
        public virtual async Task<TEntity> SaveOrUpdateAsync(TEntity save)
        {
            var entity = save as IEntity<int>;
            if (entity.IsTransient())
            {
             

               await UnitOfWork.Session.SaveOrUpdateAsync(entity);
            }
            else
            {
              await UnitOfWork.Session.MergeAsync(entity);
            }
            await UnitOfWork.Session.FlushAsync();
            return save;
        }

        #endregion
        #region Delete
        public virtual void DeleteAll(Expression<Func<TEntity, bool>> filter)
        {
            AsQueryable().Where(filter).Delete();
        }
        public virtual void Delete(TEntity entity) 
        {
            AsyncUtil.RunSync(() => DeleteAsync(entity));
        }

        public virtual TEntity Delete(object id, int version, bool useProxy = true)  
        {
            TEntity entity = useProxy
                                ? AsProxy(id)
                                : First(id);

            //entity.Version = version;
            UnitOfWork.Session.Delete(entity);

            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
           await UnitOfWork.Session.DeleteAsync(entity);
        }
        public virtual async Task DeleteAsync(object id)
        {
            var entity = await FirstOrDefaultAsync(id);
            await UnitOfWork.Session.DeleteAsync(entity);
        }
        #endregion
        public virtual void Attach(TEntity entity)  
        {
            UnitOfWork.Session.Lock(entity, LockMode.None);
        }

        public virtual void Detach(TEntity entity)  
        {
            UnitOfWork.Session.Evict(entity);
        }

        public void Refresh(TEntity entity)  
        {
            UnitOfWork.Session.Refresh(entity);
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            return AsyncUtil.RunSync(() => CountAsync(filter));

        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return (filter == null) ? AsQueryable().CountAsync() : AsQueryable().CountAsync(filter);
        }

        public async Task FlushSessionAndEvictAsync(TEntity entity)
        {
            await ClearAndFlushAsync();
             await UnitOfWork.Session.EvictAsync(entity);
        }
        public async Task ClearAndFlushAsync( )
        {
            UnitOfWork.Session.Clear();
            await UnitOfWork.Session.FlushAsync();
          
        }

        public TEntity Load(object id, LockMode lockMode = null)
        {
            if (lockMode == null) lockMode = LockMode.None;
            return UnitOfWork.Session.Load<TEntity>(id, lockMode);
        }

        public async Task<TEntity> LoadAsync(object id, LockMode lockMode = null)
        {
            if (lockMode == null) lockMode = LockMode.None;
            return await UnitOfWork.Session.LoadAsync<TEntity>(id, lockMode);
        }
        public async Task FlushChangesAsync()
        {
            await UnitOfWork.Session.FlushAsync();
            UnitOfWork.Session.Clear();
        }
        

        public virtual async Task<object> AddOrUpdateAsync(object entity)
        {
            var save = entity as IBaseEntity;
            BeginTransaction();
            if (save.IsTransient())
            {
                await AddAsync(save);
            }
            else
            {
                await MergeAsync(save);
            }
            await CommitAsync();
            await FlushAsync();
            return save;
        }
        public virtual async Task<TEntity> AddOrUpdateAsync(TEntity entity)
        {
            BeginTransaction();
            var ent = entity as IBaseEntity;
            if (ent.IsTransient())
            {
                await AddAsync(entity);
            }
            else
            {
                await MergeAsync(entity);
            }
            await CommitAsync();
            await FlushChangesAsync();
            return entity;
        }

        public virtual async Task<object> AddAsync(object entity)
        {

            await UnitOfWork.Session.SaveAsync(entity);
            return entity;
        }
        public virtual async Task<object> MergeAsync(object entity)
        {
            await UnitOfWork.Session.MergeAsync(entity);
            return entity;
        }

        public virtual async Task<object> SaveOrUpdateBatchAsync(object entity)
        {
            var save = entity as IAggregateRoot;
            if (save.IsTransient())
            {
                await AddAsync(save);
            }
            else
            {
                await MergeAsync(save);
            }
            return save;
        }

        public void BeginTransaction()
        {
            UnitOfWork.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            await UnitOfWork.CommitAsync();
        }

        public async Task FlushAsync()
        {
            await UnitOfWork.Session.FlushAsync();
        }



        public async Task<TEntity> SaveOrUpdateFlushAsync(TEntity entity)
        {

            var ent = entity as IBaseEntity;
            if (ent.IsTransient())
            {
                await AddAsync(entity);
            }
            else
            {
                await MergeAsync(entity);
            }
            await FlushChangesAsync();
            return entity;
        }

        public async Task<object> LoadAsync(Type type, object id)
        {
            return await UnitOfWork.Session.LoadAsync(type, id);
        }
        ~NhDataOperation()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //NHibernateHelper.Instance.UnitOfWorkFactory().Remove(typeof(TEntity).FullName);
                //UnitOfWork.Dispose();
                //GC.SuppressFinalize(this);
            }
        }
    }
}
