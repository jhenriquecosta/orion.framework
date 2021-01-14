using System;
using Orion.Framework.DataLayer.Stores.Operations;
using Orion.Framework.Dependency;

namespace Orion.Framework.Domains.Repositories {
   
    public interface ICompactRepository<TEntity> : ICompactRepository<TEntity, Guid>
        where TEntity : class, IAggregateRoot, IKey<Guid>, IVersion {
    }

    public interface ICompactRepository<TEntity, in TKey> : IScopeDependency,
        IFindById<TEntity, TKey>, IFindByIdAsync<TEntity, TKey>,
        IFindByIds<TEntity, TKey>, IFindByIdsAsync<TEntity, TKey>,
        IExists<TEntity, TKey>, IExistsAsync<TEntity, TKey>,
        IAdd<TEntity, TKey>, IAddAsync<TEntity, TKey>,
        IUpdate<TEntity, TKey>, IUpdateAsync<TEntity, TKey>,
        IRemove<TEntity, TKey>, IRemoveAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot, IKey<TKey>, IVersion
    {
    }
    //public interface ICompactRepository<TEntity, in TKey> : IScopeDependency,
    //    IFindById<TEntity, TKey>,
    //    IFindByIdAsync<TEntity, TKey>,     
    //    IExists<TEntity, TKey>,
    //    IExistsAsync<TEntity, TKey>,
    //    IAdd<TEntity, TKey>,
    //    IAddAsync<TEntity, TKey>,
    //    IUpdate<TEntity, TKey>,
    //    IUpdateAsync<TEntity, TKey>,
    //    IRemove<TEntity, TKey>,
    //    IRemoveAsync<TEntity, TKey>
    //    where TEntity : class, IAggregateRoot, IKey<TKey>,IVersion {
    //}



}

     