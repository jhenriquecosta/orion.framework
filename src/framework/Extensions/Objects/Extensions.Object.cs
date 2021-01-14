using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Orion.Framework.Helpers;


namespace Orion.Framework
{
    /// <summary>
    ///     Extension methods for all objects.
    /// </summary>
    public static class ObjectExtensions
    {
        //public static AttachedProperty<object, object> InternalId= new AttachedProperty<object, object>(nameof(InternalId));
        //public static string ObjectId(this object obj)
        //{
        //    var id = obj.GetAttachedValue(InternalId);
        //    if (id.IsNull())
        //    {
        //        id = Guid.NewGuid().ToString();
        //       obj.SetAttachedValue(InternalId,id);
        //    }
        //    return id.ToString();
        //}
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
        public static TItem CreateDefault<TItem>(this object obj)
        {
            var newItemCreator = new Lazy<Func<TItem>>(() => Reflection.CreateNewItem<TItem>());
            return newItemCreator.Value();
        }


        /// <summary>
        /// Gets whether <see cref="PropertyInfo">propertyInfo</see> is an Auto Property.
        /// </summary>
        /// <param name="propertyInfo">Property Info to check.</param>
        /// <returns>A <see cref="bool">Boolean</see>.</returns>
        public static bool IsAutoProperty(this PropertyInfo propertyInfo)
        {
            var isAutoProperty = false;

            var getMethod = propertyInfo.GetGetMethod(true);
            var setMethod = propertyInfo.GetSetMethod(true);

            if (getMethod != null && setMethod != null)
            {
                var getMethodIsCompilerGenerated = getMethod.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any();
                var setMethodIsCompilerGenerated = setMethod.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any();

                if (getMethodIsCompilerGenerated && setMethodIsCompilerGenerated)
                {
                    isAutoProperty = true;
                }
            }

            return isAutoProperty;
        }

        /// <summary>
        /// Gets Whether property info is settable.
        /// </summary>
        /// <param name="propertyInfo">Property info to check.</param>
        /// <returns>A <see cref="bool">Boolean</see>.</returns>
        public static bool IsReadonly(this PropertyInfo propertyInfo)
        {
            var setter = propertyInfo.GetSetMethod(false);
            return setter == null;
        }

        public static TAttribute GetAttribute<TAttribute>(this MemberInfo memberInfo) where TAttribute : Attribute
        {
            try
            {
                var ret = (TAttribute)memberInfo.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault();
                return ret;
            }
            catch(Exception )
            {
                return null;
            }
        }

        public static Dictionary<PropertyInfo, TAttribute> GetAllPropertiesWithAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            var result = new Dictionary<PropertyInfo, TAttribute>();
            foreach (var propertyInfo in type.GetProperties())
            {
                var attribute = propertyInfo.GetAttribute<TAttribute>();
                if (attribute != null)
                {
                    result.Add(propertyInfo, attribute);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns real (not nullable) type of property.
        /// </summary>
        public static Type GetRealType(this Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        public static bool IsReallyDateTime(this Type type)
        {
            return type.GetRealType() == typeof(DateTime);
        }

        public static string GetFullMethodName(this MethodInfo methodInfo)
        {
            return $"{methodInfo.DeclaringType.Name}.{methodInfo.Name}";
        }

        public static bool IsParentOf(this Type type,Type parent)
        {
            return ReflectionHelper.IsAssignableToGenericType(type, parent);
        }
       
        //public static bool IsAutoMapping(this Type obj)
        //{
        //    var vInter = obj.GetInterfaces();
        //    var ret = vInter.Contains(typeof(IAutoMapped));
        //   // Console.WriteLine($"{obj.FullName}->{ret.ToString()}");
        //    return ret;
        //}
        public static object ExecuteMethod(this object obj, string methodName, object[] args)
        {

            MethodInfo methodInfo = obj.GetType().GetMethod(methodName);
            object result = null;

            if (methodInfo != null)
            {
               
                ParameterInfo[] parameters = methodInfo.GetParameters();
                //  object classInstance = Activator.CreateInstance(type, null);

                if (parameters.Length == 0)
                {
                    // This works fine
                    result = methodInfo.Invoke(obj, null);
                }
                else
                {
                    // object[] parametersArray = new object[] { "Hello" };

                    // The invoke does NOT work;
                    // it throws "Object does not match target type"             
                    result = methodInfo.Invoke(obj, args);
                }
            }
            return result;
        }
        /// <summary>
        ///     Used to simplify and beautify casting an object to a type.
        /// </summary>
        /// <typeparam name="T">Type to be casted</typeparam>
        /// <param name="obj">Object to cast</param>
        /// <returns>Casted object</returns>
        [NotNull]
        public static T As<T>([NotNull] this object obj)
            where T : class
        {
            return (T)obj;
        }

        /// <summary>
        ///     Converts given object to a value type using <see cref="Convert.ChangeType(object,System.TypeCode)" /> method.
        /// </summary>
        /// <param name="obj">Object to be converted</param>
        /// <typeparam name="T">Type of the target object</typeparam>
        /// <returns>Converted object</returns>
        //public static T To<T>([NotNull] this object obj) where T : struct
        //{
        //    return (T)System.Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        //}

        /// <summary>
        ///     Check if an item is in a list.
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <param name="list">List of items</param>
        /// <typeparam name="T">Type of the items</typeparam>
        public static bool IsIn<T>(this T item, [NotNull] params T[] list)
        {
            return list.Contains(item);
        }

        /// <summary>
        ///     Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        //public static void ForEach<T>(this IEnumerable<T> items,  Action<T> action)
        //{
        //    if (items == null)
        //    {
        //        return;
        //    }

        //    foreach (T obj in items)
        //    {
        //        action(obj);
        //    }
        //}
        
    }
}
