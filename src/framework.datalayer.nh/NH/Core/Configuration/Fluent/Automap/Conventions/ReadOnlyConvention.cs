using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Orion.Framework.DataLayer.NH.Fluent.Conventions
{
    /// <summary>
    /// ReadOnlyConvention class.
    /// </summary>
    public class ReadOnlyConvention : IClassConvention
    {
        #region Public Methods

        /// <summary>
        /// Sets read-only if ILookup.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IClassInstance instance )
        {
            //if ( typeof( ILookup ).IsAssignableFrom ( instance.EntityType ) )
            //{
            //    instance.ReadOnly ();
            //}
        }

        #endregion
    }
}
