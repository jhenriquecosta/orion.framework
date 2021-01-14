using System;
using System.Threading.Tasks;

namespace Orion.Framework.Caches
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICache
    {

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        bool Exists(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expiration"></param>
        T Get<T>(string key, Func<T> func, TimeSpan? expiration = null);
        T Get<T>(string key);
        Task<T> GetAsync<T>(string key, Func<Task<T>> func, TimeSpan? expiration = null);
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration"></param>
        bool TryAdd<T>(string key, T value, TimeSpan? expiration = null);
        Task<bool> TryAddAsync<T>(string key, T value, TimeSpan? expiration = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
        /// <summary>
        /// 
        /// </summary>
        void Clear();
    }
}
