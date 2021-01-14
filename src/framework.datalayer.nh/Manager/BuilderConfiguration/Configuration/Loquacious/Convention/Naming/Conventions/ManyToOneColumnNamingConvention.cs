using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// Defines the conventions used when mapping many to one relationships to database column names
    /// </summary>
    public enum ManyToOneColumnNamingConvention
    {
        /// <summary>
        /// Default naming convention
        /// </summary>
        Default,

        /// <summary>
        /// A user defined naming convention
        /// </summary>
        Custom, 

        /// <summary>
        /// The name of the target entity
        /// </summary>
        TargetEntityName,

        /// <summary>
        /// The name of the target entity appended to the target entity identifier property name
        /// </summary>
        TargetEntityNameIDPropertyName

    }
}
