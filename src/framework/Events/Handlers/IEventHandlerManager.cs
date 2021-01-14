using System.Collections.Generic;

namespace Orion.Framework.HandleEvents.Handlers {
    /// <summary>
    /// 
    /// </summary>
    public interface IEventHandlerManager {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        List<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent;
    }
}
