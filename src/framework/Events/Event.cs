using System;
using System.Text;
using Orion.Framework.Dependency;

namespace Orion.Framework.HandleEvents 
{
    /// <summary>
    /// 
    /// </summary>
    public class Event : IEvent {
        /// <summary>
        /// 
        /// </summary>
        public Event() {
            Id = Orion.Framework.Helpers.Id.Guid();
            Time = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// 
        /// </summary>
        public override string ToString() {
            var result = new StringBuilder();
            result.AppendLine( $": {Id}" );
            result.AppendLine( $":{Time.ToMillisecondString()}" );
            result.Append( $"：{this}" );
            return result.ToString();
        }
    }
}
