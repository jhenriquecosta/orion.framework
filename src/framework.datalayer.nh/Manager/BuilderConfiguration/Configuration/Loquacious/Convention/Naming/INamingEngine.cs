using System;
using System.Reflection;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Naming
{
    /// <summary>
    /// Defines the components that builds names from entity types and relationships types
    /// </summary>
    public interface INamingEngine
    {
        /// <summary>
        /// Generates the name of a schema based on a given entity type
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <returns>The schema name</returns>
        string ToSchemaName(Type entityType);

        /// <summary>
        /// Generates the name of a schema based on a couple of related entities
        /// </summary>
        /// <param name="sourceType">The source type</param>
        /// <param name="targetType">The target type</param>
        /// <returns>The schema name</returns>
        string ToSchemaName(Type sourceType, Type targetType);

        /// <summary>
        /// Generates the name of a table for a given entity type according to the naming conventions configuration
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <returns>The table name</returns>
        string ToTableName(Type entityType);

        /// <summary>
        /// Generates the name of a component according to the naming conventions configuration
        /// </summary>
        /// <param name="parentEntityType">The type of the parent entity</param>
        /// <param name="componentType">The type of the component</param>
        /// <param name="relationshipProperty">The property that represents the relationship</param>
        /// <returns>The component name</returns>
        string ToComponentTableName(Type parentEntityType, Type componentType, MemberInfo relationshipProperty);

        /// <summary>
        /// Generates the name of an element according to the naming conventions configuration
        /// </summary>
        /// <param name="parentEntityType">The type of the parent entity</param>
        /// <param name="elementType">The type of the element</param>
        /// <param name="relationshipProperty">The property that represents the relationship</param>
        /// <returns>The element table name</returns>
        string ToElementTableName(Type parentEntityType, Type elementType, MemberInfo relationshipProperty);

        /// <summary>
        /// Generates the name of a column for a given property type according to the naming conventions configuration
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns>The column name</returns>
        string ToColumnName(MemberInfo property);

        /// <summary>
        /// Generates the name of a component column for a given property type according to the naming conventions configuration
        /// </summary>
        /// <param name="property">The component child property</param>
        /// <param name="parentProperty">The entity parent property</param>
        /// <returns>The column name</returns>
        string ToComponentColumnName(MemberInfo property, MemberInfo parentProperty);

        /// <summary>
        /// Generates the name of a component foreign key column for a given property type according to the naming conventions configuration
        /// </summary>
        /// <param name="property">The component child property</param>
        /// <param name="parentProperty">The entity parent property</param>
        /// <param name="idProperty">The entity id property</param>
        /// <returns>The column name</returns>
        string ToComponentForeignKeyColumnName(MemberInfo property, MemberInfo parentProperty, MemberInfo idProperty);

        /// <summary>
        /// Generates the name of the key column for an element from a given property type according to the naming conventions configuration
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns>The column name</returns>
        string ToElementKeyColumnName(MemberInfo property);

        /// <summary>
        /// Generates the name of the value column for an element from a given property type according to the naming conventions configuration
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns>The column name</returns>
        string ToElementValueColumnName(MemberInfo property);
        
        /// <summary>
        /// Generates the name of a Primary Key column from a given entity type and Id property name according to the naming conventions configuration
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="idProperty">The member info of the Id property</param>
        /// <returns>The primary key column name</returns>
        string ToPrimaryKeyColumnName(Type entityType, MemberInfo idProperty);
                
        /// <summary>
        /// Generates the name of a foreign key according the naming conventions configuration
        /// </summary>
        /// <param name="foreignKeyEntityType">The foreign key entity type</param>
        /// <param name="primaryKeyEntityType">The primary key entity</param>
        /// <param name="foreignKeyProperty">The foreign key property</param>
       /// <returns>The generated foreign key name</returns>
        string ToForeignKeyName(Type foreignKeyEntityType, Type primaryKeyEntityType, MemberInfo foreignKeyProperty, MemberInfo primaryKeyProperty);

        /// <summary>
        /// Generates the name of a foreign key according the naming conventions configuration
        /// </summary>
        /// <param name="foreignKeyEntityType">The foreign key entity type</param>
        /// <param name="primaryKeyEntityType">The primary key entity</param>
        /// <param name="foreignKeyProperty">The foreign key property</param>
        /// <param name="primaryKeyProperty">The primary key property</param>
        /// <param name="invert">A value indicating if the relationships is inverted</param>
        /// <returns>The generated foreign key name</returns>
        string ToComponentForeignKeyName(Type foreignKeyEntityType, Type primaryKeyEntityType, MemberInfo foreignKeyProperty, MemberInfo primaryKeyProperty, bool invert = false);

        /// <summary>
        /// Generates the name of a foreign key column according the relationship property and the Id property
        /// </summary>
        /// <param name="relationshipProperty">The relationship property</param>
        /// <param name="idProperty">The target entity Id property</param>
        /// <returns>The generated column name</returns>
        string ToForeignKeyColumnName(MemberInfo relationshipProperty, MemberInfo idProperty);

        /// <summary>
        /// Generates the name of a foreign key column according the target entity type and the Id property
        /// </summary>
        /// <param name="targetEntityType">The target entity type</param>
        /// <param name="idProperty">The target entity Id property</param>
        /// <returns>The generated column name</returns>
        string ToManyToManyForeignKeyColumnName(Type targetEntityType, MemberInfo idProperty);

        /// <summary>
        /// Generates the name of the foreign key column name for a many to many relationship according the naming conventions configuration
        /// </summary>
        /// <param name="foreignKeyEntityType">The foreign key entity type</param>
        /// <param name="primaryKeyEntityType">The primary key entity</param>
        /// <param name="foreignKeyProperty">The foreign key property</param>
        /// <returns>The generated foreign key name</returns>
        string ToManyToManyForeignKeyName(Type foreignKeyEntityType, Type primaryKeyEntityType, Type targetType, MemberInfo foreignKeyProperty);

        /// <summary>
        /// Generates the name of a many to many table according the naming conventions configuration
        /// </summary>
        /// <param name="firstEntityType">The type of the first entity of the relationship</param>
        /// <param name="secondEntityType">The type of the second entity of the relationship</param>
        /// <returns>The generated table name</returns>
        string ToManyToManyTableName(Type firstEntityType, Type secondEntityType);

        /// <summary>
        /// Formats the name of a database constraint according to the naming conventions configuration
        /// </summary>
        /// <param name="constraint">The constraint name</param>
        /// <returns>The formatted contraint name</returns>
        string ToConstraintName(string constraint);
    }
}
