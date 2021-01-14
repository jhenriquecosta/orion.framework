namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Config
{
    /// <summary>
    /// A base class for configuration clasess
    /// </summary>
    /// <typeparam name="TRootConfig">The type of the root configuration</typeparam>
    public abstract class BaseConfig<TRootConfig>
    {
        #region Properties

        /// <summary>
        /// Gets the root config
        /// </summary>
        protected TRootConfig RootConfig { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseConfig"/> class
        /// </summary>
        /// <param name="rootConfig">An instance of the root config</param>
        internal BaseConfig(TRootConfig rootConfig)
        {
            this.RootConfig = rootConfig;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Ends the current configuration and returns to the root config
        /// </summary>
        /// <returns>An instance of the root config</returns>
        public TRootConfig EndConfig()
        {
            return this.RootConfig;
        }

        #endregion
    }
}
