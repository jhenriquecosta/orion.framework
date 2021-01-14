using System;
using System.Text;

namespace Orion.Framework.Logs.Abstractions {
    /// <summary>
    
    /// </summary>
    public interface ILogContent {
        /// <summary>
       
        /// </summary>
        string LogName { get; set; }
        /// <summary>
   
        /// </summary>
        string Level { get; set; }
        /// <summary>
       
        /// </summary>
        string TraceId { get; set; }
        /// <summary>
     
        /// </summary>
        string OperationTime { get; set; }
        /// <summary>
      
        /// </summary>
        string Duration { get; set; }
        /// <summary>
       
        /// </summary>
        string Ip { get; set; }
        /// <summary>
     
        /// </summary>
        string Host { get; set; }
        /// <summary>
    
        /// </summary>
        string ThreadId { get; set; }
        /// <summary>
   
        /// </summary>
        string Browser { get; set; }
        /// <summary>
       
        /// </summary>
        string Url { get; set; }
        /// <summary>
      
        /// </summary>
        string UserId { get; set; }
        /// <summary>
      
        /// </summary>
        StringBuilder Content { get; set; }
        /// <summary>
      
        /// </summary>
        Exception Exception { get; set; }
    }
}
