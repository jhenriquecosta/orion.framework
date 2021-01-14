using System.Linq;
using FluentNHibernate.Conventions.Inspections;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.DataLayer.NH.Fluent;

namespace Orion.Framework
{
    /// <summary>
    /// ComponentBaseInspectorExtensions class.
    /// </summary>
    public static class ComponentBaseInspectorExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets the naming strategy.
        /// </summary>
        /// <param name="componentBaseInspector">The component base inspector.</param>
        /// <returns>A <see cref="Orion.Framework.DataLayer.NH.Domain.IComponentNamingStrategy"/></returns>
        public static IComponentNamingStrategy GetNamingStrategy ( this IComponentBaseInspector componentBaseInspector )
        {
            IComponentNamingStrategy componentNamingStrategy = null;

            var namingAttributes = componentBaseInspector.Property.MemberInfo.GetCustomAttributes (
                typeof( ComponentNamingStrategyAttribute ), false );
            if ( namingAttributes.Count () > 0 )
            {
                var namingAttribute = namingAttributes[0] as ComponentNamingStrategyAttribute;
                if ( namingAttribute != null )
                {
                    componentNamingStrategy = namingAttribute.ComponentNamingStrategy;
                }
            }

            var componentType = componentBaseInspector.Property.PropertyType;

            if ( componentNamingStrategy == null )
            {
                var classNamingAttributes = componentType.GetCustomAttributes (
                    typeof( ComponentNamingStrategyAttribute ), false );
                if ( classNamingAttributes.Count () > 0 )
                {
                    var classNamingAttribute = classNamingAttributes[0] as ComponentNamingStrategyAttribute;
                    if ( classNamingAttribute != null )
                    {
                        componentNamingStrategy = classNamingAttribute.ComponentNamingStrategy;
                    }
                }
            }

            return componentNamingStrategy ?? new ComponentPropertyReplaceStrategy ();
        }

        #endregion
    }
}
