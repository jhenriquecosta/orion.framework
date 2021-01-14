using NHibernate.Mapping.ByCode;
using System;
using System.Linq;
using System.Reflection;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Extensions
{
    /// <summary>
    /// Provides extensions methods to the IModelInspector interface
    /// </summary>
    public static class IModelInspectorExtensions
    {
        /// <summary>
        /// Gets the name of the identifier property for a given entity
        /// </summary>
        /// <param name="modelInspector">An instance the model inspector</param>
        /// <param name="entityType">The type of the entity</param>
        /// <returns>The name of the ID property</returns>
        public static string GetIdentifierPropertyName(this IModelInspector modelInspector, Type entityType)
        {
            string idPropertyName = null;
            if (modelInspector.GetType().Equals(typeof(SimpleModelInspector)))
            {
                var declaredModelField = typeof(SimpleModelInspector).GetField("declaredModel", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);
                if (declaredModelField != null)
                {
                    IModelExplicitDeclarationsHolder modelHolder = declaredModelField.GetValue(modelInspector) as IModelExplicitDeclarationsHolder;
                    if (modelHolder != null)
                    {
                        idPropertyName = modelHolder.Poids.Where(i => entityType.GetBaseTypes().Contains(i.DeclaringType)).Select(i => i.Name).SingleOrDefault();
                    }
                }
            }

            return idPropertyName;
        }

        /// <summary>
        /// Gets the name of the version property for a given entity
        /// </summary>
        /// <param name="modelInspector">An instance the model inspector</param>
        /// <param name="entityType">The type of the entity</param>
        /// <returns>The name of the Version property</returns>
        public static string GetVersionPropertyName(this IModelInspector modelInspector, Type entityType)
        {
            string versionPropertyName = null;
            if (modelInspector.GetType().Equals(typeof(SimpleModelInspector)))
            {
                var declaredModelField = typeof(SimpleModelInspector).GetField("declaredModel", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);
                if (declaredModelField != null)
                {
                    IModelExplicitDeclarationsHolder modelHolder = declaredModelField.GetValue(modelInspector) as IModelExplicitDeclarationsHolder;
                    if (modelHolder != null)
                    {
                        versionPropertyName = modelHolder.VersionProperties.Where(i => entityType.GetBaseTypes().Contains(i.DeclaringType)).Select(i => i.Name).SingleOrDefault();
                    }
                }
            }

            return versionPropertyName;
        }

        /// <summary>
        /// Gets the member info of the identifier property for a given entity
        /// </summary>
        /// <param name="modelInspector">An instance the model inspector</param>
        /// <param name="entityType">The type of the entity</param>
        /// <returns>The member info of the ID property</returns>
        public static MemberInfo GetIdentifierMember(this IModelInspector modelInspector, Type entityType)
        {
            string idPropertyName = GetIdentifierPropertyName(modelInspector, entityType);

            if (string.IsNullOrEmpty(idPropertyName))
            {
                throw new System.Configuration.ConfigurationErrorsException(string.Format("Missing identifier property. Wrong mapping or missing mapping class for the type {0}", entityType.GetType().FullName));
            }

            return entityType.GetMember(idPropertyName).SingleOrDefault();
        }

        /// <summary>
        /// Gets the member info of the version property for a given entity
        /// </summary>
        /// <param name="modelInspector">An instance the model inspector</param>
        /// <param name="entityType">The type of the entity</param>
        /// <returns>The member info of the Version property</returns>
        public static MemberInfo GetVersionMember(this IModelInspector modelInspector, Type entityType)
        {
            string versionPropertyName = GetVersionPropertyName(modelInspector, entityType);

            return !string.IsNullOrEmpty(versionPropertyName) ? entityType.GetMember(versionPropertyName).SingleOrDefault() : null;
        }
    }
}
