using System;
using System.ComponentModel.DataAnnotations.Schema;
using Orion.Framework.Domains.Attributes;
using Orion.Framework.Timing;

namespace Orion.Framework.Domains
{
  
    [Serializable]
    public abstract class AuditedAggregateRootFull : AuditedAggregateRootFull<IAggregateRoot>, IAuditedFull
    {

    }

    /// <summary>
    /// A shortcut of <see cref="CreationAuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class AuditedAggregateRootFull<TEntity> : AuditedAggregateRootFull<TEntity,int>, IAuditedFull, IAggregateRoot<TEntity, int> where TEntity : IAggregateRoot
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class AuditedAggregateRootFull<TEntity,TKey> : AuditedAggregateRoot<TEntity,TKey>, IAuditedFull, IAggregateRoot<TEntity, TKey> where TEntity : IAggregateRoot
    {
        [ModelField(Ignore = true)]
        public virtual bool IsDeleted { get; set; }

        [ModelField(Ignore = true)]
        public virtual DateTime? DeletedOn { get; set; }
        [ModelField(Ignore = true)]
        public virtual string DeletedUser { get; set; }

    }

     
}