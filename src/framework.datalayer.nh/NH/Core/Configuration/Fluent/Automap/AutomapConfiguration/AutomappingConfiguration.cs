using System;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using NLog;
using System.ComponentModel.DataAnnotations.Schema;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.NH.Fluent
{
    /// <summary>
    /// AutomappingConfiguration class.
    /// </summary>
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        #region Public Methods

        /// <summary>
        /// Checks to see if class is layer supertype.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if is supertype; otherwise false.</returns>
        public override bool AbstractClassIsLayerSupertype ( Type type )
        {
            var attributes = type.GetCustomAttributes (typeof(NotLayerSupertypeAttribute), false);

            var result = attributes.Length <= 0 && base.AbstractClassIsLayerSupertype ( type );
         
            return result;
        }


        public override string GetComponentColumnPrefix(Member member)
        {
            return "";
        }
        /// <summary>
        /// Determines whether the specified type is component.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns><c>true</c> if the specified type is component; otherwise, <c>false</c>.</returns>
        public override bool IsComponent ( Type type )
        {
            return type.IsNHibernateComponent ();
        }

        /// <summary>
        /// Determines whether the specified member is id.
        /// </summary>
        /// <param name="member">The member to check.</param>
        /// <returns><c>true</c> if the specified member is id; otherwise, <c>false</c>.</returns>
        public override bool IsId ( Member member )
        {
            var declaringType = member.MemberInfo.DeclaringType;
            var IsId = member.Name.Equals("Id");

            if (typeof(IEntity).IsAssignableFrom(declaringType) || IsId)
            {
                return member.Name == "Id";
            }


            return false;
        }

        /// <summary>
        /// Determins whether should map type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if should map type; otherwise false.</returns>
        public override bool ShouldMap ( Type type )
        {
            var baseShouldMap = base.ShouldMap ( type );
            var isEntity = typeof( IEntity ).IsAssignableFrom ( type );
            var isAutoMapping = type.GetCustomAttributes(typeof(AutoMappingAttribute), false).Length > 0;
            var isNotMapped = type.GetCustomAttributes(typeof(NotMappedAttribute), false).Length > 0;
            

            var ignoreMappingTypeAttributes = type.GetCustomAttributes ( typeof( IgnoreMappingAttribute ), false );

            var result = baseShouldMap && ( isEntity || isAutoMapping ) && !type.IsInterface && ignoreMappingTypeAttributes.Length == 0;


          
            return result;
           
        }

        /// <summary>
        /// Determins whether should map member.
        /// </summary>
        /// <param name="member">The member to check.</param>
        /// <returns>True if should map member; otherwise false.</returns>
        public override bool ShouldMap ( Member member )
        {
            var ignoreMappingMemberAttributes = member.MemberInfo.GetCustomAttributes (typeof( IgnoreMappingAttribute ), false );
            var ignoreMappingMemberTypeAttributes = member.PropertyType.GetCustomAttributes (typeof( IgnoreMappingAttribute ), false );
            var notMappedAttributes = member.MemberInfo.GetCustomAttributes(typeof(NotMappedAttribute), false);
            
            var shouldMap = base.ShouldMap ( member ) && (ignoreMappingMemberTypeAttributes.Length == 0 && ignoreMappingMemberAttributes.Length == 0  && notMappedAttributes.Length == 0);

            return shouldMap;
        }

        #endregion
    }
}
