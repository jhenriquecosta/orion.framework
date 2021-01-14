using Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Extensions;
using Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Config
{
    /// <summary>
    /// Provides methods to configure the entity definitions
    /// </summary>
    public class EntitiesConfig : BaseConfig<ConventionMapConfig>
    {
        #region Private Members

        private IList<Type> rootEntityTypes;
        private IList<Type> entityTypes;
        private FilterHelper<Assembly> assembliesFilter;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesConfig"/> class.
        /// </summary>
        /// <param name="rootConfig">An instance of the current root configuration</param>
        internal EntitiesConfig(ConventionMapConfig rootConfig)
            : base(rootConfig)
        {
            this.rootEntityTypes = new List<Type>();
            this.entityTypes = new List<Type>();
            this.assembliesFilter = new FilterHelper<Assembly>();
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the collection of root entities
        /// </summary>
        internal IReadOnlyCollection<Type> RootEntityTypes
        {
            get { return new ReadOnlyCollection<Type>(this.rootEntityTypes); }
        }

        /// <summary>
        /// Gets the collection of entities
        /// </summary>
        internal IReadOnlyCollection<Type> EntityTypes
        {
            get { return new ReadOnlyCollection<Type>(this.entityTypes); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Allows to indicate that a given type is used as base entity type
        /// </summary>
        /// <typeparam name="TBaseEntity">The Type of the base entity</typeparam>
        /// <returns>An instance of EntityDefinitionsConfig</returns>
        public EntitiesConfig AddBaseEntity<TBaseEntity>() where TBaseEntity : class
        {
            if (!this.rootEntityTypes.Contains(typeof(TBaseEntity)))
            {
                this.rootEntityTypes.Add(typeof(TBaseEntity));
            }

            return this;
        }
        public EntitiesConfig AddBaseEntity(Type baseType) 
        {
            if (!this.rootEntityTypes.Contains(baseType))
            {
                this.rootEntityTypes.Add(baseType);
            }

            return this;
        }

        /// <summary>
        /// Includes this assembly when creating the entity list
        /// </summary>
        /// <param name="assembly">The source assembly that contains the entity types</param>
        /// <returns>An instance of EntityDefinitionsConfig</returns>
        public EntitiesConfig SearchForEntitiesOnThisAssembly(Assembly assembly)
        {
            this.assembliesFilter.IncludedItems.Add(assembly);

            return this;
        }

        /// <summary>
        /// Excludes this assembly when creating the entity list
        /// </summary>
        /// <param name="assembly">The source assembly that contains the entity types to exclude</param>
        /// <returns>An instance of EntityDefinitionsConfig</returns>
        public EntitiesConfig ExcludeEntitiesFromThisAssembly(Assembly assembly)
        {
            this.assembliesFilter.ExcludedItems.Add(assembly);

            return this;
        }

        /// <summary>
        /// Includes these assemblies when creating the entity list
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <returns>An instance of EntityDefinitionsConfig</returns>
        public EntitiesConfig SearchForEntitiesOnTheseAssemblies(Func<Assembly, bool> filter)
        {
            this.assembliesFilter.IncludeFilters.Add(filter);

            return this;
        }

        /// <summary>
        /// Excludes these assemblies when creating the entity list
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <returns>An instance of EntityDefinitionsConfig</returns>
        public EntitiesConfig ExcludeEntitiesFromTheseAssemblies(Func<Assembly, bool> filter)
        {
            this.assembliesFilter.ExcludeFilters.Add(filter);

            return this;
        }

        /// <summary>
        /// Adds a list of entity types that define the model
        /// </summary>
        /// <param name="entityTypes">The entity types to add</param>
        /// <returns>An instance of EntityDefinitionsConfig</returns>
        public EntitiesConfig AddEntities(IList<Type> entityTypes)
        {
            this.entityTypes = this.entityTypes.Concat(entityTypes).ToList();

            return this;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Gets the list of source assemblies from an initial source
        /// </summary>
        /// <param name="sourceList">The source list</param>
        /// <returns>The assembly list</returns>
        internal IList<Assembly> GetSourceAssemblies(IList<Assembly> sourceList)
        {
            return this.assembliesFilter.ApplyFilters(sourceList);
        }

        #endregion
    }
}
