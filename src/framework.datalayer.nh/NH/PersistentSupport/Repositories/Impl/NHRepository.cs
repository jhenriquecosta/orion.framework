using NHibernate;
using NHibernate.Linq;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.DataLayer.Repositories.Abstracts;
using Orion.Framework.DataLayer.Repositories.Contracts;
using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.NH.Repositories
{

    public class NHRepository<TSessionContext, TEntity> : NHRepository<TSessionContext, TEntity, int>, IRepository<TEntity>
    where TEntity : class, IEntity<TEntity, int>
    where TSessionContext : ISessionContext
    {
        public NHRepository(ISessionProvider provider) : base(provider)
        {
            var typeContext = typeof(TSessionContext).Name;
           
        }

    }
    public class NHRepository<TSessionContext, TEntity, TPrimaryKey> : AbstractRepository<TEntity, TPrimaryKey>, IRepositoryWithSession
       where TEntity : class, IEntity<TEntity, TPrimaryKey>
       where TSessionContext : ISessionContext
    {
        private readonly ISessionProvider _sessionProvider;

        public NHRepository(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public virtual ISession Session => _sessionProvider.GetSession<TSessionContext>();

        public override IQueryable<TEntity> AsQueryable()
        {
            return Session.Query<TEntity>();
        }
        public override IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? AsQueryable() : AsQueryable().Where(filter);
        }
        public override List<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetAll(filter).ToList();
        }
        public override async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await GetAll(filter).ToListAsync();
        }
         
        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return Session.Get<TEntity>(id);
        }

        public override TEntity Load(TPrimaryKey id)
        {
            return Session.Load<TEntity>(id);
        }

        public override TEntity Insert(TEntity entity)
        {
            Session.Save(entity);
            return entity;
        }

        public override TEntity InsertOrUpdate(TEntity entity)
        {
            Session.SaveOrUpdate(entity);
            return entity;
        }

        public override Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return Task.FromResult(InsertOrUpdate(entity));
        }

        public override TEntity Update(TEntity entity)
        {
            Session.Update(entity);
            return entity;
        }
        public override async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Session.UpdateAsync(entity);
            return entity;
        }

        #region Delete
        public override void Delete(TPrimaryKey id)
        {
            Delete(Session.Load<TEntity>(id));
        }
         public override void Delete(TEntity entity)
        {
            if (entity is ISoftDelete delete)
            { 
                delete.IsDeleted = true;
                Update(entity);
            }
            else Session.Delete(entity);
        }
        public override async Task DeleteAsync(TPrimaryKey id)
        {
             await DeleteAsync(await Session.LoadAsync<TEntity>(id));
            
        }
        public override async Task DeleteAsync(TEntity entity)
        {
            if (entity is ISoftDelete delete)
            {
                delete.IsDeleted = true;
                await UpdateAsync(entity);
            }
            else await Session.DeleteAsync(entity);
        }
       
        #endregion

        public ISession GetSession()
        {
            return Session;
        }
    }
}
