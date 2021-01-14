using Orion.Framework.Logs.Abstractions;

namespace Orion.Framework.Logs.Core {
    /// <summary>
    /// 
    /// </summary>
    public class NullLogFormat : ILogFormat {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ILogFormat Instance = new NullLogFormat();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public string Format( ILogContent content ) {
            return "";
        }
    }
}
