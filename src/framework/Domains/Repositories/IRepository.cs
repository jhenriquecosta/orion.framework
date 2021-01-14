using Orion.Framework.DataLayer.Stores;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;

namespace Orion.Framework.Domains.Repositories
{

    public interface IRepository<TEntity, in TKey> : IQueryRepository<TEntity, TKey>, IStore<TEntity, TKey> where TEntity : class, IAggregateRoot, IKey<TKey>
    {
        IUnitOfWork UnitOfWork { get; }
    }
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IAggregateRoot, IKey<int>
    {

    }
}
