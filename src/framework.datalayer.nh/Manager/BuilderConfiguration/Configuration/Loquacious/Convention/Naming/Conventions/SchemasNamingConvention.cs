using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// Defines the different naming conventions used when generating database schema names
    /// </summary>
    public enum SchemasNamingConvention
    {
        /// <summary>
        /// Default NH MCC
        /// </summary>
        Default,

        /// <summary>
        /// Custom naming convention
        /// </summary>
        Custom,

        /// <summary>
        /// The unqualified assembly name
        /// </summary>
        AssemblyName,

        /// <summary>
        /// The unqualified namespace name
        /// </summary>
        NamespaceName,
        Attribute
    }
}
