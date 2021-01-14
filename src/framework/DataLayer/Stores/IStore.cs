using System;
using Orion.Framework.DataLayer.Stores.Operations;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores 
{

    public interface IStore<TEntity> : IStore<TEntity, int> where TEntity : class, IKey<int>, IVersion
    {
    }

    public interface IStore<TEntity, in TKey> : IQueryStore<TEntity, TKey>,
            IAdd<TEntity, TKey>,
            IAddAsync<TEntity, TKey>,
            IUpdate<TEntity, TKey>,
            IUpdateAsync<TEntity, TKey>,
            IRemove<TEntity, TKey>,
            IRemoveAsync<TEntity, TKey>

        //IInsert<TEntity, TKey>,
        //IInsertOrUpdate<TEntity, TKey>,
        //IInsertOrUpdateAsync<TEntity, TKey>,
        //IInsertAsync<TEntity, TKey>,
        //IUpdate<TEntity, TKey>, 
        //IUpdateAsync<TEntity, TKey>,
        //IDelete<TEntity, TKey>, 
        //IDeleteAsync<TEntity, TKey>

        where TEntity : class, IKey<TKey>
    {}
}
