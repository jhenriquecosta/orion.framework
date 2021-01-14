using System;
using System.Reflection;
using Orion.Framework.DataLayer.NH.Fluent;
using Orion.Framework.Exceptions;
using Orion.Framework.Helpers;

namespace Orion.Framework.Domains.Entities
{

    /// <summary>
    /// Some helper methods for entities.
    /// </summary>
    public static class EntityHelper
    {
        public static bool IsEntity(Type type)
        {
            return ReflectionHelper.IsAssignableToGenericType(type, typeof (IEntity<>));
        }

        public static Type GetPrimaryKeyType<TEntity>()
        {
            return GetPrimaryKeyType(typeof (TEntity));
        }

        /// <summary>
        /// Gets primary key type of given entity type
        /// </summary>
        public static Type GetPrimaryKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof (IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IKey<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }
            string aspnetIdentity = "Microsoft.AspNetCore.Identity";
            if (entityType.BaseType.FullName.Contains(aspnetIdentity))
            {
                var args = entityType.BaseType.GenericTypeArguments[0];
                return args;
            }

            //var mapTable = entityType.GetTypeInfo().GetAttribute<MapTableAttribute>();
            //if (mapTable.PrimaryKey != null)
            //{

            //   return entityType.GenericTypeArguments[0];
            //}


            throw new Warning("Can not find primary key type of given entity type: " + entityType + ". Be sure that this entity type implements IEntity<TPrimaryKey> interface");
        }
    }
}