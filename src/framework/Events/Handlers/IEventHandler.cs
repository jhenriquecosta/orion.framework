using System.Threading.Tasks;

namespace Orion.Framework.HandleEvents.Handlers {
    /// <summary>
    /// 
    /// </summary>
    public interface IEventHandler {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : IEvent {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        Task HandleAsync( TEvent @event ) ;
    }
}
