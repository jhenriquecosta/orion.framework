using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// Defines the different naming convention used when mapping components to database tables
    /// </summary>
    public enum ComponentsTableNamingConvention
    {
        /// <summary>
        /// Just the component name (default)
        /// </summary>
        ComponentName,

        /// <summary>
        /// The name of the relationships between the parent entity and the component
        /// </summary>
        RelationshipName,

        /// <summary>
        /// Custom user-defined naming convention
        /// </summary>
        Custom,

        /// <summary>
        /// The entity name appened to the component name
        /// </summary>
        EntityNameComponentName,

        /// <summary>
        /// The entity name appened to the relationship name
        /// </summary>
        EntityNameRelationshipName,

        /// <summary>
        /// The entity name appened to the component name separated by an underscore
        /// </summary>
        EntityName_ComponentName,

        /// <summary>
        /// The entity name appened to the relationship name separated by an underscore
        /// </summary>
        EntityName_RelationshipName,
    }
}
