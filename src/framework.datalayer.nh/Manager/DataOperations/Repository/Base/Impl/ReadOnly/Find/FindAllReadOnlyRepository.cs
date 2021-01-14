using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Framework.DataLayer.NHibernate.DataLayer.Stores;
using Framework.DataLayer.NHibernate.Domains;

namespace Framework.DataLayer.NHibernate.Repository.Base
{


    // [ServiceInterceptor(typeof(NHUnitOfWorkAttribute))]
    public abstract partial class RepositoryBase<TEntity, TKey> : IStore<TEntity, TKey> where TEntity : class, IKey<TKey>
    {
        public List<TEntity> FindAllNoTracking(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = FindAsNoTracking();
            if (predicate != null) query = query.Where(predicate);
            query = query.EagerFetchAll();
            var list = query.ToList();
            return list;
        }

        public Task<List<TEntity>> FindAllNoTrackingAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = FindAsNoTracking();
            if (predicate != null) query = query.Where(predicate);
            query = query.EagerFetchAll();
            var list = query.ToListAsync();
            return list;
        }
    }
}
