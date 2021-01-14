using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Orion.Framework.Helpers;
using Orion.Framework.Properties;
using Orion.Framework.Sessions;
using Orion.Framework.Validations;
using Convert = System.Convert;

namespace Orion.Framework.Domains
{



    public abstract class EntityBase : EntityBase<IEntity> 
    {

    }
    public abstract class EntityBase<TEntity> : EntityBase<TEntity, int> where TEntity : IEntity
    {
       
    }

    [Serializable]
    public abstract class EntityBase<TEntity, TKey> : DomainBase<TEntity>, IEntity<TEntity, TKey> where TEntity : IEntity
    {

        [Key]
        [DataMember]
        public virtual TKey Id { get; set; }
        public virtual void Init()
        {
            InitId();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void InitId()
        {
            if (typeof(TKey) == typeof(int) || typeof(TKey) == typeof(long))
                return;
            if (string.IsNullOrWhiteSpace(Id.SafeString()) || Id.Equals(default(TKey)))
                Id = CreateId();
        }


        #region OldEqualsAndTransient
        /// <inheritdoc />
        /// <summary>
        ///     Checks if this entity is transient (it has not an Id).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        public virtual bool IsTransient()
        {
            if (EqualityComparer<TKey>.Default.Equals(Id, default(TKey)))
            {
                return true;
            }


            if (typeof(TKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(TKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }
        public override bool Equals(object other)
        {
            return this == (other as EntityBase<TEntity, TKey>);
        }


        public override int GetHashCode()
        {
            return ReferenceEquals(Id, null) ? 0 : Id.GetHashCode();
        }


        public static bool operator ==(EntityBase<TEntity, TKey> left, EntityBase<TEntity, TKey> right)
        {
            if ((object)left == null && (object)right == null)
                return true;
            if (!(left is TEntity) || !(right is TEntity))
                return false;
            if (Equals(left.Id, null))
                return false;
            if (left.Id.Equals(default(TKey)))
                return false;
            return left.Id.Equals(right.Id);
        }


        public static bool operator !=(EntityBase<TEntity, TKey> left, EntityBase<TEntity, TKey> right)
        {
            return !(left == right);
        }
        #endregion

        protected virtual TKey CreateId() {
            return Orion.Framework.Helpers.TypeConvert.To<TKey>( Guid.NewGuid() );
        }

     
        protected virtual ISession Session => Ioc.Create<ISession>();
        protected override void Validate(ValidationResultCollection results)
        {
            ValidateId(results);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void ValidateId(ValidationResultCollection results)
        {
            if (typeof(TKey) == typeof(int) || typeof(TKey) == typeof(long))
                return;
            if (string.IsNullOrWhiteSpace(Id.SafeString()) || Id.Equals(default(TKey)))
                results.Add(new ValidationResult(Resources.IdIsEmpty));
        }


    }
}
