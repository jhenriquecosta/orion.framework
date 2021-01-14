using System;
using System.Linq;
using System.Reflection;
using NHibernate.Collection.Generic;
using NHibernate.Proxy;
using Orion.Framework.DataLayer.NH.Fluent;

namespace Orion.Framework
{
    /// <summary>
    /// NHibernateExtensions class.
    /// </summary>
    public static partial class NHibernateExtensions
    {
             
        /// <summary>
        /// Recursively removes NHibernate proxies. Do not use session.Save(objt) or session.Update(objt) after unproxying, you might lose important data
        /// </summary>
        /// 
        public static void UnProxy(this object objt)
        {
            for (int i = 0; i < objt.GetType().GetProperties().Count(); i++)
            {
                try
                {
                    PropertyInfo propertyInfo = objt.GetType().GetProperties()[i];
                    var propValue = propertyInfo.GetValue(objt, null);
                    if (propValue.IsProxy())
                    {
                        System.Type objType = NHibernateProxyHelper.GetClassWithoutInitializingProxy(propValue);
                        //Creates a new NonProxyObject
                        object NonProxyObject = Activator.CreateInstance(objType);
                        //Copy everything that it can be copied
                        foreach (var prop in propValue.GetType().GetProperties())
                        {
                            try
                            {
                                object a = prop.GetValue(propValue);
                                NonProxyObject.GetType().GetProperty(prop.Name).SetValue(NonProxyObject, a);
                            }
                            catch { }
                        }
                        //Change the proxy to the real class
                        propertyInfo.SetValue(objt, NonProxyObject);
                    }

                    //Lists
                    if (propValue.GetType().IsGenericType && propValue.GetType().GetGenericTypeDefinition() == typeof(PersistentGenericBag<>))
                    {
                        try
                        {
                            int count = (int)(propValue.GetType().GetProperty("Count").GetValue(propValue));
                        }
                        catch { propertyInfo.SetValue(objt, null); }
                    }

                    if (propValue.GetType().Assembly.GetName().Name != "mscorlib" && propValue.GetType().Assembly.GetName().Name != "NHibernate")
                    {
                        // user-defined!
                        propValue.UnProxy();
                    }
                }
                catch { }
            }
        }



        #region Public Methods

        /// <summary>
        /// Determines whether the specified type is a Nhibernate component.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns><c>true</c> if the specified type is a Nhibernate component; otherwise, <c>false</c>.</returns>
        public static bool IsNHibernateComponent ( this Type type )
        {
            var nhibernateComponentAttributes = type.GetCustomAttributes ( typeof( ComponentAttribute ), false );
            var result = nhibernateComponentAttributes.Length == 1;
            return result;
        }

       
        #endregion
    }
}
