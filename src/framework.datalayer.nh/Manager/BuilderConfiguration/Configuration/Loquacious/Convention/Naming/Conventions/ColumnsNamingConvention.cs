using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// Defines the different naming conventions used when generating table column names
    /// </summary>
    public enum ColumnsNamingConvention
    {
        /// <summary>
        /// Default naming convention
        /// </summary>
        Default,

        /// <summary>
        /// Custom naming convention
        /// </summary>
        Custom,

        /// <summary>
        /// Pascal case naming convention
        /// </summary>
        PascalCase,

        /// <summary>
        /// Camel case naming convention
        /// </summary>
        CamelCase,

        /// <summary>
        /// Uppercase underscore separated naming convention
        /// </summary>
        Uppercase,

        /// <summary>
        /// Lowercase underscore separated naming convention
        /// </summary>
        Lowercase
    }
}
