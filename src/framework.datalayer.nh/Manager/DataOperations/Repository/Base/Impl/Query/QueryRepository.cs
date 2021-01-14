using NHibernate;
using NHibernate.Linq;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Proxy;
using Framework.DataLayer.NHibernate.DataLayer.Stores;
using Framework.DataLayer.NHibernate.Domains;
using Framework.DataLayer.NHibernate.Domains.Repositories;
using Framework.DataLayer.NHibernate.Validations.Aspects;
using Framework.DataLayer.NHibernate.DataLayer.UnitOfWorks;

namespace Framework.DataLayer.NHibernate.Repository.Base
{


    // [ServiceInterceptor(typeof(NHUnitOfWorkAttribute))]
    public abstract partial class RepositoryBase<TEntity, TKey> : IStore<TEntity, TKey> where TEntity : class, IKey<TKey>
    {
      

        #region Query
        protected IQueryable<TEntity> Query(IQueryable<TEntity> queryable, IQueryBase<TEntity> query)
        {
            queryable = queryable.Where(query);
            var order = query.GetOrder();
            return string.IsNullOrWhiteSpace(order) ? queryable : queryable.OrderBy(order);
        }
        public List<TEntity> Query(IQueryBase<TEntity> query)
        {
            return Query(Find(), query).ToList();
        }

        public async Task<List<TEntity>> QueryAsync(IQueryBase<TEntity> query)
        {
            return await Query(Find(), query).ToListAsync();

        }

        #endregion

    }
}
