using Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Config
{
    /// <summary>
    /// Provides methods to configure the custom mappings definitions in a fluent way
    /// </summary>
    public class MappingsConfig : BaseConfig<ConventionMapConfig>
    {
        #region Private Members

        private IList<Type> mappings;
        private FilterHelper<Assembly> assemblyFilter;
        private IList<Func<Type, bool>> mappingFilters;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingsConfig"/> class.
        /// </summary>
        /// <param name="rootConfig">An instance of the current root configuration</param>
        internal MappingsConfig(ConventionMapConfig rootConfig)
            :base(rootConfig)
        {
            this.mappings = new List<Type>();
            this.assemblyFilter = new FilterHelper<Assembly>();
            this.mappingFilters = new List<Func<Type, bool>>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Includes this assembly when creating the mappings list
        /// </summary>
        /// <param name="assembly">The source assembly that contains the mappings types to include</param>
        /// <returns>An instance of MappingsConfig</returns>
        public MappingsConfig SearchForMappingsOnThisAssembly(Assembly assembly)
        {
            this.assemblyFilter.IncludedItems.Add(assembly);

            return this;
        }

        /// <summary>
        /// Excludes this assembly when creating the mappings list
        /// </summary>
        /// <param name="assembly">The source assembly that contains the mappings types to exclude</param>
        /// <returns>An instance of MappingsConfig</returns>
        public MappingsConfig ExcludeMappingsFromThisAssembly(Assembly assembly)
        {
            this.assemblyFilter.ExcludedItems.Add(assembly);

            return this;
        }

        /// <summary>
        /// Includes these assemblies when creating the mappings list
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <returns>An instance of MappingsConfig</returns>
        public MappingsConfig SearchForMappingsOnTheseAssemblies(Func<Assembly, bool> filter)
        {
            this.assemblyFilter.IncludeFilters.Add(filter);

            return this;
        }

        /// <summary>
        /// Excludes these assemblies when creating the mappings list
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <returns>An instance of MappingsConfig</returns>
        public MappingsConfig ExcludeMappingsFromTheseAssemblies(Func<Assembly, bool> filter)
        {
            this.assemblyFilter.ExcludeFilters.Add(filter);

            return this;
        }

        /// <summary>
        /// Adds a filter criteria used to identity mapping types
        /// </summary>
        /// <param name="filter">The criteria to filter the mapping types</param>
        /// <returns>An instance of MappingsConfig</returns>
        public MappingsConfig AddMappingFilter(Func<Type, bool> filter)
        {
            this.mappingFilters.Add(filter);

            return this;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Gets the list of mapping types from an initial source
        /// </summary>
        /// <param name="sourceList">The source list</param>
        /// <returns>The mapping types list</returns>
        internal IList<Type> GetMappings(IList<Assembly> sourceList)
        {
            var result = mappingFilters.Any() 
                ? this.assemblyFilter.ApplyFilters(sourceList)
                 .SelectMany(a => a.GetExportedTypes())
                 .Where(t => mappingFilters.All(m => m(t)))
                 .ToList()
                : new List<Type>();

            return result;
        }

        #endregion
    }
}
