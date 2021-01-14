using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.NH.Fluent.Conventions
{
    /// <summary>
    /// SchemaConvention class.
    /// </summary>
    public class SchemaConvention : IClassConvention
    {
        #region Public Methods

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <param name="type">The type to get module name for.</param>
        /// <returns>The module name.</returns>
        public static string GetModuleName ( Type type )
        {
            var attr = type.GetAttribute<MapTableAttribute>();

            //var schema_catalog = AppHelper.GetCache<string>("catalog");
            //if (schema_catalog != null)
            //{
            //    var schema = "dbo";
            //    if (attr != null)
            //    {
            //        if (!attr.Schema.IsEmpty()) schema = attr.Schema;
            //    }
            //    schema_catalog = $"{schema_catalog}.{schema}";
            //    return schema_catalog;
            //}

            return attr?.Schema ?? "";
        }

        /// <summary>
        /// Applies the schema name for the module.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IClassInstance instance )
        {
            var type = instance.EntityType;
            var moduleName = GetModuleName(type).ToLower();
            if (!string.IsNullOrWhiteSpace(moduleName))
            {
                instance.Schema(moduleName);
            }
        }

        #endregion
    }
}
