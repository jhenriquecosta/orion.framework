using System.Diagnostics;
using Orion.Framework.Aspects;

namespace Orion.Framework.Logs.Abstractions {
    /// <summary>
   
    /// </summary>
    [Ignore]
    public interface ILogContext {
        /// <summary>
      
        /// </summary>
        string TraceId { get; }
        /// <summary>
       
        /// </summary>
        Stopwatch Stopwatch { get; }
        /// <summary>
    
        /// </summary>
        string Ip { get; }
        /// <summary>
      
        /// </summary>
        string Host { get; }
        /// <summary>
   
        /// </summary>
        string Browser { get; }
        /// <summary>
      
        /// </summary>
        string Url { get; }
    }
}
