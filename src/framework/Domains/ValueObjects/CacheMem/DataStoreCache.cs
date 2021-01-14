using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orion.Framework.Domains.ValueObjects
{
    public static class DataStoreCache
    {

        private static ConcurrentDictionary<string, object> _cache;

        private static object _sync;
        private static string _key;

        static DataStoreCache()
        {
            _cache = new ConcurrentDictionary<string, object>();
            _sync = new object();
        }
        public static void AddOrUpdate<T>(string key,T value)
        {
            try
            {
                Remove(key);
                Add(key, value);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #region Add   
        public static void Add<T>(string key, T value)
        {
            
            Type type = typeof(T);
            _key = $"{key}";
            //if (value.GetType() != type) throw new ApplicationException(String.Format("The type of value passed to cache {0} does not match the cache type {1} for key {2}", value.GetType().FullName, type.FullName, key));
            lock (_sync)
            {
                //if (_cache.ContainsKey(_key)) throw new ApplicationException(String.Format("An object with key '{0}' already exists", key));
                lock (_sync)
                {
                    _cache.GetOrAdd(_key, value);
                }
            }
        }
        #endregion
        #region FindAll()
        public static List<KeyValuePair<string, object>> FindAll()
        {
            return _cache.ToList();
        }
        #endregion
        #region Find
        public static bool Find(string key)
        {
            lock (_sync)
            {
                return _cache.ContainsKey(key);
            }
        }

        #endregion
        #region Get
        public static T Get<T>(string key)
        {
            return (T)Get(key);
        }
        public static object Get(string key)
        {

            lock (_sync)
            {
                if (_cache.ContainsKey(key) == false)
                    return null;//throw new ApplicationException(String.Format("An object with key '{0}' does not exists", key));
                lock (_sync)
                {
                    return _cache[key];
                }
            }
        }
        #endregion
        #region Create

        public static T Create<T>(string key, params object[] constructorParameters) where T : class
        {
            Type type = typeof(T);
            T value = (T)Activator.CreateInstance(type, constructorParameters);
            lock (_sync)
            {
                if (_cache.ContainsKey(key + type.Name))
                    throw new ApplicationException(String.Format("An object with key '{0}' already exists", key));
                lock (_sync)
                {
                    _cache.GetOrAdd(key + type.Name, value);
                }
            }
            return value;
        }
        public static T Create<T>(params object[] constructorParameters) where T : class
        {
            Type type = typeof(T);
            T value = (T)Activator.CreateInstance(type, constructorParameters);
            lock (_sync)
            {
                if (_cache.ContainsKey(type.Name))
                    throw new ApplicationException(String.Format("An object of type '{0}' already exists", type.Name));
                lock (_sync)
                {
                    _cache.GetOrAdd(type.Name, value);
                }

            }
            return value;
        }
        #endregion
        #region Remove
        public static void Remove(string key)
        {

            object _value;
            _key = $"{key}";
            
            lock (_sync)
            {
                if (_cache.ContainsKey(_key) == false) return; //throw new ApplicationException(String.Format("An object with key '{0}' does not exists in cache", key));
                lock (_sync)
                {
                    _cache.TryRemove (_key,out _value);
                }
            }
        }
        #endregion
        #region Count
        public static int Count()
        {
            return FindAll().Count();
        }
        #endregion
        #region Disabled
        //public static bool Find<T>() where T : class
        //{
        //    Type type = typeof(T);
        //    lock (_sync)
        //    {
        //        return _cache.ContainsKey(type.Name);
        //    }
        //}
        //public static T Get<T>() where T : class
        //{
        //    Type type = typeof(T);
        //    lock (_sync)
        //    {
        //        if (_cache.ContainsKey(type.Name) == false)
        //            throw new ApplicationException("An object of the desired type does not exist: " + type.Name);
        //        lock (_sync)
        //        {
        //            return (T)_cache[type.Name];
        //        }
        //    }
        //}
        //public static void Remove<T>()
        //{
        //    Type type = typeof(T);
        //    lock (_sync)
        //    {
        //        if (_cache.ContainsKey(type.Name) == false)
        //            throw new ApplicationException(String.Format("An object of type '{0}' does not exists in cache", type.Name));
        //        lock (_sync)
        //        {
        //            _cache.Remove(type.Name);

        //        }
        //    }
        //}
        #endregion
    }
}