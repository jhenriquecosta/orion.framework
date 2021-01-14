using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Orion.Framework.DataLayer.NHibernate.Fluent.Conventions
{
    /// <summary>
    /// ColumnNameConvention class.  IComponentConventionAcceptance
    /// </summary>
    public class ColumnNameComponentConvention :  IPropertyConvention, IPropertyConventionAcceptance
    {
        #region Public Methods
       
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(c =>
            {
                var x = c.Name;
                var ret = c.EntityType.IsNHibernateComponent();
                return ret;
            });
        }
        public void Apply(IPropertyInstance instance)
        {
            var columnName = instance.GetColumnAttribute();
            if (instance.Property.PropertyType.IsEnum || instance.Property.PropertyType.IsNullableEnum())
            {
                columnName = GetColumnNameForEnum(instance.Property.Name).ToPascalCase();

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
        private string GetColumnNameForEnum(string defaultColumnName)
        {
            return $"{defaultColumnName}";
        }
        #endregion
    }
}
