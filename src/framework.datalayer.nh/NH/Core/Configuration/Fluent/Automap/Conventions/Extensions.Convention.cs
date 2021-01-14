using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Conventions.Instances;

namespace Orion.Framework.DataLayer.NH.Fluent.Conventions
{

    public static class IPropertyExtensions
    {
        public static void HasNullAttribute(this IPropertyInstance instance)
        {
            if (instance == null) return;

            if (instance.Property.MemberInfo.IsDefined(typeof(RequiredAttribute), false))
            {
                instance.Not.Nullable();
            }
        }
        public static void HasKeyAttribute(this IPropertyInstance instance)
        {
            if (instance == null) return;

            if (instance.Property.MemberInfo.IsDefined(typeof(KeyAttribute), false))
            {
                instance.Unique();
            }
        }
        public static void HasLengthAttributes(this IPropertyInstance instance)
        {
            if (instance == null) return;

            var max =  (from attribute
                    in instance.Property.MemberInfo.GetCustomAttributes(typeof(StringLengthAttribute), false)
                select (StringLengthAttribute)attribute into result
                select result.MaximumLength).FirstOrDefault();
            if (max > 0)
            {
                instance.CustomSqlType($"varchar({max})");
            }
        }
              
        public static string GetColumnAttribute(this IPropertyInstance instance)
        {
            var colName = string.Empty;
            if (instance == null) return colName;

            if (!instance.Property.MemberInfo.IsDefined(typeof(ColumnAttribute), false)) return colName;
            var column = instance.Property.MemberInfo.GetCustomAttribute<ColumnAttribute>(true);
            if (column != null)
            {
                colName = column.Name;
            }
            return colName;
        }
        public static string GetColumnAttribute(this IManyToOneInstance instance)
        {
            var colName = string.Empty;
            if (instance == null) return colName;

            if (!instance.Property.MemberInfo.IsDefined(typeof(ColumnAttribute), false)) return colName;
            var column = instance.Property.MemberInfo.GetCustomAttribute<ColumnAttribute>(true);
            if (column != null)
            {
                colName = column.Name;
            }
            return colName;
        }


    }
   

  

    

   


   
   

   

  


}
