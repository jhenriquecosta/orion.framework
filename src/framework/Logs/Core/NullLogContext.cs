using System.Diagnostics;
using Orion.Framework.Logs.Abstractions;

namespace Orion.Framework.Logs.Core {
    /// <summary>
    /// 
    /// </summary>
    public class NullLogContext : ILogContext {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ILogContext Instance = new NullLogContext();
        /// <summary>
        /// 
        /// </summary>
        public string TraceId => string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Stopwatch Stopwatch => new Stopwatch();
        /// <summary>
        /// IP
        /// </summary>
        public string Ip => string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Host => string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Browser => string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Url => string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public int Order => 0;

        /// <summary>
        /// 
        /// </summary>
        public void UpdateContext()
        {
        }
    }
}
