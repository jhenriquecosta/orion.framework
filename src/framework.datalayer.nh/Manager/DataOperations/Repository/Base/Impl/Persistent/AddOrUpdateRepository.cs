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
using System.Text;

namespace Framework.DataLayer.NHibernate.Repository.Base
{


    // [ServiceInterceptor(typeof(NHUnitOfWorkAttribute))]
    public abstract partial class RepositoryBase<TEntity, TKey> : IStore<TEntity, TKey> where TEntity : class, IKey<TKey>
    {

        #region AddOrUpdate 

        public void AddOrUpdate([Valid] TEntity entity)
        {
            AddOrUpdateAsync(entity).Wait();
        }

        public async Task AddOrUpdateAsync([Valid] TEntity entity, CancellationToken cancellationToken = default)
        {

            try
            {
                var isproxy = entity.IsProxy();
                if (entity.Id.ToString().ToInt() == 0)
                {
                    await AddAsync(entity);
                }
                else
                {
                    await MergeAsync(entity);
                }
                

            }
            catch (Exception ex)
            {
                var erro = new StringBuilder();
                erro.AppendLine($"Erro ao salvar a entidade: {entity.Id}-{entity}");
                erro.AppendLine($"Data {DateTime.Now}");
                erro.AppendLine($"{ex.Message}");
                Logger.Warn(erro.ToString());
                throw;
            }

        }

        #endregion

    }
}
