using System;
using Orion.Framework.Domains.Repositories;

namespace Orion.Framework.Domains.Trees {

    public interface ITreeRepository<TEntity> : ITreeRepository<TEntity, Guid, Guid?> where TEntity : class, ITreeEntity<TEntity, Guid, Guid?>
    {
    }

   
    public interface ITreeRepository<TEntity, in TKey, in TParentId> : IRepository<TEntity, TKey>, ITreeCompactRepository<TEntity, TKey, TParentId>
        where TEntity : class, ITreeEntity<TEntity, TKey, TParentId> {
    }
}
