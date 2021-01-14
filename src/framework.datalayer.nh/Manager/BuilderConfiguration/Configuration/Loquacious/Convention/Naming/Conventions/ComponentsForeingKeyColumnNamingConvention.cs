using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// Defines the different naming conventions used when resolving the names for components foreign keys columns
    /// </summary>
    public enum ComponentsForeignKeyColumnNamingConvention
    {
        /// <summary>
        /// Default. The entity name appened to the word "key"
        /// </summary>
        Default,

        /// <summary>
        /// The entity name appened to name of the entity identifier property
        /// </summary>
        EntityNameIDPropertyName,

        /// <summary>
        /// A user-defined convention
        /// </summary>
        Custom
    }
}
