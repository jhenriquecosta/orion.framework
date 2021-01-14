using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.NH.Applications.Dao
{
    public interface IDaoDomainService { }
    public interface IDaoDomainService<TEntity> : IDaoDomainService<TEntity,int> where TEntity : class, IEntity<TEntity, int>
    { }
    public interface IDaoDomainService<TEntity, TKey> : IDaoDomainService where TEntity : class, IEntity<TEntity, TKey>
    {
       // IUnitOfWorkCompleteHandle UnitOfWorkComplete { get; }
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> FirstOrDefaultAsync(TKey id);
        Task<TEntity> SaveOrUpdateAsync(TEntity entity);
        Task<TEntity> SaveAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> MergeAsync(TEntity entity);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);

        Task DeleteAsync(TKey id);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task FlushAsync();

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity FindOne(Expression<Func<TEntity, bool>> filter);
        TEntity FirstOrDefault(TKey id);
        TEntity SaveOrUpdate(TEntity entity);
        TEntity Save(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Merge(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> predicate);

        void Delete(TKey id);
        int Count(Expression<Func<TEntity, bool>> filter);

        void Flush();


    }
}
