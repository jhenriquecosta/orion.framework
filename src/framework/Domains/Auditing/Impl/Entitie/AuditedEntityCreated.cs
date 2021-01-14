using System;
using System.ComponentModel.DataAnnotations.Schema;
using Orion.Framework.Domains.Attributes;
using Orion.Framework.Timing;

namespace Orion.Framework.Domains
{
  
    [Serializable]
    public abstract class AuditedEntityCreated : AuditedEntityCreated<IEntity>, ICreatedAudited
    {

    }

    /// <summary>
    /// A shortcut of <see cref="CreationAuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class AuditedEntityCreated<TEntity> : AuditedEntityCreated<TEntity,int>,  ICreatedAudited, IEntity<TEntity,int> where TEntity : IEntity
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class AuditedEntityCreated<TEntity,TKey> : EntityBase<TEntity,TKey>, ICreatedAudited, IEntity<TEntity, TKey> where TEntity : IEntity
    {
        [ModelField(Ignore = true)]
        public virtual DateTime? CreatedOn { get; set; }
        [ModelField(Ignore = true)]
        public virtual string CreatedUser { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AuditedEntityCreated()
        {
            CreatedOn = Clock.Now;
        }
    }

     
}