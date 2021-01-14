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
        public IQueryable<TEntity> FindAsNoTracking()
        {
            //return NHibernateHelper.Instance.Execute(session => unitOfWork.ActiveSession.Query<TEntity>());
            return unitOfWork.SessionReadOnly.Query<TEntity>();
            // return Find();
        }
        public TEntity FindNoTracking(TKey id)
        {
            return NHibernateHelper.Instance.Execute(session => unitOfWork.Session.Get<TEntity>(id), false);
        }
    }
}
