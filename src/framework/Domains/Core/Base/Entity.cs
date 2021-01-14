using System;
using System.Collections.Generic;
using System.Text;

namespace Zeus.Domains
{
    public class Entity<TEntity> : EntityBase<TEntity,int> where TEntity : IEntity
    {
   
    }
   
}
