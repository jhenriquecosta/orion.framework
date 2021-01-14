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
       
        #region IAdd
        public void Add([Valid] TEntity entity)
        {
            unitOfWork.Session.Save(entity);
        }

        public void Add([Valid] IEnumerable<TEntity> entities)
        {
            entities.ForEach(Add);
        }
        #endregion
        #region IAddAsync
        public async Task AddAsync([Valid] TEntity entity, CancellationToken cancellationToken = default)
        {
            await unitOfWork.Session.SaveAsync(entity, cancellationToken);
        }

        public async Task AddAsync([Valid] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await entities.ForEachAsync(async data => { await AddAsync(data, cancellationToken); });
        }
        #endregion     

    }
}
