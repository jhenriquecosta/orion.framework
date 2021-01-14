using Orion.Framework.Dependency;
using System;
using System.Linq;
using System.Reflection;
using Orion.Framework.Helpers;

namespace Orion.Framework
{
    public static partial class TypeHelper
    {
        public static TItem CreateInstance<TItem>()
        {
            var newItemCreator = new Lazy<Func<TItem>>(() => Reflection.CreateNewItem<TItem>());
            return newItemCreator.Value();
        }
        public static bool IsFunc(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Type type = obj.GetType();
            if (!type.GetTypeInfo().IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(Func<>);
        }

        public static bool IsFunc<TReturn>(object obj)
        {
            return obj != null && obj.GetType() == typeof(Func<TReturn>);
        }

        public static bool IsPrimitiveExtendedIncludingNullable(Type type)
        {
           
            if (IsPrimitiveExtended(type))
            {
                return true;
            }

            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return IsPrimitiveExtended(type.GenericTypeArguments[0]);
            }

            return false;
        }

        private static bool IsPrimitiveExtended(Type type)
        {
            if (type.GetTypeInfo().IsPrimitive)
            {
                return true;
            }

            return type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
        }


        //create by type
        public static object CreateInstance(string asm, string fullName)
        {
            return CreateInstance(Assembly.Load(asm), fullName);
        }
        public static object CreateInstance(Assembly asm, string fullName)
        {
            var namespaces = asm.GetTypes().Select(t => t.Namespace);
            foreach (var item in namespaces)
            {
                var type = asm.GetType(item + "." + fullName);
                if (type != null) return Activator.CreateInstance(type);
            }
            return null;
        }

        public static Type GetType(string instance,Type Parent = null)
        {
            var assemblies = Bootstrapper.GetAssemblies(); //busca os assemblies
            if (assemblies == null) assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type type = null;
            if (Parent != null)
            {
                type = assemblies.SelectMany(f => f.GetTypes()).FirstOrDefault(t => t.FullName.Contains(instance) && t.IsParentOf(Parent));
            }
            return type;
        }
        public static object CreateInstance(Type instance, object[] args = null)
        {
            return Activator.CreateInstance(instance);
        }


        public static object CreateInstance(string strFullyQualifiedName)
        {
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null) return Activator.CreateInstance(type);
            var assemblies = Bootstrapper.GetAssemblies(); //busca os assemblies
            if (assemblies == null) assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in assemblies)
            {
                //  type = asm.GetType(strFullyQualifiedName);
                type = GetType(asm, strFullyQualifiedName);
                if (type != null) return Activator.CreateInstance(type);
            }
            return null;
        }
        /// <summary>
        /// Cria uma instância com tipo e parâmetros definidos.
        /// </summary>
        /// <param name="type">O tipo da instância a ser criada.</param>
        /// <param name="args">Os parâmetros da instância.</param>
        /// <returns>A instância. Uma conversão explicita será necessária.</returns>
        public static object Create(System.Type type, object[] args)
        {
            System.Type t = type.Assembly.GetType(type.FullName);
            return t.InvokeMember(string.Empty,
                BindingFlags.DeclaredOnly | BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.CreateInstance, null, null, args);
        }
        /// <summary>
        /// Cria uma instância de uma classe presente em um assembly.
        /// </summary>
        /// <param name="assemblyFile">O assembly.</param>
        /// <param name="typeFullName">Nome completo do tipo.</param>
        /// <param name="args">Os argumentos.</param>
        /// <returns>A instância. Uma conversão explicita será necessária.</returns>
		public static object CreateInstance(string assemblyFile, string typeFullName, object[] args)
        {
            Assembly a = Assembly.LoadFrom(assemblyFile);
            System.Type type = a.GetType(typeFullName);
            return type.InvokeMember(string.Empty,
                BindingFlags.DeclaredOnly | BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.CreateInstance, null, null, args);
        }
        // Type
        public static Type GetType(Assembly asm, string fullName)
        {
            Type xtValue = null;
            try
            {
                if (fullName.IndexOf('.') > 0)
                    xtValue = asm.GetTypes().SingleOrDefault(t => t.FullName.Equals(fullName));
                else
                  //  asm.GetTypes().ToList().ForEach(f => Trace.WriteLine($"{ f.Namespace}-{f.FullName}-{fullName}"));
                    xtValue = asm.GetTypes().SingleOrDefault(t => t.FullName.Equals(t.Namespace + "." + fullName));
            }
            catch (Exception)
            { }
            return xtValue;
        }
        public static Type GetType(string fullName)
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = GetType(asm, fullName);
                if (type != null) return type;
            }
            return null;
        }
    }
}
