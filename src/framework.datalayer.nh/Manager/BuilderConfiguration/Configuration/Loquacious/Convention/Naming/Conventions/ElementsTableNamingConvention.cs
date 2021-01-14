using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// Defines the different naming convention used when mapping elements to database tables
    /// </summary>
    public enum ElementsTableNamingConvention
    {
        /// <summary>
        /// The element type name (default)
        /// </summary>
        ElementTypeName,

        /// <summary>
        /// The name of the element
        /// </summary>
        PropertyName,

        /// <summary>
        /// Custom user-defined naming convention
        /// </summary>
        Custom,

        /// <summary>
        /// The entity name appened to the element name
        /// </summary>
        EntityNameElementName,

        /// <summary>
        /// The entity name appened to the property name
        /// </summary>
        EntityNamePropertyName,

        /// <summary>
        /// The entity name appened to the element name separated by an underscore
        /// </summary>
        EntityName_ElementName,

        /// <summary>
        /// The entity name appened to the property name separated by an underscore
        /// </summary>
        EntityName_PropertyName,
    }
}
