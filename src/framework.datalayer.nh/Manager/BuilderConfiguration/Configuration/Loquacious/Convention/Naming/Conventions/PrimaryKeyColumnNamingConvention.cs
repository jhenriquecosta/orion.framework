using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// Defines the different naming conventions for primary key columns
    /// </summary>
    public enum PrimaryKeyColumnNamingConvention
    {
        /// <summary>
        /// The name of the ID property
        /// </summary>
        Default,

        /// <summary>
        /// Custom user defined convention
        /// </summary>
        Custom,

        /// <summary>
        /// The name of the Entity appended to the name of the ID property
        /// </summary>
        EntityNameIdPropertyName,

         /// <summary>
        /// The name of the Entity appended to the name of the ID property (both separated by an underscore)
        /// </summary>
        EntityName_IdPropertyName
    }
}
