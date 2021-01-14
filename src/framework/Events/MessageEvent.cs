using System.Text;

namespace Orion.Framework.HandleEvents {
    /// <summary>
    /// 
    /// </summary>
    public class MessageEvent : Event, IMessageEvent {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override string ToString() {
            var result = new StringBuilder();
            result.AppendLine( $": {Id}" );
            result.AppendLine( $":{Time.ToMillisecondString()}" );
            if( string.IsNullOrWhiteSpace( Name ) == false )
                result.AppendLine( $":{Name}" );
            if( string.IsNullOrWhiteSpace( Callback ) == false )
                result.AppendLine( $":{Callback}" );
            result.Append( $"：{Orion.Framework.Json.Json.ToJson( Data )}" );
            return result.ToString();
        }
    }
}
