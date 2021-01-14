using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention
{
    /// <summary>
    /// Defines the different conditions used to determine if a given relationship is many to many
    /// </summary>
    public enum ManyToManyRelationshipsCondition
    {
        /// <summary>
        /// Default behavior. Many to many relationships are mapped as two one to many relationships.
        /// </summary>
        Never,

        /// <summary>
        /// Each side of the relationship has a public property whose name is equal to the type name of the other side of the relationship
        /// </summary>
        RelationshipPropertyNamesEqualToEntityNames,

        /// <summary>
        /// Each side of the relationship has a public property whose name is equal to the pluralized type name of the other side of the relationship
        /// </summary>
        RelationshipPropertyNamesEqualToPluralizedEntityNames,

        /// <summary>
        /// Each side of the relationship has a public property whose collection generic type is equal to the type of the other side of the relationship
        /// </summary>
        RelationshipPropertyTypeEqualToEntityType,
    }
}
