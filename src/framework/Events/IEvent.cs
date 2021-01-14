using System;

namespace Orion.Framework.HandleEvents {
    /// <summary>
    /// 
    /// </summary>
    public interface IEvent {
        /// <summary>
        /// 
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        DateTime Time { get; }
    }
}
