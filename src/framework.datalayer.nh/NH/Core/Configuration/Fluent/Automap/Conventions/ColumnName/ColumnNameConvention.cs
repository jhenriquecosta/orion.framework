using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.NH.Fluent.Conventions
{
    /// <summary>
    /// ColumnNameConvention class.
    /// </summary>
    public class ColumnNameConvention : IComponentConvention, IPropertyConvention
    {
        #region Public Methods

        /// <summary>
        /// Applies the column name based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IComponentInstance instance )
        {
            var namingStrategy = instance.GetNamingStrategy ();
          
            foreach ( var propertyInstance in instance.Properties )
            {
                var columnName = namingStrategy.GetColumnName ( instance.Property, propertyInstance.Property, false );
                if ( propertyInstance.Property.PropertyType.IsEnum ||
                     propertyInstance.Property.PropertyType.IsNullableEnum () )
                {
                    columnName = GetColumnNameForEnum ( columnName );
                }
                if (columnName.IsNullOrEmpty())
                {
                    columnName = instance.Name;
                }
                var fieldName = instance.Property.MemberInfo.GetAttribute<FieldAttribute>();
                if (fieldName != null)
                {
                    columnName = fieldName.Name;
                }
                else
                {
                    columnName = columnName.ToPascalCaseWithUndescore(); //NamingHelper.ToUppercase(columnName);
                }
                columnName = columnName.ToLower();                           
                propertyInstance.Column ( columnName );
            }
        }

        /// <summary>
        /// Applies the column name based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IPropertyInstance instance )
        {
            var columnName = instance.GetColumnAttribute();
            if (!instance.Property.DeclaringType.IsNHibernateComponent())
            {
                if (instance.Property.PropertyType.IsEnum || instance.Property.PropertyType.IsNullableEnum())
                {
                    columnName = GetColumnNameForEnum(instance.Property.Name).ToPascalCase();
                    // instance.Column ( columnName );
                }


                if (columnName.IsNullOrEmpty())
                {
                    columnName = instance.Name;
                }
             
                var fieldName = instance.Property.MemberInfo.GetAttribute<FieldAttribute>();
                if (fieldName != null)
                {
                    columnName = fieldName.Name;
                }
                else
                {
                    columnName = columnName.ToPascalCaseWithUndescore(); //NamingHelper.ToUppercase(columnName);
                }
                columnName = columnName.ToLower();
                instance.Column(columnName);
            }
        }

        #endregion

        #region Methods

        private static string GetColumnNameForEnum ( string defaultColumnName )
        {
            return $"{defaultColumnName}";
        }

        #endregion
    }
}
