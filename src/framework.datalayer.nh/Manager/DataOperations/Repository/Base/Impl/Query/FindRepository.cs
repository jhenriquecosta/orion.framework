using System;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Framework.DataLayer.NHibernate.DataLayer.Stores;
using Framework.DataLayer.NHibernate.Domains;
using Framework.DataLayer.NHibernate.Domains.Repositories;

namespace Framework.DataLayer.NHibernate.Repository.Base
{


    // [ServiceInterceptor(typeof(NHUnitOfWorkAttribute))]
    public abstract partial class RepositoryBase<TEntity, TKey> : IStore<TEntity, TKey> where TEntity : class, IKey<TKey>
    {
        #region IFindQueryable
        public IQueryable<TEntity> Find()
        {
            return unitOfWork.Session.Query<TEntity>();
        }

        public IQueryable<TEntity> Find(ICriteria<TEntity> criteria)
        {
            return Find().Where(criteria);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Find().Where(predicate);
        }
        #endregion

    }
}
