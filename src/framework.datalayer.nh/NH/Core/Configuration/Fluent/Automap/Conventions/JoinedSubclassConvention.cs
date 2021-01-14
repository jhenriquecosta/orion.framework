using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Orion.Framework.Domains;
using Orion.Framework.Helpers;
using Orion.Framework.Settings;

namespace Orion.Framework.DataLayer.NH.Fluent.Conventions
{
    /// <summary>
    /// JoinedSubclassConvention class.
    /// </summary>
    public class JoinedSubclassConvention : IJoinedSubclassConvention
    {
        #region Public Methods

        /// <summary>
        /// Adds Module namespace to schema.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IJoinedSubclassInstance instance )
        {
             
            var type = instance.EntityType;
            var schema = SchemaConvention.GetModuleName(type);
            var tableName = TableNameConvention.GetTableName(type);

            var attr = type.GetAttribute<MapTableAttribute>();
            var key = instance.EntityType.BaseType.Name;
            key = $"{key}_Id";
            key = attr?.PrimaryKey ?? key;
            key = key.ToLower();
            if (!schema.IsNullOrEmpty()) instance.Schema(schema);
            instance.Table($"{tableName}");         
            instance.Key.Column ( key );
        }

        #endregion
    }
}
