using System;
using System.ComponentModel.DataAnnotations.Schema;
using Orion.Framework.Domains.Attributes;
using Orion.Framework.Timing;

namespace Orion.Framework.Domains
{
  
    [Serializable]
    public abstract class AuditedAggregateRootCreated : AuditedAggregateRootCreated<IAggregateRoot>, ICreatedAudited
    {

    }

    /// <summary>
    /// A shortcut of <see cref="CreationAuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class AuditedAggregateRootCreated<TEntity> : AuditedAggregateRootCreated<TEntity,int>,  ICreatedAudited, IAggregateRoot<TEntity, int> where TEntity : IAggregateRoot
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class AuditedAggregateRootCreated<TEntity,TKey> : AggregateRoot<TEntity,TKey>, ICreatedAudited, IAggregateRoot<TEntity, TKey> where TEntity : IAggregateRoot
    {

        [ModelField(Ignore = true)]
        public virtual DateTime? CreatedOn { get; set; }
        [ModelField(Ignore = true)]
        public virtual string CreatedUser { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AuditedAggregateRootCreated()
        {
            CreatedOn = Clock.Now;
        }
    }

     
}