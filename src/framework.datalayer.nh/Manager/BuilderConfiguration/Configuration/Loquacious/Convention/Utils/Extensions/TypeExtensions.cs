using System;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Extensions
{
    /// <summary>
    /// Provides extension methods fot the Type class
    /// </summary>
    internal static class TypeExt
    {
        /// <summary>
        /// Determines if this type is ancestor of another type
        /// </summary>
        /// <param name="type">This instance</param>
        /// <param name="child">The child type</param>
        /// <returns>True if this type is ancestor of child, false if not</returns>
        public static bool InheritsFrom(this Type type, Type child)
        {
            bool isAncestor = false;
            while (type != null)
            {
                if (((type != child) && (type.IsGenericType && type.GetGenericTypeDefinition() == child)) || (!type.IsGenericTypeDefinition && (type == child)))
                {
                    isAncestor = true;
                    break;
                }

                type = type.BaseType;
            }

            return isAncestor;
        }

    }
}
