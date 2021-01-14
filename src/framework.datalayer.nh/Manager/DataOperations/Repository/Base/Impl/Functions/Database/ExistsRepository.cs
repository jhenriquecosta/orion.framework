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

        #region IExists

        public bool Exists(params TKey[] ids)
        {
            if (ids == null)
                return false;
            return Exists(t => ids.Contains(t.Id));
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return Find().Any(predicate);

        }

        public async Task<bool> ExistsAsync(params TKey[] ids)
        {
            if (ids == null)
                return false;
            return await ExistsAsync(t => ids.Contains(t.Id));

        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return false;
            return await Find().AnyAsync(predicate);

        }
        #endregion

    }
}
