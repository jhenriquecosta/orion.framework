using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.Webs.Applications.Aspects;
using Framework.DataLayer.UnitOfWorks;
using Framework.Domains.Services;

namespace Framework.Webs.Applications
{
    public interface IDataCrudService<TEntity> : IDomainService where TEntity : class
    {
      
        IUnitOfWork UnitOfWork { get; }
        void Use(IUnitOfWork unitOfWork);

        TEntity FirstOrDefault(object id);
        Task<TEntity> FirstOrDefaultAsync(object id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);

        [UnitOfWork]
        TEntity SaveOrUpdate(TEntity entity);

        [UnitOfWork]
        Task<TEntity> SaveOrUpdateAsync(TEntity entity);

        [UnitOfWork]
        void Delete(TEntity entity);

        [UnitOfWork]
        Task DeleteAsync(TEntity entity);

        IList<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null);
        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter = null);

        int Count(Expression<Func<TEntity, bool>> filter = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
    }

}
