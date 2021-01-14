using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zeus.Dependency;
using Zeus.Domains;

namespace Zeus.NHibernate.Repository
{
    /// <summary>
    /// IRepository interface defines basic repository services.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// 

    public interface INHRepository<TEntity>  : IScopeDependency where TEntity :  IAggregateRoot
    {
        #region Get data
        TEntity FirstOrDefault(object id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        IList<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null, int limit = 0);
        #endregion

        #region save/update/delete data
        TEntity Save(TEntity entity);
        TEntity SaveOrUpdate(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        #endregion

        #region Query
        dynamic SQLQuery(string sqlQuery);
        IList<T> HQLQuery<T>(string query);
        //IList<TEntity> Query(IQueryBase<TEntity> query);
        //PagerList<TEntity> PagerQuery(IQueryBase<TEntity> query);
        #endregion

        #region async data
        Task<TEntity> SaveAsync(TEntity entity);
        Task<TEntity> SaveOrUpdateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        #endregion

    }

    
}
