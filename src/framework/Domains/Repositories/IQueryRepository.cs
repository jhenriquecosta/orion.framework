using System;
using Orion.Framework.DataLayer.Stores;

namespace Orion.Framework.Domains.Repositories {

    public interface IQueryRepository<TEntity> : IQueryRepository<TEntity, int> where TEntity : class, IAggregateRoot,IKey<int> {
    }

 
    public interface IQueryRepository<TEntity, in TKey> : IQueryStore<TEntity,TKey> where TEntity : class, IAggregateRoot,IKey<TKey> {
    }
}
