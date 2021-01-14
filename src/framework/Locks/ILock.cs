using System;

namespace Orion.Framework.Locks {
    /// <summary>
    /// 
    /// </summary>
    public interface ILock {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiration"></param>
        bool Lock( string key, TimeSpan? expiration = null );
        /// <summary>
        /// 
        /// </summary>
        void UnLock();
    }
}
