using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// Defines the different naming convention used when mapping components columns to tables columns
    /// </summary>
    public enum ComponentsColumnsNamingConvention
    {
        /// <summary>
        /// Just the component property name (default)
        /// </summary>
        PropertyName,

        /// <summary>
        /// The component name appended to the property name
        /// </summary>
        ComponentNamePropertyName,

        /// <summary>
        /// The component name appended to the property name and separated by an underscore char
        /// </summary>
        ComponentName_PropertyName,

        /// <summary>
        /// The entity property name name appended to the component property name
        /// </summary>
        EntityPropertyNameComponentPropertyName,

        /// <summary>
        /// The entity property name name appended to the component property name separated by an underscore char
        /// </summary>
        EntityPropertyName_ComponentPropertyName,

        /// <summary>
        /// Custom user-defined naming convention
        /// </summary>
        Custom,
    }
}
