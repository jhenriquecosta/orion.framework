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
        #region IFindByIds
        public List<TEntity> FindByIds(params TKey[] ids)
        {
            return FindByIds((IEnumerable<TKey>)ids);

        }

        public List<TEntity> FindByIds(IEnumerable<TKey> ids)
        {
            if (ids == null)
                return null;
            return FindAll(t => ids.Contains(t.Id)).ToList();

        }

        public List<TEntity> FindByIds(string ids)
        {
            var idList = Framework.DataLayer.NHibernate.Helpers.TypeConvert.ToList<TKey>(ids);
            return FindByIds(idList);

        }
        #endregion
        #region IFindByIdsAsync
        public async Task<List<TEntity>> FindByIdsAsync(params TKey[] ids)
        {
            return await FindByIdsAsync((IEnumerable<TKey>)ids);
        }

        public async Task<List<TEntity>> FindByIdsAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            if (ids == null)
                return null;
            return await Find(t => ids.Contains(t.Id)).ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> FindByIdsAsync(string ids)
        {
            var idList = Framework.DataLayer.NHibernate.Helpers.TypeConvert.ToList<TKey>(ids);
            return await FindByIdsAsync(idList);

        }

        #endregion

    }
}
