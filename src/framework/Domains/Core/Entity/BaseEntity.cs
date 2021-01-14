using System;
using Orion.Framework.Domains.Attributes;
using Orion.Framework.Validations;

namespace Orion.Framework.Domains
{
    #region interface
    

    public interface IBaseEntity : IEntity, IAudited, IDelete, IUnitOrganization,ICloneable
    {
    }

    
    public interface IBaseEntity<out TKey> : IEntity<TKey>, IBaseEntity
    {
    }

    
    public interface IBaseEntity<in TEntity, out TKey> : IEntity<TEntity, TKey>, IBaseEntity<TKey> where TEntity : IBaseEntity
    {
    }

    #endregion

    public abstract class BaseEntity : BaseEntity<IBaseEntity>,IBaseEntity
    {

    }

    public abstract class BaseEntity<TEntity> : BaseEntity<TEntity, int> where TEntity : IBaseEntity
    {
        public override object Clone()
        {
            var entity = this.MemberwiseClone();
            return entity;
        }
    }

    public abstract class BaseEntity<TEntity,TKey> : EntityBase<TEntity,TKey>, IBaseEntity<TEntity, TKey> where TEntity : IBaseEntity
    {
        
        [ModelField(Ignore = true)]
        public virtual bool IsDeleted { get; set; }

        [ModelField(Ignore = true)]
        public virtual int? OrganizationCode { get; set; }
        
        [ModelField(Ignore = true)]
        public virtual DateTime? CreatedOn { get; set; }
        [ModelField(Ignore = true)]
        public virtual DateTime? ChangedOn { get; set; }
        [ModelField(Ignore = true)]
        public virtual DateTime? DeletedOn { get; set; }
        [ModelField(Ignore = true)]
        public virtual string CreatedByUser { get; set; }
        [ModelField(Ignore = true)]
        public virtual string ChangedByUser { get; set; }
        [ModelField(Ignore = true)]
        public virtual string DeletedByUser { get; set; }

        public virtual object Clone()
        {
            var entity =  this.MemberwiseClone();
            return entity;
        }
    }
  
   
}
