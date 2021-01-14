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


        #region IUpdate
        public void Update([Valid] TEntity entity)
        {

            unitOfWork.Session.Update(entity);
        }

        public void Update([Valid] IEnumerable<TEntity> entities)
        {
            entities.ForEach(save => Update(save));
        }
        #endregion
        #region IUpdateAsync
        public async Task MergeAsync([Valid] TEntity entity)
        {
            try
            {
                await unitOfWork.Session.MergeAsync(entity);
            }
            catch (Exception ex)
            {
                var erro = new StringBuilder();
                erro.AppendLine($"Erro na acão MERGE  na entidade: {entity.Id}-{entity}");
                erro.AppendLine($"Data {DateTime.Now}");
                erro.AppendLine($"{ex.Message}");
                Logger.Warn(erro.ToString());
                throw;
            }
        }

        public async Task UpdateAsync([Valid] TEntity entity)
        {
            try
            {
                await AddOrUpdateAsync(entity); //unitOfWork.ActiveSession.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                var erro = new StringBuilder();
                erro.AppendLine($"Erro na acão UPDATE  na entidade: {entity.Id}-{entity}");
                erro.AppendLine($"Data {DateTime.Now}");
                erro.AppendLine($"{ex.Message}");
                Logger.Warn(erro.ToString());
                throw;
            }
        }

        public async Task UpdateAsync([Valid] IEnumerable<TEntity> entities)
        {
            await entities.ForEachAsync(async data => { await UpdateAsync(data); });
        }

        #endregion

    }
}
