using System;
using System.Collections.Generic;
using System.Text;
using Orion.Framework.Domains.Trees;

namespace Orion.Framework.Domains
{



    public abstract class TreeAggregateRootAudited<TEntity> : TreeAggregateRootAudited<TEntity,Guid,Guid?> where TEntity : ITreeEntity<TEntity, Guid,Guid?>
    {

    }
    public abstract class TreeAggregateRootAudited<TEntity, TKey,TParentId> : TreeEntityBase<TEntity, TKey, TParentId> where TEntity : ITreeEntity<TEntity, TKey, TParentId>
    {
        public virtual DateTime? CreatedTime { get; set; }
        public virtual int? CreatedUser { get; set; }
        public virtual DateTime? ChangedTime { get; set; }
        public virtual int? ChangedUser { get; set; }
    }
  
}
