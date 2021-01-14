using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System;
using Orion.Framework.DataLayer.NH.Events.Filters;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.NH.Fluent.Conventions
{
    /// <summary>
    /// TableNameConvention class.
    /// </summary>
    public class TableNameConvention : IClassConvention
    {
        #region Public Methods

        public static string GetTableName(Type type )
        {
            var strName = type.Name.ToPascalCase();
          
            var tableName = strName;   //.ToSentenceCase().ToLower();
            var attr = type.GetAttribute<MapTableAttribute>();
            
            tableName = attr?.Table ?? tableName;
            return tableName;
        }
        /// <summary>
        /// Applies the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IClassInstance instance )
        {
            
            var tableName = GetTableName(instance.EntityType).ToLower();            
            instance.Table($"{tableName}");
           
            instance.ApplyFilter<AppFilterSoftDelete>();
           
        }

        #endregion
    }
}
