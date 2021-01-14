namespace Orion.Framework.HandleEvents {
    /// <summary>
    /// 
    /// </summary>
    public interface IMessageEvent : IEvent {
        /// <summary>
        /// 
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        object Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string Callback { get; set; }
    }
}
