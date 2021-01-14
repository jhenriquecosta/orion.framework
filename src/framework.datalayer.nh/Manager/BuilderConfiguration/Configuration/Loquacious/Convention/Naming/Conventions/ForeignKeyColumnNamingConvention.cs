using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// Defines the different naming conventions for foreign key columns
    /// </summary>
    public enum ForeignKeyColumnNamingConvention
    {
        /// <summary>
        /// The relationship property name
        /// </summary>
        Default,

        /// <summary>
        /// Custom user defined convention
        /// </summary>
        Custom,

        /// <summary>
        /// The relationship property name appended to the name of the target ID property
        /// </summary>
        PropertyNameIdPropertyName,

        /// <summary>
        /// The relationship property name appended to the name of the target ID property (both separated by an underscore character)
        /// </summary>
        PropertyName_IdPropertyName
    }
}
