using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;

namespace Orion.Framework.DataLayer.NH.Dao
{
    public interface IDao
    {
        IQueryable<TEntity> AsQueryable<TEntity>(Expression<Func<TEntity, bool>> filter = null);
        Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> LoadAsync<TEntity>(object id);
        Task DeattachAsync<TEntity>(TEntity obj);
        Task<TEntity> SaveAsync<TEntity>(TEntity entity);
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity);
        Task<TEntity> MergeAsync<TEntity>(TEntity entity);
        Task DeleteAsync<TEntity>(TEntity entity);
        IUnitOfWork UnitOfWork { get; set; }
        Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null);
        ISession GetSession { get; set; }
        Task<TEntity> SaveOrUpdateAsync<TEntity>(TEntity entity);
        Task FlushChangesAsync();
        Task<TEntity> GetAsync<TEntity>(object id);
        Task<TEntity> RefreshAsync<TEntity>(object value);
        ISession OpenSession<TSessionContext>(ISessionProvider provider) where TSessionContext : ISessionContext;
        void ClearSession();
        
    }
}
