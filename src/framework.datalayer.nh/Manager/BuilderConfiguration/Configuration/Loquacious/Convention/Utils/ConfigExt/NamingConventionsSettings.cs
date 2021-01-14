using Framework.DataLayer.NHibernate.Loquacious.Convention.Config;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.ConfigExt
{
    /// <summary>
    /// Provides read-only access to the naming conventions settings
    /// </summary>
    public class NamingConventionsSettings
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NamingConventionsSettings"/> class.
        /// </summary>
        internal NamingConventionsSettings(NamingConventionsConfig config)
        {
            this.TablesNamingConvention = config.TablesNamingConvention;
            this.ColumnsNamingConvention = config.ColumnsNamingConvention;
            this.ConstraintNamingConvention = config.ConstraintNamingConvention;
            this.PrimaryKeyColumnNamingConvention = config.PrimaryKeyColumnNamingConvention;
            this.ForeignKeyColumnNamingConvention = config.ForeignKeyColumnNamingConvention;
            this.ForeignKeyNamingConvention = config.ForeignKeyNamingConvention;
            this.ComponentsTableNamingConvention = config.ComponentsTableNamingConvention;
            this.ElementsTableNamingConvention = config.ElementsTableNamingConvention;
            this.ComponentsColumnsNamingConvention = config.ComponentsColumnsNamingConvention;
            this.ManyToManyTableNamingConvention = config.ManyToManyTableNamingConvention;
            this.ManyToManyRelationshipsCondition = config.ManyToManyRelationshipsCondition;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the selected naming convention for entities
        /// </summary>
        public TablesNamingConvention TablesNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for column names
        /// </summary>
        public ColumnsNamingConvention ColumnsNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for constraint names
        /// </summary>
        public ConstraintNamingConvention ConstraintNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for primary key columns
        /// </summary>
        public PrimaryKeyColumnNamingConvention PrimaryKeyColumnNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for foreign key columns
        /// </summary>
        public ForeignKeyColumnNamingConvention ForeignKeyColumnNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for foreign key names
        /// </summary>
        public ForeignKeyNamingConvention ForeignKeyNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for component table names
        /// </summary>
        public ComponentsTableNamingConvention ComponentsTableNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for element table names
        /// </summary>
        public ElementsTableNamingConvention ElementsTableNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for component columns names
        /// </summary>
        public ComponentsColumnsNamingConvention ComponentsColumnsNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected naming convention for many to many relationships table names
        /// </summary>
        public ManyToManyTableNamingConvention ManyToManyTableNamingConvention { get; private set; }

        /// <summary>
        /// Gets the selected confition used to identify many to many relationships
        /// </summary>
        public ManyToManyRelationshipsCondition ManyToManyRelationshipsCondition { get; private set; }

        #endregion
    }
}
