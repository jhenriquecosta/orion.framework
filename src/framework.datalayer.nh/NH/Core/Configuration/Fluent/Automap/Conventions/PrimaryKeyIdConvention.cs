using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Orion.Framework.DataLayer.NH.Fluent.Conventions
{
    /// <summary>
    /// PrimaryKeyIdConvention class.
    /// </summary>
    public class PrimaryKeyIdConvention : IIdConvention
    {
        #region Public Methods

        /// <summary>
        /// Applies primary key name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IIdentityInstance instance )
        {
            var key = instance.EntityType.Name;

            //if ( typeof( ILookup ).IsAssignableFrom ( instance.EntityType ) )
            //{
            //    key = key + "_Lkp";
            //}
            //key = $"{key}_Id";
            key = "id";
            instance.Column (key );

          //  instance.Access.CamelCaseField  ( CamelCasePrefix.Underscore );

            if (instance.Type == typeof(int))
            {
                instance.GeneratedBy.Identity();
            }
            else
            {
                instance.GeneratedBy.Assigned();
            }
            // instance.GeneratedBy.Custom ( typeof( CustomTableHiLoGenerator ) );

        }

        #endregion
    }
}
