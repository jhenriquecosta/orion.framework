using System;
using Orion.Framework.DataLayer.NH.Contracts;

namespace Orion.Framework.DataLayer.NH.Fluent
{
    /// <summary>
    /// Strategy for type name plus property name as column name naming.
    /// </summary>
    public class TypeNamePlusPropertyNameAsColumnNameNamingStrategy : IComponentNamingStrategy
    {
        #region Public Methods

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityPropertyType">Type of the entity property.</param>
        /// <param name="entityPropertyName">Name of the entity property.</param>
        /// <param name="valueObjectType">Type of the value object.</param>
        /// <param name="valueObjectPropertyType">Type of the value object property.</param>
        /// <param name="valueObjectPropertyName">Name of the value object property.</param>
        /// <param name="shortNameIndicator">If set to <c>true</c> [short name indicator].</param>
        /// <returns>The column name.</returns>
        public string GetColumnName (
            Type entityType,
            Type entityPropertyType,
            string entityPropertyName,
            Type valueObjectType,
            Type valueObjectPropertyType,
            string valueObjectPropertyName,
            bool shortNameIndicator = false )
        {
            return valueObjectType.Name + valueObjectPropertyName;
        }

        #endregion
    }
}
