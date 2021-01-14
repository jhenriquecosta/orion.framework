using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orion.Framework.Exceptions;
using Orion.Framework.Domains;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;

namespace Orion.Framework.DataLayer.NH.Dao
{

    public abstract class NhDao : IDao, IDisposable
    {
        private static ISession _session;
        private static IUnitOfWork _uow;
        public NhDao(ISession session)
        {
            _session = session;
        }

        public ISession OpenSession<TSessionContext>(ISessionProvider provider) where TSessionContext : ISessionContext
        {
            var session = provider.GetSession<TSessionContext>();
            _session = session;
            return _session;
        }
        public ISession GetSession 
        {
            get { return _session; }           
            set { value = _session; } 
        }
        public IUnitOfWork UnitOfWork
        {
            get { return _uow; }
            set { value = _uow; }
        }
        public virtual void ClearSession()
        {
             _session.Clear();
         
        }
        public virtual async Task DeattachAsync<TEntity>(TEntity obj)
        {
            await _session.EvictAsync(obj);
        }
        public virtual async Task FlushChangesAsync()
        {
            await _session.FlushAsync();
            _session.Clear();
        }
       
         

        public virtual async Task DeleteAsync<TEntity>(TEntity obj)
        {
            try
            {
                 await _session.DeleteAsync(obj);
                
            }
            catch (Exception ex)
            {
                var erro = $"Erro ao deletar o objecto {typeof(TEntity).FullName} \r {ex.Message}";
                throw new Warning(erro);
            }
        }
      
        public virtual async Task<TEntity> SaveOrUpdateAsync<TEntity>(TEntity obj)
        {
            var entity = obj as IEntity;
            obj = entity.IsTransient() ? await SaveAsync(obj) : await MergeAsync(obj);
            return obj;
        }
        public virtual async Task<TEntity> SaveAsync<TEntity>(TEntity obj)
        {
            try
            {
                //using var session = _sessionFactory.OpenSession();
                var result = await _session.SaveAsync(obj);
                return obj;

            }
            catch (Exception ex)
            {
                var erro = $"Erro ao carregar o objecto {typeof(TEntity).FullName} \r {ex.Message}";
                throw new Warning(erro);
            }
        }
        public virtual async Task<TEntity> UpdateAsync<TEntity>(TEntity obj)
        {
            try
            {
                await _session.UpdateAsync(obj);
                return obj;

            }
            catch (Exception ex)
            {
                var erro = $"Erro ao carregar o objecto {typeof(TEntity).FullName} \r {ex.Message}";
                throw new Warning(erro);
            }
        }
        public virtual async Task<TEntity> MergeAsync<TEntity>(TEntity obj)
        {
            try
            {
                var result = await _session.MergeAsync(obj);
                return obj;

            }
            catch (Exception ex)
            {
                var erro = $"Erro ao carregar o objecto {typeof(TEntity).FullName} \r {ex.Message}";
                throw new Warning(erro);
            }
        }
        public virtual async Task<TEntity> LoadAsync<TEntity>(object id)
        {
            try
            {
               
                var result = await _session.LoadAsync<TEntity>(id);
                return result;
            }
            catch (Exception ex)
            {
                var erro = $"Erro ao carregar o objecto {typeof(TEntity).FullName} \r {ex.Message}";
                throw new Warning(erro);
            }
        }
        public virtual async Task<TEntity> GetAsync<TEntity>(object id)
        {
            try
            {
                //using var session = this.OpenSession();
                var result = await _session.GetAsync<TEntity>(id);
                return result;
            }
            catch (Exception ex)
            {
                var erro = $"Erro ao carregar o objecto {typeof(TEntity).FullName} \r {ex.Message}";
                throw new Warning(erro);
            }
        }
        public virtual async Task<TEntity> RefreshAsync<TEntity>(object value)
        {
            try
            {
               
                await _session.RefreshAsync(value);
                return (TEntity)value;
            }
            catch (Exception ex)
            {
                var erro = $"Erro ao carregar o objecto {typeof(TEntity).FullName} \r {ex.Message}";
                throw new Warning(erro);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
              
                var result = filter == null ? _session.Query<TEntity>() : _session.Query<TEntity>().Where(filter);
                await _session.EvictAsync(result);
                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                var erro = $"Erro ao carregar o objecto {typeof(TEntity).FullName} \r {ex.Message}";
                throw new Warning(erro);
            }

        }
        public virtual async Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>>  filter)
        {
            try
            {
               // using var session = this.OpenSession();
                var result = await _session.Query<TEntity>().FirstOrDefaultAsync(filter);
                await _session.EvictAsync(result);
                return  result;
            }
            catch (Exception ex)
            {
                var erro = $"Erro ao carregar o objecto {typeof(TEntity).FullName} \r {ex.Message}";
                throw new Warning(erro);
            }

        }
        public virtual async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {  
                var result = filter == null ? await _session.Query<TEntity>().CountAsync() : await _session.Query<TEntity>().CountAsync(filter);
                 return result;
            }
            catch (Exception ex)
            {
                var erro = $"Erro ao carregar o objecto {typeof(TEntity).FullName} \r {ex.Message}";
                throw new Warning(erro);
            }

        }
        public virtual IQueryable<TEntity> AsQueryable<TEntity>(Expression<Func<TEntity, bool>> filter = null)
        {
            var result = filter == null ? _session.Query<TEntity>() : _session.Query<TEntity>().Where(filter);
            return result;
        }

        

        ~NhDao()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //NHibernateHelper.Instance.UnitOfWorkFactory().Remove(typeof(TEntity).FullName);
                //UnitOfWork.Dispose();
                _session.Close();
                _session.Dispose();              
                GC.SuppressFinalize(this);
            }
        }
    }
}
