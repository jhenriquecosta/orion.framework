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
        public TEntity UnProxy(TEntity entity)
        {
            INHibernateProxy proxy = entity as INHibernateProxy;
            if (proxy != null)
            {
                return (TEntity)proxy.HibernateLazyInitializer.GetImplementation();
            }

            return entity;
        }

        public virtual TEntity AsProxy(object id)
        {
            return unitOfWork.Session.Load<TEntity>(id, LockMode.None);
        }
        public virtual void Attach(TEntity entity)
        {
            unitOfWork.Session.Lock(entity, LockMode.None);
        }
        public virtual void DeAttach(TEntity entity)
        {
            unitOfWork.Session.Evict(entity);
        }

        public void Refresh(TEntity entity)
        {
            unitOfWork.Session.Refresh(entity);
        }
    }
}
