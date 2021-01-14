using NHibernate;
using Framework.DataLayer.NHibernate.Repository.Base;
using Framework.DataLayer.NHibernate.DataLayer.Stores;
using Framework.DataLayer.NHibernate.DataLayer.UnitOfWorks;
using Framework.DataLayer.NHibernate.Domains;
using Framework.DataLayer.NHibernate.Domains.Repositories;
using NLog;

namespace Framework.DataLayer.NHibernate.Repository
{

    public abstract class Repository<TEntity> : Repository<TEntity, int>, IRepository<TEntity>, IUnitOfWorkRepository where TEntity : class, IAggregateRoot<TEntity, int>
    {
        protected Repository(IUnitOfWorkFactory unitOfWork) : base(unitOfWork)
        {
        }
    }

    public abstract class Repository<TEntity,TKey> : RepositoryBase<TEntity, TKey>, IRepository<TEntity, TKey> where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        protected Repository(IUnitOfWorkFactory unitOfWork) : base(unitOfWork)
        {
        }
        
    }
}

