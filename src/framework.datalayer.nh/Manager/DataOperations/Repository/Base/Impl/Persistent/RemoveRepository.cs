using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Framework.DataLayer.NHibernate.DataLayer.Stores;
using Framework.DataLayer.NHibernate.Domains;
using NLog;
using System.Text;

namespace Framework.DataLayer.NHibernate.Repository.Base
{


    // [ServiceInterceptor(typeof(NHUnitOfWorkAttribute))]
    public abstract partial class RepositoryBase<TEntity, TKey> : IStore<TEntity, TKey> where TEntity : class, IKey<TKey>
    {
        #region IRemove
        public void Remove(object id)
        {
            Remove(unitOfWork.Session.Load<TEntity>(id));
        }

        public void Remove(TEntity entity)
        {
            unitOfWork.Session.Delete(entity);

        }

        public void Remove(IEnumerable<TKey> ids)
        {
            if (ids == null)
                return;
            var list = Find(f => ids.Contains(f.Id));
            Remove(list);
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return;
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            if (!enumerable.Any())
                return;
            enumerable.ForEach(Remove);

        }
        #endregion
        #region IRemoveAsync

        public async Task RemoveAsync(object id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await unitOfWork.Session.LoadAsync<TEntity>(id);
                await RemoveAsync(entity, cancellationToken);
            }
            catch (Exception ex)
            {
                var erro = new StringBuilder();
                erro.AppendLine($"Erro: Exclusao de registro");
                erro.AppendLine($"Data {DateTime.Now}");
                erro.AppendLine($"{ex.Message}");
                Logger.Warn(erro.ToString());
                throw ex;
            }
        }

        public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await unitOfWork.Session.DeleteAsync(entity, cancellationToken);
            }
            catch (Exception ex)
            {
                var erro = new StringBuilder();
                erro.AppendLine($"Erro: Exclusao de registro");
                erro.AppendLine($"Entidade {entity.Id}-{entity}");
                erro.AppendLine($"Data {DateTime.Now}");
                erro.AppendLine($"{ex.Message}");
                Logger.Warn(erro.ToString());
                throw ex;
            }
        }

        public async Task RemoveAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            if (ids == null) return;
            try
            {
                await ids.ForEachAsync(async data =>
                {
                    var id = (object)data;
                    var delete = FindAsync(id);
                    await RemoveAsync(delete);
                });

                //await DeleteAsync(ids.ToList().Select(t => (object)t), cancellationToken);

            }
            catch (Exception ex)
            {
                var erro = new StringBuilder();
                erro.AppendLine($"Erro: Exclusao de registro em lote");
                erro.AppendLine($"Data {DateTime.Now}");
                erro.AppendLine($"{ex.Message}");
                Logger.Warn(erro.ToString());
                throw;
            }
        }

        public async Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null) return;
            try
            {
                await entities.ForEachAsync(async data =>
                {

                    await RemoveAsync(data);
                });

            }
            catch (Exception ex)
            {
                var erro = new StringBuilder();
                erro.AppendLine($"Erro: Exclusao de registro em lote");                
                erro.AppendLine($"Data {DateTime.Now}");
                erro.AppendLine($"{ex.Message}");
                Logger.Warn(erro.ToString());
                throw;
            }


        }


        #endregion

    }
}
