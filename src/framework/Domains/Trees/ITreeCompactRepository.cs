using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orion.Framework.DataLayer.Stores.Operations;
using Orion.Framework.Domains.Repositories;

namespace Orion.Framework.Domains.Trees {
  
    public interface ITreeCompactRepository<TEntity> : ITreeCompactRepository<TEntity, Guid, Guid?> where TEntity : class, ITreeEntity<TEntity, Guid, Guid?> {
    }

   
    public interface ITreeCompactRepository<TEntity, in TKey, in TParentId> : ICompactRepository<TEntity, TKey>,IFindByIdNoTrackingAsync<TEntity,TKey>
        where TEntity : class, ITreeEntity<TEntity, TKey, TParentId> {
      
        Task<int> GenerateSortIdAsync( TParentId parentId );
    
        Task<List<TEntity>> GetAllChildrenAsync( TEntity parent );
    }
}
