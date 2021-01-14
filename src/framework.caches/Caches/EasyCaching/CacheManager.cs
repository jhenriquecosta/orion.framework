using System;
using System.Threading.Tasks;
using EasyCaching.Core;

namespace Orion.Framework.Caches.EasyCaching {
    /// <summary>
    /// 
    /// </summary>
    public class CacheManager : ICache {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEasyCachingProvider _provider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public CacheManager( IEasyCachingProvider provider ) {
            _provider = provider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public bool Exists( string key ) {
            return _provider.Exists( key );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expiration"></param>
        public T Get<T>( string key, Func<T> func, TimeSpan? expiration = null ) 
        {
            var result = _provider.Get( key, func, GetExpiration( expiration ) );          
            return result.Value;
        }
        public T Get<T>(string key)
        {
            var result = _provider.Get<T>(key);
            return result.Value;
        }
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> func, TimeSpan? expiration = null)
        {
            var result = await _provider.GetAsync(key, func, GetExpiration(expiration));
            return result.Value;
        }
        public async Task<T> GetAsync<T>(string key)
        {
            var result = await _provider.GetAsync<T>(key);
            return result.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        private TimeSpan GetExpiration( TimeSpan? expiration ) {
            expiration = expiration ?? TimeSpan.FromHours( 12 );
            return expiration.SafeValue();
        }

        /// <summary>
        /// ，，
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration"></param>
        public bool TryAdd<T>( string key, T value, TimeSpan? expiration = null ) {
            var result = _provider.TrySet( key, value, GetExpiration( expiration ) );
            return result;
        }
        public Task<bool> TryAddAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            return  _provider.TrySetAsync(key, value, GetExpiration(expiration));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Remove( string key ) {
            _provider.Remove( key );
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear() {
            _provider.Flush();
        }
    }
}
