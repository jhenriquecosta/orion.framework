using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Impl;
using System.Reflection;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Extensions
{
    /// <summary>
    /// Provides extension methods for the IClassAttributesMapper type
    /// </summary>
    internal static class IClassAttributesMapperExtensions
    {
        /// <summary>
        /// Gets the schema name from the class mapper
        /// </summary>
        /// <param name="classAttributesMapper">An instance that implements IClassAttributesMapper</param>
        /// <returns>The schema name</returns>
        public static string GetSchema(this IClassAttributesMapper classAttributesMapper)
        {
            string schemaName = null;
            if (classAttributesMapper.GetType().Equals(typeof(ClassMapper)))
            {
                var classMappingField = typeof(ClassMapper).GetField("classMapping", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);
                if (classMappingField != null)
                {
                    HbmClass hbmClass = classMappingField.GetValue(classAttributesMapper) as HbmClass;
                    if (hbmClass != null)
                    {
                        schemaName = hbmClass.schema;
                    }
                }
            }

            return schemaName;
        }
    }
}
