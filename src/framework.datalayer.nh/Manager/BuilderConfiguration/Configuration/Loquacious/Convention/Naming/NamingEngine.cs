using Framework.DataLayer.NHibernate.Loquacious.Convention.Config;
using Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Helpers;
using NHibernate.Util;
using System;
using System.Configuration;
using System.Reflection;
using Framework.DataLayer.NHibernate.Properties;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Naming
{
    /// <summary>
    /// Provides methods to format names according a given configuration
    /// </summary>
    internal class NamingEngine : INamingEngine
    {
        #region Members

        private NamingConventionsConfig namingConventions;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NamingEngine"/> class.
        /// </summary>
        /// <param name="namingConventions">An instance of the current naming conventions</param>
        public NamingEngine(NamingConventionsConfig namingConventions)
        {
            this.namingConventions = namingConventions;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates the name of a schema based on a given entity type
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <returns>The schema name</returns>
        public string ToSchemaName(Type entityType)
        {
            string result = null;
            switch (namingConventions.SchemasNamingConvention)
            {
                case SchemasNamingConvention.Default: result = null; break;
                case SchemasNamingConvention.AssemblyName: result = NamingHelper.ToCamelCase(StringHelper.Unqualify(entityType.Assembly.GetName().Name)); break;
                case SchemasNamingConvention.NamespaceName: result =  NamingHelper.ToCamelCase(StringHelper.Unqualify(entityType.Namespace)); break;
                case SchemasNamingConvention.Attribute:
                {
                    result = "";
                }
                    break;
                case SchemasNamingConvention.Custom:
                    {
                        if (namingConventions.SchemasCustomNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(SchemasNamingConvention).Name));

                        result = namingConventions.SchemasCustomNamingConvention(entityType);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Generates the name of a schema based on a couple of related entities
        /// </summary>
        /// <param name="sourceType">The source type</param>
        /// <param name="targetType">The target type</param>
        /// <returns>The schema name</returns>
        public string ToSchemaName(Type sourceType, Type targetType)
        {
            Type type = string.Compare(sourceType.Name, targetType.Name) < 0 ? sourceType : targetType;

            return ToSchemaName(type);
        }

        /// <summary>
        /// Generates the name of a table for a given entity type according to the naming conventions configuration
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <returns>The table name</returns>
        public string ToTableName(Type entityType)
        {
            string result = null;
            switch (namingConventions.TablesNamingConvention)
            {
                case TablesNamingConvention.Default: result = entityType.Name; break;
                case TablesNamingConvention.CamelCase: result = NamingHelper.ToCamelCase(entityType.Name); break;
                case TablesNamingConvention.PascalCase: result = NamingHelper.ToPascalCase(entityType.Name); break;
                case TablesNamingConvention.Lowercase: result = NamingHelper.ToLowercase(entityType.Name); break;
                case TablesNamingConvention.Uppercase: result = NamingHelper.ToUppercase(entityType.Name); break;
                case TablesNamingConvention.Custom:
                    {
                        if (namingConventions.TablesCustomNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(TablesNamingConvention).Name));

                        result = namingConventions.TablesCustomNamingConvention(entityType);
                    }; break;
            }

            return PurgeTypeName(result);
        }

        /// <summary>
        /// Generates the name of a component according to the naming conventions configuration
        /// </summary>
        /// <param name="parentEntityType">The type of the parent entity</param>
        /// <param name="componentType">The type of the component</param>
        /// <param name="relationshipProperty">The property that represents the relationship</param>
        /// <returns>The component name</returns>
        public string ToComponentTableName(Type parentEntityType, Type componentType, MemberInfo relationshipProperty)
        {
            string result = null;
            string relationshipPropertyName = relationshipProperty.Name;
            switch (namingConventions.ComponentsTableNamingConvention)
            {
                case ComponentsTableNamingConvention.ComponentName: result = ToTableName(componentType); break;
                case ComponentsTableNamingConvention.RelationshipName: result = ToTableName(relationshipPropertyName); break;
                case ComponentsTableNamingConvention.EntityNameComponentName: result = ToTableName(string.Concat(parentEntityType.Name, componentType.Name)); break;
                case ComponentsTableNamingConvention.EntityNameRelationshipName: result = ToTableName(string.Concat(parentEntityType.Name, relationshipPropertyName)); break;
                case ComponentsTableNamingConvention.EntityName_ComponentName: result = string.Format(ConventionFormats.RelationshipUnderscoreSeparated, ToTableName(parentEntityType), ToTableName(componentType)); break;
                case ComponentsTableNamingConvention.EntityName_RelationshipName: result = string.Format(ConventionFormats.RelationshipUnderscoreSeparated, ToTableName(parentEntityType), ToTableName(relationshipPropertyName)); break;
                case ComponentsTableNamingConvention.Custom:
                    {
                        if (namingConventions.ComponentsCustomTableNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(ComponentsTableNamingConvention).Name));

                        result = namingConventions.ComponentsCustomTableNamingConvention(parentEntityType, componentType, relationshipProperty);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Generates the name of an element according to the naming conventions configuration
        /// </summary>
        /// <param name="parentEntityType">The type of the parent entity</param>
        /// <param name="elementType">The type of the element</param>
        /// <param name="relationshipProperty">The property that represents the relationship</param>
        /// <returns>The element name</returns>
        public string ToElementTableName(Type parentEntityType, Type elementType, MemberInfo relationshipProperty)
        {
            string result = null;
            switch (namingConventions.ElementsTableNamingConvention)
            {
                case ElementsTableNamingConvention.ElementTypeName: result = ToTableName(elementType); break;
                case ElementsTableNamingConvention.PropertyName: result = ToTableName(relationshipProperty.Name); break;
                case ElementsTableNamingConvention.EntityNameElementName: result = ToTableName(string.Concat(parentEntityType.Name, elementType.Name)); break;
                case ElementsTableNamingConvention.EntityNamePropertyName: result = ToTableName(string.Concat(parentEntityType.Name, relationshipProperty.Name)); break;
                case ElementsTableNamingConvention.EntityName_ElementName: result = string.Format(ConventionFormats.RelationshipUnderscoreSeparated, ToTableName(parentEntityType), ToTableName(elementType)); break;
                case ElementsTableNamingConvention.EntityName_PropertyName: result = string.Format(ConventionFormats.RelationshipUnderscoreSeparated, ToTableName(parentEntityType), ToTableName(relationshipProperty.Name)); break;
                case ElementsTableNamingConvention.Custom:
                    {
                        if (namingConventions.ElementsCustomTableNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(ElementsTableNamingConvention).Name));

                        result = namingConventions.ElementsCustomTableNamingConvention(parentEntityType, elementType, relationshipProperty);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Generates the name of a column for a given property type according to the naming conventions configuration
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns>The table name</returns>
        public string ToColumnName(MemberInfo property)
        {
            string result = null;
            string propertyName = property.Name;
            switch (namingConventions.ColumnsNamingConvention)
            {
                case ColumnsNamingConvention.Default: result = propertyName; break;
                case ColumnsNamingConvention.CamelCase: result = NamingHelper.ToCamelCase(propertyName); break;
                case ColumnsNamingConvention.PascalCase: result = NamingHelper.ToPascalCase(propertyName); break;
                case ColumnsNamingConvention.Lowercase: result = NamingHelper.ToLowercase(propertyName); break;
                case ColumnsNamingConvention.Uppercase: result = NamingHelper.ToUppercase(propertyName); break;
                case ColumnsNamingConvention.Custom:
                    {
                        if (namingConventions.ColumnsCustomNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(ComponentsTableNamingConvention).Name));

                        result = namingConventions.ColumnsCustomNamingConvention(property);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Generates the name of a component column for a given property type according to the naming conventions configuration
        /// </summary>
        /// <param name="property">The component child property</param>
        /// <param name="parentProperty">The entity parent property</param>
        /// <returns>The column name</returns>
        public string ToComponentColumnName(MemberInfo property, MemberInfo parentProperty)
        {
            string result = null;
            switch (namingConventions.ComponentsColumnsNamingConvention)
            {
                case ComponentsColumnsNamingConvention.PropertyName: result = ToColumnName(property); break;
                case ComponentsColumnsNamingConvention.ComponentNamePropertyName: result = ToColumnName(string.Concat(property.DeclaringType.Name, property.Name)); break;
                case ComponentsColumnsNamingConvention.ComponentName_PropertyName: result = string.Format(ConventionFormats.RelationshipUnderscoreSeparated, ToColumnName(property.DeclaringType.Name), ToColumnName(property.Name)); break;
                case ComponentsColumnsNamingConvention.EntityPropertyNameComponentPropertyName: result = ToColumnName(string.Concat(parentProperty.Name, property.Name)); break;
                case ComponentsColumnsNamingConvention.EntityPropertyName_ComponentPropertyName: result = string.Format(ConventionFormats.RelationshipUnderscoreSeparated, ToColumnName(parentProperty.Name), ToColumnName(property.Name)); break;
                case ComponentsColumnsNamingConvention.Custom:
                    {
                        if (namingConventions.ComponentsColumnsCustomNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(ComponentsColumnsNamingConvention).Name));

                        result = namingConventions.ComponentsColumnsCustomNamingConvention(property.DeclaringType, property);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Generates the name of a component foreign key column for a given property type according to the naming conventions configuration
        /// </summary>
        /// <param name="property">The component child property</param>
        /// <param name="parentProperty">The entity parent property</param>
        /// <param name="idProperty">The entity id property</param>
        /// <returns>The column name</returns>
        public string ToComponentForeignKeyColumnName(MemberInfo property, MemberInfo parentProperty, MemberInfo idProperty)
        {
            string result = null;

            string columnName = ToForeignKeyColumnName(property, idProperty);
            switch (namingConventions.ComponentsColumnsNamingConvention)
            {
                case ComponentsColumnsNamingConvention.PropertyName: result = ToColumnName(property); break;
                case ComponentsColumnsNamingConvention.ComponentNamePropertyName: result = ToColumnName(string.Concat(property.DeclaringType.Name, ToColumnNameSuffix(columnName))); break;
                case ComponentsColumnsNamingConvention.ComponentName_PropertyName: result = string.Format(ConventionFormats.RelationshipUnderscoreSeparated, ToColumnName(property.DeclaringType.Name), ToColumnName(columnName)); break;
                case ComponentsColumnsNamingConvention.EntityPropertyNameComponentPropertyName: result = ToColumnName(string.Concat(parentProperty.Name, ToColumnNameSuffix(columnName))); break;
                case ComponentsColumnsNamingConvention.EntityPropertyName_ComponentPropertyName: result = string.Format(ConventionFormats.RelationshipUnderscoreSeparated, ToColumnName(parentProperty.Name), ToColumnName(columnName)); break;
                case ComponentsColumnsNamingConvention.Custom:
                    {
                        if (namingConventions.ComponentsColumnsCustomNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(ComponentsColumnsNamingConvention).Name));

                        result = namingConventions.ComponentsColumnsCustomNamingConvention(property.DeclaringType, property);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Generates the name of the key column for an element from a given property type according to the naming conventions configuration
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns>The column name</returns>
        public string ToElementKeyColumnName(MemberInfo property)
        {
            return namingConventions.ColumnsNamingConvention != ColumnsNamingConvention.Custom
                ? ToColumnName(ConventionFormats.ElementKeyColumnName)
                : ToColumnName(property);
        }

        /// <summary>
        /// Generates the name of the value column for an element from a given property type according to the naming conventions configuration
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns>The column name</returns>
        public string ToElementValueColumnName(MemberInfo property)
        {
            return namingConventions.ColumnsNamingConvention != ColumnsNamingConvention.Custom
                ? ToColumnName(ConventionFormats.ElementValueColumnName)
                : ToColumnName(property);
        }

        /// <summary>
        /// Generates the name of a Primary Key column from a given entity type and Id property name according to the naming conventions configuration
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="idProperty">The member info of the Id property</param>
        /// <returns>The primary key column name</returns>
        public string ToPrimaryKeyColumnName(Type entityType, MemberInfo idProperty)
        {
            string result = null;
            switch (namingConventions.PrimaryKeyColumnNamingConvention)
            {
                case PrimaryKeyColumnNamingConvention.Default: result = ToColumnName(idProperty); break;
                case PrimaryKeyColumnNamingConvention.EntityNameIdPropertyName: result = ToColumnName(string.Concat(entityType.Name, idProperty.Name)); break;
                case PrimaryKeyColumnNamingConvention.EntityName_IdPropertyName: result = string.Format(ConventionFormats.RelationshipUnderscoreSeparated, ToColumnName(entityType.Name), ToColumnName(idProperty)); break;
                case PrimaryKeyColumnNamingConvention.Custom:
                    {
                        if (namingConventions.PrimaryKeyColumnCustomNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(PrimaryKeyColumnNamingConvention).Name));

                        result = namingConventions.PrimaryKeyColumnCustomNamingConvention(entityType, idProperty);
                    }; break;
            }

            return result;
        }
       
        /// <summary>
        /// Generates the name of the foreign key column name for a many to many relationship according the naming conventions configuration
        /// </summary>
        /// <param name="foreignKeyEntityType">The foreign key entity type</param>
        /// <param name="primaryKeyEntityType">The primary key entity</param>
        /// <param name="foreignKeyProperty">The foreign key property</param>
        /// <returns>The generated foreign key name</returns>
        public string ToManyToManyForeignKeyName(Type foreignKeyEntityType, Type primaryKeyEntityType, Type targetType, MemberInfo foreignKeyProperty)
        {
            string result = null;
            string foreignKeyEntityName = ToManyToManyTableName(foreignKeyEntityType, primaryKeyEntityType);
            switch (namingConventions.ForeignKeyNamingConvention)
            {
                case ForeignKeyNamingConvention.FK_FKTable_PKTable: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTarget, foreignKeyEntityName, targetType.Name)); break;
                case ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTargetKey, foreignKeyEntityName, targetType.Name, foreignKeyProperty.Name)); break;
                case ForeignKeyNamingConvention.FKTable_PKTable_FK: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTargetSuffix, foreignKeyEntityName, targetType.Name)); break;
                case ForeignKeyNamingConvention.FKTable_PKTable_FKColumn_FK: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTargetKeySuffix, foreignKeyEntityName, targetType.Name, foreignKeyProperty.Name)); break;
                case ForeignKeyNamingConvention.Custom:
                    {
                        if (namingConventions.ForeignKeyCustomNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(ForeignKeyNamingConvention).Name));

                        result = namingConventions.ForeignKeyCustomNamingConvention(foreignKeyEntityType, primaryKeyEntityType, targetType, foreignKeyProperty);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Generates the name of a foreign key according the naming conventions configuration
        /// </summary>
        /// <param name="foreignKeyEntityType">The foreign key entity type</param>
        /// <param name="primaryKeyEntityType">The primary key entity</param>
        /// <param name="foreignKeyProperty">The foreign key property</param>
        /// <param name="foreignKeyProperty">The primary key property</param>
        /// <returns>The generated foreign key name</returns>
        public string ToForeignKeyName(Type foreignKeyEntityType, Type primaryKeyEntityType, MemberInfo foreignKeyProperty, MemberInfo primaryKeyProperty)
        {
            string result = null;
            switch (namingConventions.ForeignKeyNamingConvention)
            {
                case ForeignKeyNamingConvention.FK_FKTable_PKTable: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTarget, foreignKeyEntityType.Name, primaryKeyEntityType.Name)); break;
                case ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTargetKey, foreignKeyEntityType.Name, primaryKeyEntityType.Name, ToForeignKeyColumnName(foreignKeyProperty, primaryKeyProperty))); break;
                case ForeignKeyNamingConvention.FKTable_PKTable_FK: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTargetSuffix, foreignKeyEntityType.Name, primaryKeyEntityType.Name)); break;
                case ForeignKeyNamingConvention.FKTable_PKTable_FKColumn_FK: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTargetKeySuffix, foreignKeyEntityType.Name, primaryKeyEntityType.Name, ToForeignKeyColumnName(foreignKeyProperty, primaryKeyProperty))); break;
                case ForeignKeyNamingConvention.Custom:
                    {
                        if (namingConventions.ForeignKeyCustomNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(ForeignKeyNamingConvention).Name));

                        result = namingConventions.ForeignKeyCustomNamingConvention(foreignKeyEntityType, primaryKeyEntityType, foreignKeyProperty, primaryKeyProperty);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Generates the name of a foreign key according the naming conventions configuration
        /// </summary>
        /// <param name="foreignKeyEntityType">The foreign key entity type</param>
        /// <param name="primaryKeyEntityType">The primary key entity</param>
        /// <param name="foreignKeyProperty">The foreign key property</param>
        /// <param name="primaryKeyProperty">The primary key property</param>
        /// <param name="invert">A value indicating if the relationships is inverted</param>
        /// <returns>The generated foreign key name</returns>
        public string ToComponentForeignKeyName(Type foreignKeyEntityType, Type primaryKeyEntityType, MemberInfo foreignKeyProperty, MemberInfo primaryKeyProperty, bool invert = false)
        {
            string result = null;
            string componentTableName = invert ? ToComponentTableName(foreignKeyEntityType, primaryKeyEntityType, foreignKeyProperty) : ToComponentTableName(primaryKeyEntityType, foreignKeyEntityType, foreignKeyProperty);
            switch (namingConventions.ForeignKeyNamingConvention)
            {
                case ForeignKeyNamingConvention.FK_FKTable_PKTable: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTarget, componentTableName, primaryKeyEntityType.Name)); break;
                case ForeignKeyNamingConvention.FK_FKTable_PKTable_FKColumn: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTargetKey, componentTableName, primaryKeyEntityType.Name, ToPrimaryKeyColumnName(primaryKeyEntityType, primaryKeyProperty))); break;
                case ForeignKeyNamingConvention.FKTable_PKTable_FK: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTargetSuffix, componentTableName, primaryKeyEntityType.Name)); break;
                case ForeignKeyNamingConvention.FKTable_PKTable_FKColumn_FK: result = ToConstraintName(string.Format(ConventionFormats.ForeignKeySourceTargetKeySuffix, componentTableName, primaryKeyEntityType.Name, ToPrimaryKeyColumnName(primaryKeyEntityType, primaryKeyProperty))); break;
                case ForeignKeyNamingConvention.Custom:
                    {
                        if (namingConventions.ForeignKeyCustomNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(ForeignKeyNamingConvention).Name));

                        result = namingConventions.ForeignKeyCustomNamingConvention(foreignKeyEntityType, primaryKeyEntityType, foreignKeyProperty, primaryKeyProperty);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Generates the name of a foreign key column according the relationship property and the Id property
        /// </summary>
        /// <param name="relationshipProperty">The relationship property</param>
        /// <param name="idProperty">The target entity Id property</param>
        /// <returns>The generated column name</returns>
        public string ToForeignKeyColumnName(MemberInfo relationshipProperty, MemberInfo idProperty)
        {
            string result = null;
            switch (namingConventions.ForeignKeyColumnNamingConvention)
            {
                case ForeignKeyColumnNamingConvention.Default: result = ToColumnName(relationshipProperty); break;
                case ForeignKeyColumnNamingConvention.PropertyNameIdPropertyName: result = ToColumnName(string.Concat(relationshipProperty.Name, idProperty.Name)); break;
                case ForeignKeyColumnNamingConvention.PropertyName_IdPropertyName: result = string.Format(ConventionFormats.RelationshipUnderscoreSeparated, ToColumnName(relationshipProperty), ToColumnName(idProperty)); break;
                case ForeignKeyColumnNamingConvention.Custom:
                    {
                        if (namingConventions.ForeignKeyColumnCustomNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(ForeignKeyColumnNamingConvention).Name));

                        result = namingConventions.ForeignKeyColumnCustomNamingConvention(relationshipProperty, idProperty);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Generates the name of a foreign key column according the target entity type and the Id property
        /// </summary>
        /// <param name="targetEntityType">The target entity type</param>
        /// <param name="idProperty">The target entity Id property</param>
        /// <returns>The generated column name</returns>
        public string ToManyToManyForeignKeyColumnName(Type targetEntityType, MemberInfo idProperty)
        {
            string result = ToForeignKeyColumnName(targetEntityType, idProperty);

            if (string.Compare(targetEntityType.Name, result, true) == 0)
            {
                result = string.Format(ConventionFormats.RelationshipUnderscoreSeparated, ToColumnName(targetEntityType), ToColumnName(ConventionFormats.DefaultForeignKeyColumnSuffix));
            }

            return result;
        }

        /// <summary>
        /// Generates the name of a many to many table according the naming conventions configuration
        /// </summary>
        /// <param name="firstEntityType">The type of the first entity of the relationship</param>
        /// <param name="secondEntityType">The type of the second entity of the relationship</param>
        /// <returns>The generated table name</returns>
        public string ToManyToManyTableName(Type firstEntityType, Type secondEntityType)
        {
            string result = null;
            Type firstType = firstEntityType;
            Type secondType = secondEntityType;

            if (string.Compare(firstEntityType.Name, secondEntityType.Name) > 0)
            {
                firstType = secondEntityType;
                secondType = firstEntityType;
            }

            switch (namingConventions.ManyToManyTableNamingConvention)
            {
                case ManyToManyTableNamingConvention.FirstTableNameToSecondTableName: result = ToTableName(string.Format(ConventionFormats.ManyToManyToSeparator, firstType.Name, secondType.Name)); break;
                case ManyToManyTableNamingConvention.FirstTableNameSecondTableName: result = ToTableName(string.Format(ConventionFormats.ManyToManyNoSeparator, firstType.Name, secondType.Name)); break;
                case ManyToManyTableNamingConvention.FirstTableName_SecondTableName: result = string.Format(ConventionFormats.ManyToManyUndescoreSeparator, ToTableName(firstType), ToTableName(secondType)); break;
                case ManyToManyTableNamingConvention.Custom:
                    {
                        if (namingConventions.ManyToManyCustomTableNamingConvention == null)
                            throw new ConfigurationErrorsException(string.Format(ExceptionMessages.CustomConventionNotFound, typeof(ManyToManyTableNamingConvention).Name));

                        result = namingConventions.ManyToManyCustomTableNamingConvention(firstType, secondType);
                    }; break;
            }

            return result;
        }

        /// <summary>
        /// Formats the name of a database constraint according to the naming conventions configuration
        /// </summary>
        /// <param name="constraint">The constraint</param>
        /// <returns>The formatted constraint name</returns>
        public string ToConstraintName(string constraint)
        {
            string result = null;
            switch (namingConventions.ConstraintNamingConvention)
            {
                case ConstraintNamingConvention.Default: result = constraint; break;
                case ConstraintNamingConvention.Lowercase: result = NamingHelper.ToLowercase(constraint); break;
                case ConstraintNamingConvention.Uppercase: result = NamingHelper.ToUppercase(constraint); break;
            }

            return result;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates the name of a table for a given entity type according to the naming conventions configuration
        /// </summary>
        /// <param name="entityName">The entity name</param>
        /// <returns>The table name</returns>
        private string ToTableName(string entityName)
        {
            string result = entityName;
            switch (namingConventions.TablesNamingConvention)
            {
                case TablesNamingConvention.Default: result = entityName; break;
                case TablesNamingConvention.CamelCase: result = NamingHelper.ToCamelCase(entityName); break;
                case TablesNamingConvention.PascalCase: result = NamingHelper.ToPascalCase(entityName); break;
                case TablesNamingConvention.Lowercase: result = NamingHelper.ToLowercase(entityName); break;
                case TablesNamingConvention.Uppercase: result = NamingHelper.ToUppercase(entityName); break;
            }

            return PurgeTypeName(result);
        }

        /// <summary>
        /// Generates the name of a column for a given property name according to the naming conventions configuration
        /// </summary>
        /// <param name="propertyName">The property name</param>
        /// <returns>The column name</returns>
        private string ToColumnName(string propertyName)
        {
            string result = null;
            switch (namingConventions.ColumnsNamingConvention)
            {
                case ColumnsNamingConvention.Default: result = propertyName; break;
                case ColumnsNamingConvention.CamelCase: result = NamingHelper.ToCamelCase(propertyName); break;
                case ColumnsNamingConvention.PascalCase: result = NamingHelper.ToPascalCase(propertyName); break;
                case ColumnsNamingConvention.Lowercase: result = NamingHelper.ToLowercase(propertyName); break;
                case ColumnsNamingConvention.Uppercase: result = NamingHelper.ToUppercase(propertyName); break;
            }

            return result;
        }


        /// <summary>
        /// Generates the name of a column for a given property name according to the naming conventions configuration
        /// </summary>
        /// <param name="propertyName">The property name</param>
        /// <returns>The column name</returns>
        private string ToColumnNameSuffix(string propertyName)
        {
            string result = null;
            switch (namingConventions.ColumnsNamingConvention)
            {
                case ColumnsNamingConvention.Default:
                case ColumnsNamingConvention.CamelCase:
                case ColumnsNamingConvention.PascalCase: result = NamingHelper.ToPascalCase(propertyName); break;
                case ColumnsNamingConvention.Lowercase: result = NamingHelper.ToLowercase(propertyName); break;
                case ColumnsNamingConvention.Uppercase: result = NamingHelper.ToUppercase(propertyName); break;
            }

            return result;
        }
              
        /// <summary>
        /// Removes the characters not supported by the database from a type name
        /// </summary>
        /// <param name="typeName">The type name</param>
        /// <returns>The purged type name</returns>
        private string PurgeTypeName(string typeName)
        {
            return typeName.Contains("`") ? typeName.Substring(0, typeName.IndexOf("`")) : typeName;
        }

        #endregion
    }
}
