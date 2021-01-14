using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Framework.DataLayer.NHibernate.DataLayer.Stores;
using Framework.DataLayer.NHibernate.Domains;
using Framework.DataLayer.NHibernate.Domains.Repositories;

namespace Framework.DataLayer.NHibernate.Repository.Base
{


    // [ServiceInterceptor(typeof(NHUnitOfWorkAttribute))]
    public abstract partial class RepositoryBase<TEntity, TKey> : IStore<TEntity, TKey> where TEntity : class, IKey<TKey>
    {
     
        #region Find All Entities
        #region IFindAll
        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = Find();
            if (predicate != null) query = query.Where(predicate);
            return query.ToList();
        }

        public Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = Find();
            if (predicate != null) query = query.Where(predicate);
            return query.ToListAsync();

        }


        #endregion
        #region IPagerQuery
        public PagerList<TEntity> PagerQuery(IQueryBase<TEntity> query)
        {
            return Find().Where(query).ToPagerList(query.GetPager());

        }
        public async Task<PagerList<TEntity>> PagerQueryAsync(IQueryBase<TEntity> query)
        {
            var data = await Find().Where(query).ToPagerListAsync(query.GetPager());
            return data;
        }

        #endregion


        #endregion
    }
}
