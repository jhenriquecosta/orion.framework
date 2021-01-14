using System.Linq;
using FluentNHibernate.MappingModel.ClassBased;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.DataLayer.NH.Fluent;

namespace Orion.Framework
{
    /// <summary>
    /// ComponentMappingExtensions class.
    /// </summary>
    public static class ComponentMappingExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets the naming strategy.
        /// </summary>
        /// <param name="componentMapping">The component mapping.</param>
        /// <returns>A <see cref="Orion.Framework.DataLayer.NH.Domain.IComponentNamingStrategy"/></returns>
        public static IComponentNamingStrategy GetNamingStrategy ( this IComponentMapping componentMapping )
        {
            IComponentNamingStrategy componentNamingStrategy = null;

            var namingAttributes = componentMapping.Member.MemberInfo.GetCustomAttributes (
                typeof( ComponentNamingStrategyAttribute ), false );
            if ( namingAttributes.Count () > 0 )
            {
                var namingAttribute = namingAttributes[0] as ComponentNamingStrategyAttribute;
                if ( namingAttribute != null )
                {
                    componentNamingStrategy = namingAttribute.ComponentNamingStrategy;
                }
            }

            var componentType = componentMapping.Member.PropertyType;

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
