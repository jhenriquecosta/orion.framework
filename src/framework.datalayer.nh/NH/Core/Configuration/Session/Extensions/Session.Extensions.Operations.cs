using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Orion.Framework
{

    public static partial class SessionExtensions
    {
        private static HashSet<Type> GetLoadableTypes(this Assembly assembly)
        {
            var loadableTypes = new HashSet<Type>();
            if (assembly == null) return loadableTypes;
            try
            {
                loadableTypes.AddRange(assembly.GetTypes());
            }
            catch (ReflectionTypeLoadException e)
            {
                loadableTypes.AddRange(e.Types.Where(t => t != null));
            }
            return loadableTypes.FindAll(type => !typeof(INHibernateProxy).IsAssignableFrom(type));
        }
    }
}
