using System;
using System.ComponentModel.DataAnnotations.Schema;
using Orion.Framework.Domains.Attributes;

namespace Orion.Framework.Domains
{
    [Serializable]
    public abstract class AuditedEntity : AuditedEntity<IEntity>, IAudited
    {

    }

    /// <summary>
    /// A shortcut of <see cref="CreationAuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class AuditedEntity<TEntity> : AuditedEntity<TEntity, int>, IAudited, IEntity<TEntity, int> where TEntity : IEntity
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class AuditedEntity<TEntity, TKey> : AuditedEntityCreated<TEntity, TKey>, IAudited, IEntity<TEntity, TKey> where TEntity : IEntity
    {
        [ModelField(Ignore = true)]
        public virtual DateTime? ChangedOn { get; set; }
        [ModelField(Ignore = true)]
        public virtual string ChangedUser { get; set; }
    }
}