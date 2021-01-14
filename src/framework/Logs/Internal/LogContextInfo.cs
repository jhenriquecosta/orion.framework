using System.Diagnostics;

namespace Orion.Framework.Logs.Internal {
    /// <summary>
    
    /// </summary>
    public class LogContextInfo {
        /// <summary>
     
        public string TraceId { get; set; }

        /// <summary>
      
        /// </summary>
        public Stopwatch Stopwatch { get; set; }

        /// <summary>
     
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
   
        /// </summary>
        public string Host { get; set; }

        /// <summary>
     
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
      
        /// </summary>
        public string Url { get; set; }
    }
}
