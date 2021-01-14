using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Orion.Framework.Domains.Attributes;

namespace Orion.Framework.Domains
{
   

    #region Entity
 

    //public abstract class Entity<TEntity> : BaseEntity<TEntity> where TEntity : IBaseEntity
    //{

    //}
    //public abstract class Entity : BaseEntity, IBaseEntity
    //{

    //}
   
    //public abstract class Entity<TEntity,TKey> : BaseEntity<TEntity, TKey>, IBaseEntity<TEntity, TKey> where TEntity : IBaseEntity
    //{

    //}
    //public abstract class Entity<TEntity> : BaseEntity<TEntity, int> where TEntity : IBaseEntity
    //{

    //}
    //public abstract class Entity : BaseEntity, IBaseEntity
    //{

    //}

    public class DomainEntity : BaseEntity
    {

    }

   
    #endregion


}
