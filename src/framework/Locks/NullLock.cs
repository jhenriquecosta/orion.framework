using System;

namespace Orion.Framework.Locks {
    /// <summary>
    /// 
    /// </summary>
    public class NullLock : ILock {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ILock Instance = new NullLock();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiration"></param>
        public bool Lock( string key, TimeSpan? expiration = null ) {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnLock() {
        }
    }
}
