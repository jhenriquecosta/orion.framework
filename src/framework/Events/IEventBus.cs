using System.Threading.Tasks;

namespace Orion.Framework.HandleEvents {
    /// <summary>
    /// 
    /// </summary>
    public interface IEventBus {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        Task PublishAsync<TEvent>( TEvent @event ) where TEvent : IEvent;
    }
}
