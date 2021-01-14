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
        #region ReadOnly
        #region IFindAsNoTracking
    
        #endregion

     
     

        public Task<TEntity> FindNoTrackingAsync(TKey id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindByIdsNoTracking(params TKey[] ids)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindByIdsNoTracking(IEnumerable<TKey> ids)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindByIdsNoTracking(string ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> FindByIdsNoTrackingAsync(params TKey[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> FindByIdsNoTrackingAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> FindByIdsNoTrackingAsync(string ids)
        {
            throw new NotImplementedException();
        }
        public Task<PagerList<TEntity>> PagerQueryAsNoTrackingAsync(IQueryBase<TEntity> query)
        {
            throw new NotImplementedException();
        }
        public Task<List<TEntity>> QueryAsNoTrackingAsync(IQueryBase<TEntity> query)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
