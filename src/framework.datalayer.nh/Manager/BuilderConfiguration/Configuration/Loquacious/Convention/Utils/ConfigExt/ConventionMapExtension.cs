using Framework.DataLayer.NHibernate.Loquacious.Convention.Naming;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.ConfigExt
{
    /// <summary>
    /// An entry point to extend Firehawk configuration and functionality
    /// </summary>
    public class ConventionMapExtension
    {
        #region Properties

        /// <summary>
        /// Gets the current Nhibernate configuration
        /// </summary>
        protected Configuration NhConfiguration { get; private set; }

        /// <summary>
        /// Get the current model mapper
        /// </summary>
        protected ModelMapper ModelMapper { get; private set; }

        /// <summary>
        /// Gets the naming conventions settings
        /// </summary>
        protected NamingConventionsSettings NamingConventions { get; private set; }

        /// <summary>
        /// Gets the current naming engine
        /// </summary>
        protected INamingEngine NamingEngine { get; private set; }

        #endregion
              
        #region Public Methods

        /// <summary>
        /// Allows to execute custom code before the given entity types are compiled into the mapping document
        /// </summary>
        /// <param name="entityTypes">The entity types</param>
        public virtual void BeforeCompileMappings(IList<Type> entityTypes)
        { 

        }

        /// <summary>
        /// Allows to execute custom code after the entity types were compiled to the given mapping document
        /// </summary>
        /// <param name="entityTypes">The entity types</param>
        public virtual void AfterCompileMappings(IList<Type> entityTypes, HbmMapping mapping)
        {

        }

        /// <summary>
        /// Allows to execute custom code before the given mapping document is built
        /// </summary>
        /// <param name="mapping">The mapping doc</param>
        public virtual void BeforeBuildMappings(HbmMapping mapping)
        {

        }

        /// <summary>
        /// Allows to execute custom code after the given mapping document is built
        /// </summary>
        /// <param name="mapping">The mapping doc</param>
        public virtual void AfterBuildMappings(HbmMapping mapping)
        {

        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Initializes the extension instance with the provided values
        /// </summary>
        /// <param name="nhConfig">The current Nhibernate configuration</param>
        /// <param name="modelMapper">An instance of the current model mapper</param>
        /// <param name="namingConventions">The naming conventions settings</param>
        /// <param name="namingEngine">The naming engine</param>
        internal void Init(Configuration nhConfig, ModelMapper modelMapper, NamingConventionsSettings namingConventions, INamingEngine namingEngine)
        {
            this.NhConfiguration = nhConfig;
            this.ModelMapper = modelMapper;
            this.NamingConventions = namingConventions;
            this.NamingEngine = namingEngine;
        }

        #endregion
    }
}
