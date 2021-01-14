using System;
using Orion.Framework.Caches;

namespace Orion.Framework.Locks.Default {
    /// <summary>
    /// 
    /// </summary>
    public class DefaultLock : ILock {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICache _cache;
        /// <summary>
        /// 
        /// </summary>
        private string _key;
        /// <summary>
        /// 
        /// </summary>
        private TimeSpan? _expiration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        public DefaultLock( ICache cache ) {
            _cache = cache;
        }

        /// <summary>
        /// ，，
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiration"></param>
        public bool Lock( string key, TimeSpan? expiration = null ) {
            _key = key;
            _expiration = expiration;
            if ( _cache.Exists( key ) )
                return false;
            return _cache.TryAdd( key, 1, expiration );
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnLock() {
            if ( _expiration != null )
                return;
            if( _cache.Exists( _key ) == false )
                return;
            _cache.Remove( _key );
        }
    }
}
