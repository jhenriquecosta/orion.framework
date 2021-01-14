using Orion.Framework.DataLayer.Repositories.Abstracts;
using Orion.Framework.DataLayer.Repositories.Contracts;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Domains;
using Orion.Framework.Domains.Services;
using Orion.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.NH.Applications.Dao
{

    public abstract class DaoDomainService<TEntity> : DaoDomainService<TEntity, int>, IDaoDomainService<TEntity> where TEntity : class, IEntity<TEntity, int>
    {
        public DaoDomainService(IRepository<TEntity, int> repository, IUnitOfWorkManager unitOfWorkManager) : base(repository, unitOfWorkManager)
        { }
    }
    public abstract class DaoDomainService<TEntity, TKey> : DomainService, IDaoDomainService<TEntity, TKey> where TEntity : class, IEntity<TEntity, TKey>
    {
        private readonly IRepository<TEntity, TKey> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public DaoDomainService(IRepository<TEntity, TKey> repository, IUnitOfWorkManager unitOfWorkManager)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        #region Async
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            //var result = await _repository.FindAllAsync(filter);
            var result = await UseUow(async () => { return await _repository.FindAllAsync(filter); });
            return result;
        }
        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            var result = await UseUow(async () => { return await _repository.FirstOrDefaultAsync(filter); });
            return result;
        }
        public async Task<TEntity> FirstOrDefaultAsync(TKey id)
        {
            var result = await UseUow(async () => { return await _repository.GetAsync(id); });
            return result;
        }
        public async Task<TEntity> SaveOrUpdateAsync(TEntity entity)
        {
            var result = await UseUow(async () => { return await _repository.InsertOrUpdateAndGetIdAsync(entity); });
            return entity;
        }
        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            await _repository.InsertAndGetIdAsync(entity);
            return entity;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
            return entity;
        }
        public async Task<TEntity> MergeAsync(TEntity entity)
        {
            await _repository.GetSession().MergeAsync(entity);
            return entity;
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            var result = await UseUow(async () => { return await _repository.CountAsync(filter); });
            return result;
        }
        public async Task FlushAsync()
        {
            await _repository.GetSession().FlushAsync();
            _repository.GetSession().Clear();

        }
        #endregion

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return AsyncUtil.RunSync(() => FindAllAsync(filter));
        }
        public TEntity FindOne(Expression<Func<TEntity, bool>> filter)
        {
            return AsyncUtil.RunSync(() => FindOneAsync(filter));
        }
        public TEntity FirstOrDefault(TKey id)
        {
            return AsyncUtil.RunSync(() => FirstOrDefaultAsync(id));
        }

        public TEntity SaveOrUpdate(TEntity entity)
        {
            return AsyncUtil.RunSync(() => SaveOrUpdateAsync(entity));
        }
        public TEntity Save(TEntity entity)
        {
            return AsyncUtil.RunSync(() => SaveAsync(entity));
        }
        public TEntity Update(TEntity entity)
        {
            return AsyncUtil.RunSync(() => UpdateAsync(entity));
        }
        public TEntity Merge(TEntity entity)
        {
            return AsyncUtil.RunSync(() => MergeAsync(entity));
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            return AsyncUtil.RunSync(() => CountAsync(filter));
        }
        public void Flush()
        {
            AsyncUtil.RunSync(() => FlushAsync());
        }

        public async Task DeleteAsync(TKey id)
        {
            await UseUow(async () => {  await _repository.DeleteAsync(id); });
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await UseUow(async () => { await _repository.DeleteAsync(entity); });
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            AsyncUtil.RunSync(() => DeleteAsync(entity));
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Delete(TKey id)
        {
            AsyncUtil.RunSync(() => DeleteAsync(id));
        }
    }

}
