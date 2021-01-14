using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Orion.Framework.DataLayer.NH.Fluent
{
    /// <summary>
    /// VersionConvention class.
    /// </summary>
    public class VersionConvention : IVersionConvention
    {
        #region Public Methods

        /// <summary>
        /// Applies the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply(IVersionInstance instance)
        {
            instance.Not.Nullable();
            instance.Column(instance.Name.ToLowerInvariant());
        }

        #endregion
    }
 
}
