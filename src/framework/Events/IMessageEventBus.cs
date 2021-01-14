using System.Threading.Tasks;

namespace Orion.Framework.HandleEvents {
    /// <summary>
    /// 
    /// </summary>
    public interface IMessageEventBus {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event">事件param>
        Task PublishAsync<TEvent>( TEvent @event ) where TEvent : IMessageEvent;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="callback"></param>
        Task PublishAsync( string name, object data, string callback = null );
    }
}
