using Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Extensions;
using NHibernate.Cfg;
using NHibernate.Util;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// A default naming strategy that surrounds tables and columns names with square brackets
    /// </summary>
    public class ConventionNamingStrategy : INamingStrategy
    {
        /// <summary>
        /// Returns the unqualified class name according the strategy
        /// </summary>
        /// <param name="className">The class name</param>
        /// <returns>The unqualified class name</returns>
        public string ClassToTableName(string className)
        {
            return StringHelper.Unqualify(className);
        }

        /// <summary>
        /// Returns the unqualified property name according the strategy
        /// </summary>
        /// <param name="propertyName">The property name</param>
        /// <returns>The unqualified property name</returns>
        public string PropertyToColumnName(string propertyName)
        {
            return propertyName.AddSquareBrackets();
        }

        /// <summary>
        /// Returns the table name according the strategy
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <returns>The unqualified table name</returns>
        public string TableName(string tableName)
        {
            return tableName.AddSquareBrackets();
        }

        /// <summary>
        /// Return the column name according the strategy
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns>The unqualified column name</returns>
        public string ColumnName(string columnName)
        {
            return columnName.AddSquareBrackets();
        }

        /// <summary>
        /// Returns the table name  according the strategy
        /// </summary>
        /// <param name="className">The class name</param>
        /// <param name="propertyName">The property name</param>
        /// <returns>The unqualified table name</returns>
        public string PropertyToTableName(string className, string propertyName)
        {
            return StringHelper.Unqualify(propertyName);
        }

        /// <summary>
        /// Returns the logical column name according the strategy
        /// </summary>
        /// <param name="columnName">The column name</param>
        /// <param name="propertyName">The propety name</param>
        /// <returns></returns>
        public string LogicalColumnName(string columnName, string propertyName)
        {
            if (!StringHelper.IsNotEmpty(columnName))
                return StringHelper.Unqualify(propertyName);
            else
                return columnName;
        }

    }
}
