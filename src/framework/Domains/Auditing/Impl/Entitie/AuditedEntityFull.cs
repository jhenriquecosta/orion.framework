using System;
using System.ComponentModel.DataAnnotations.Schema;
using Orion.Framework.Domains.Attributes;

namespace Orion.Framework.Domains
{
    [Serializable]
    public abstract class AuditedEntityFull : AuditedEntityFull<IEntity>, IAuditedFull
    {

    }

    /// <summary>
    /// A shortcut of <see cref="CreationAuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class AuditedEntityFull<TEntity> : AuditedEntityFull<TEntity, int>, IAuditedFull, IEntity<TEntity, int> where TEntity : IEntity
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class AuditedEntityFull<TEntity, TKey> : AuditedEntity<TEntity, TKey>, IAuditedFull, IEntity<TEntity, TKey> where TEntity : IEntity
    {
        [ModelField(Ignore = true)]
        public virtual bool IsDeleted { get; set; }
        [ModelField(Ignore = true)]
        public virtual DateTime? DeletedOn { get; set; }
        [ModelField(Ignore = true)]
        public virtual string DeletedUser { get; set; }
    }
}