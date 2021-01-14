using System.Collections.Generic;

namespace Orion.Framework.Domains.Trees 
{

    public interface ITreeEntity<in TEntity, TKey, TParentId> : IAggregateRoot<TEntity, TKey>, IParentId<TParentId>, IPath, IEnabled, ISortId where TEntity : ITreeEntity<TEntity, TKey, TParentId>
    {

        void InitPath();

        void InitPath(TEntity parent);

        List<TKey> GetParentIdsFromPath(bool excludeSelf = true);
    }
}
