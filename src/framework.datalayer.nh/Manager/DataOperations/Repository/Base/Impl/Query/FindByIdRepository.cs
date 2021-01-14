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
using Framework.DataLayer.NHibernate.Helpers;

namespace Framework.DataLayer.NHibernate.Repository.Base
{


    // [ServiceInterceptor(typeof(NHUnitOfWorkAttribute))]
    public abstract partial class RepositoryBase<TEntity, TKey> : IStore<TEntity, TKey> where TEntity : class, IKey<TKey>
    {
        #region IFindById
        public TEntity Find(object id, bool isDetached = false)
        {
            return FindAsync(id, isDetached).Result;
        }
        #endregion
        #region IFindByIdAsync
        public async Task<TEntity> FindAsync(object id, bool isDetached = false, CancellationToken cancellationToken = default)
        {

            if (isDetached)
            {
                return await NHibernateHelper.Instance.ExecuteAsync(session => unitOfWork.Session.Get<TEntity>(id));
            }
            else
            {
                return await unitOfWork.Session.GetAsync<TEntity>(id, cancellationToken);
            }

        }
        #endregion
    }
}
