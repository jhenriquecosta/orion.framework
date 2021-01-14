using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Framework.DataLayer.NHibernate.DataLayer.UnitOfWorks;
using Framework.DataLayer.NHibernate.Dependency;
using Framework.DataLayer.NHibernate.Domains;

namespace Framework.DataLayer.NHibernate.DataOperation
{
    public interface IDataOperation<TEntity> : IScopeDependency,IDisposable where TEntity: class
    {
        ISession DbContext { get; }
        IUnitOfWork UnitOfWork { get; }
        void Use(IUnitOfWork unitOfWork);
        TEntity UnProxy(TEntity entity);

        TEntity AsProxy(object id);

        TEntity First(object id);

        TEntity Load(object id, LockMode lockMode=null);
        Task<TEntity> LoadAsync(object id, LockMode lockMode=null);

        TEntity First(Expression<Func<TEntity, bool>> filter);

        TEntity FirstOrDefault(object id);
        Task<TEntity> FirstOrDefaultAsync(object id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);

        IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> filter =null);

        IList<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null);
        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter=null);

        bool Any(Expression<Func<TEntity, bool>> filter);

        TEntity SaveOrUpdate(TEntity entity);
        TEntity Save(TEntity entity);
        Task<TEntity> SaveAsync(TEntity entity);

        Task<TEntity> SaveOrUpdateAsync(TEntity entity);

        void DeleteAll(Expression<Func<TEntity, bool>> filter);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(object id);
        TEntity Delete(object id, int version, bool useProxy = true);

        void Attach(TEntity entity);

        void Detach(TEntity entity);

        void Refresh(TEntity entity);

        int Count(Expression<Func<TEntity, bool>> filter = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
        Task FlushSessionAndEvictAsync(TEntity entity);
        Task ClearAndFlushAsync();
        Task FlushChangesAsync();

        void BeginTransaction();
        Task CommitAsync();
        Task FlushAsync();
       
        Task<object> LoadAsync(Type type, object id);
        Task<object> AddAsync(object entity);
        Task<object> MergeAsync(object entity);
        Task<TEntity> AddOrUpdateAsync(TEntity entity);
        Task<object> AddOrUpdateAsync(object entity);
        Task<object> SaveOrUpdateBatchAsync(object entity);
        Task<TEntity> SaveOrUpdateFlushAsync(TEntity entity);
         
    }
}
