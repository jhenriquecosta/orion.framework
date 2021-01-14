using Framework.DataLayer.NHibernate.Loquacious.Convention;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Config
{
    /// <summary>
    /// Provides methods to configure the naming conventions in a fluent way
    /// </summary>
    public class NamingConventionsConfig : BaseConfig<ConventionMapConfig>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NamingConventionsConfig"/> class.
        /// </summary>
        /// <param name="rootConfig">An instance of the current root configuration</param>
        internal NamingConventionsConfig(ConventionMapConfig rootConfig)
            : base(rootConfig)
        {
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the selected naming convention for schema names
        /// </summary>
        internal SchemasNamingConvention SchemasNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected custom naming convention for schema names
        /// </summary>
        internal Func<Type, string> SchemasCustomNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for entities
        /// </summary>
        internal TablesNamingConvention TablesNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected custom naming convention for table names
        /// </summary>
        internal Func<Type, string> TablesCustomNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for column names
        /// </summary>
        internal ColumnsNamingConvention ColumnsNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected custom naming convention for table names
        /// </summary>
        internal Func<MemberInfo, string> ColumnsCustomNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for constraint names
        /// </summary>
        internal ConstraintNamingConvention ConstraintNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for primary key columns
        /// </summary>
        internal PrimaryKeyColumnNamingConvention PrimaryKeyColumnNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected custom naming convention for primary key columns
        /// </summary>
        internal Func<Type, MemberInfo, string> PrimaryKeyColumnCustomNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for foreign key columns
        /// </summary>
        internal ForeignKeyColumnNamingConvention ForeignKeyColumnNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected custom naming convention for foreign key columns
        /// </summary>
        internal Func<MemberInfo, MemberInfo, string> ForeignKeyColumnCustomNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for foreign key names
        /// </summary>
        internal ForeignKeyNamingConvention ForeignKeyNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected custom naming convention for foreign key names
        /// </summary>
        internal Func<Type, Type, MemberInfo, MemberInfo, string> ForeignKeyCustomNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for component table names
        /// </summary>
        internal ComponentsTableNamingConvention ComponentsTableNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected custom naming convention for components table names
        /// </summary>
        internal Func<Type, Type, MemberInfo, string> ComponentsCustomTableNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for element table names
        /// </summary>
        internal ElementsTableNamingConvention ElementsTableNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected custom naming convention for element table names
        /// </summary>
        internal Func<Type, Type, MemberInfo, string> ElementsCustomTableNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for component columns names
        /// </summary>
        internal ComponentsColumnsNamingConvention ComponentsColumnsNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected custom naming convention for components columns names
        /// </summary>
        internal Func<Type, MemberInfo, string> ComponentsColumnsCustomNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for many to many relationships table names
        /// </summary>
        internal ManyToManyTableNamingConvention ManyToManyTableNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected custom naming convention for many to many relationships table names
        /// </summary>
        internal Func<Type, Type, string> ManyToManyCustomTableNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected confition used to identify many to many relationships
        /// </summary>
        internal ManyToManyRelationshipsCondition ManyToManyRelationshipsCondition { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Indicates that a given convention must be used when schema names are being generated
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseConventionForSchemaNames(SchemasNamingConvention convention)
        {
            this.SchemasNamingConvention = convention;
            return this;
        }

        /// <summary>
        /// Indicates that a given user-defined convention must be used when schema names are being generated
        /// </summary>
        /// <param name="customConvention">The custom convention function. The input parameter is the entity type</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseCustomConventionForSchemaNames(Func<Type, string> customConvention)
        {
            this.SchemasNamingConvention = SchemasNamingConvention.Custom;
            this.SchemasCustomNamingConvention = customConvention;

            return this;
        }

        /// <summary>
        /// Indicates that a given convention must be used when entity names are mapped to database table names
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseConventionForTableNames(TablesNamingConvention convention)
        {
            this.TablesNamingConvention = convention;

            return this;
        }

        /// <summary>
        /// Indicates that a given user-defined convention must be used when entity names are mapped to database table names
        /// </summary>
        /// <param name="customConvention">The custom convention function. The input parameter is the entity type</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseCustomConventionForTableNames(Func<Type, string> customConvention)
        {
            this.TablesNamingConvention = TablesNamingConvention.Custom;
            this.TablesCustomNamingConvention = customConvention;

            return this;
        }

        /// <summary>
        /// Indicates that a given convention must be used when entity property names are mapped to database table column names
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseConventionForColumnNames(ColumnsNamingConvention convention)
        {
            this.ColumnsNamingConvention = convention;

            return this;
        }

        /// <summary>
        /// Indicates that a given user-defined convention must be used when entity property names are mapped to database table column names
        /// </summary>
        /// <param name="customConvention">The custom convention function. The input parameter is the member info that describes the property</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseCustomConventionForColumnNames(Func<MemberInfo, string> customConvention)
        {
            this.ColumnsNamingConvention = ColumnsNamingConvention.Custom;
            this.ColumnsCustomNamingConvention = customConvention;

            return this;
        }

        /// <summary>
        /// Indicates that a given convention must be used when generating database constraint names
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseConventionForConstraintNames(ConstraintNamingConvention convention)
        {
            this.ConstraintNamingConvention = convention;

            return this;
        }

        /// <summary>
        /// Indicates that a given convention must be used when ID property names are mapped to database primary key column names
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseConventionForPrimaryKeyColumnNames(PrimaryKeyColumnNamingConvention convention)
        {
            this.PrimaryKeyColumnNamingConvention = convention;

            return this;
        }

        /// <summary>
        /// Indicates that a given user-defined convention must be used when ID property names are mapped to database primary key column names
        /// </summary>
        /// <param name="customConvention">The custom convention function. The input parameter are the entity type and the Id property</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseCustomConventionForPrimaryKeyColumnNames(Func<Type, MemberInfo, string> customConvention)
        {
            this.PrimaryKeyColumnNamingConvention = PrimaryKeyColumnNamingConvention.Custom;
            this.PrimaryKeyColumnCustomNamingConvention = customConvention;

            return this;
        }

        /// <summary>
        /// Indicates that a given convention must be used when relationship property names are mapped to database foreign key column names
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseConventionForForeignKeyColumnNames(ForeignKeyColumnNamingConvention convention)
        {
            this.ForeignKeyColumnNamingConvention = convention;

            return this;
        }

        /// <summary>
        /// Indicates that a given user-defined convention must be used when relationship property names are mapped to database foreign key column names
        /// </summary>
        /// <param name="customConvention">The custom convention function. The input parameters are the relationship property and the target Id property</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseCustomConventionForForeignKeyColumnNames(Func<MemberInfo, MemberInfo, string> customConvention)
        {
            this.ForeignKeyColumnNamingConvention = ForeignKeyColumnNamingConvention.Custom;
            this.ForeignKeyColumnCustomNamingConvention = customConvention;

            return this;
        }

        /// <summary>
        /// Indicates that a given convention must be used when generating the foreign key names
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseConventionForForeignKeyNames(ForeignKeyNamingConvention convention)
        {
            this.ForeignKeyNamingConvention = convention;

            return this;
        }

        /// <summary>
        /// Indicates that a given user-defined convention must be used when generating the foreign key names
        /// </summary>
        /// <param name="customConvention">The custom convention function. The input parameters are the pk entity type the fk entity type, the fk property and the pk property</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseCustomConventionForForeignKeyNames(Func<Type, Type, MemberInfo, MemberInfo, string> customConvention)
        {
            this.ForeignKeyNamingConvention = ForeignKeyNamingConvention.Custom;
            this.ForeignKeyCustomNamingConvention = customConvention;

            return this;
        }

        /// <summary>
        /// Indicates that a given convention must be used when components names are mapped to database table names
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseConventionForComponentTableNames(ComponentsTableNamingConvention convention)
        {
            this.ComponentsTableNamingConvention = convention;

            return this;
        }

        /// <summary>
        /// Indicates that a given user-defined convention must be used when components names are mapped to database table names
        /// </summary>
        /// <param name="customConvention">The custom convention function. The input parameters are the entity type, the component type and the property that relates the parent entity with the component</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseCustomConventionForComponentTableNames(Func<Type, Type, MemberInfo, string> customConvention)
        {
            this.ComponentsTableNamingConvention = ComponentsTableNamingConvention.Custom;
            this.ComponentsCustomTableNamingConvention = customConvention;

            return this;
        }

        /// <summary>
        /// Indicates that a given convention must be used when Elements names are mapped to database table names
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseConventionForElementTableNames(ElementsTableNamingConvention convention)
        {
            this.ElementsTableNamingConvention = convention;

            return this;
        }

        /// <summary>
        /// Indicates that a given user-defined convention must be used when elements names are mapped to database table names
        /// </summary>
        /// <param name="customConvention">The custom convention function. The input parameters are the entity type, the element type and the property that relates the parent entity with the element</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseCustomConventionForElementTableNames(Func<Type, Type, MemberInfo, string> customConvention)
        {
            this.ElementsTableNamingConvention = ElementsTableNamingConvention.Custom;
            this.ElementsCustomTableNamingConvention = customConvention;

            return this;
        }

        /// <summary>
        /// Indicates that a given convention must be used when components columns names are mapped to database column names 
        /// This convention applies when the relationship between the component and the parent entity is one to one, so the component is mapped to the same table as the parent entity
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseConventionForComponentColumnNames(ComponentsColumnsNamingConvention convention)
        {
            this.ComponentsColumnsNamingConvention = convention;

            return this;
        }

        /// <summary>
        /// Indicates that a given custom convention must be used when components columns names are mapped to database column names 
        /// This convention applies when the relationship between the component and the parent entity is one to one, so the component is mapped to the same table as the parent entity
        /// </summary>
        /// <param name="customConvention">The custom convention function. The input parameters are the component type and component property</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseCustomConventionForComponentColumnNames(Func<Type, MemberInfo, string> customConvention)
        {
            this.ComponentsColumnsNamingConvention = ComponentsColumnsNamingConvention.Custom;
            this.ComponentsColumnsCustomNamingConvention = customConvention;

            return this;
        }

        /// <summary>
        /// Indicates that a given convention must be used when mapping many to many relationships to table names
        /// </summary>
        /// <param name="convention">The selected convention</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseConventionForManyToManyTableNames(ManyToManyTableNamingConvention convention)
        {
            this.ManyToManyTableNamingConvention = convention;

            return this;
        }

        /// <summary>
        /// Indicates that a given user-defined convention must be used when mapping many to many relationships to table names
        /// </summary>
        /// <param name="customConvention">The custom convention function. The input parameters are the parent entity type and the parent entity identifier property name.</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig UseCustomConventionForManyToManyTableNames(Func<Type, Type, string> customConvention)
        {
            this.ManyToManyTableNamingConvention = ManyToManyTableNamingConvention.Custom;
            this.ManyToManyCustomTableNamingConvention = customConvention;

            return this;
        }

        /// <summary>
        /// Indicates that a given condition should be used to indentify many to many relationships
        /// </summary>
        /// <param name="condition">The condtion</param>
        /// <returns>An instance of NamingConventionsConfig</returns>
        public NamingConventionsConfig IsManyToManyRelationshipWhen(ManyToManyRelationshipsCondition condition)
        {
            this.ManyToManyRelationshipsCondition = condition;

            return this;
        }

        #endregion
    }
}
