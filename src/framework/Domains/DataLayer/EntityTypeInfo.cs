using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Domains
{
    public class EntityTypeInfo
    {
        /// <summary>
        /// Type of the entity.
        /// </summary>
        public Type EntityType { get;  set; }

        /// <summary>
        /// DbContext type that has DbSet property.
        /// </summary>
        public Type EntitySessionContext { get; set; }

        //public EntityTypeInfo(Type entityType, Type context)
        //{
        //    EntityType = entityType;
        //    EntitySessionContext = context;
        //}
    }
}
