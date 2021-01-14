using System;
using System.Text;
using Orion.Framework.Logs.Abstractions;

namespace Orion.Framework.Logs.Contents {
    
    public class LogContent : ILogContent, ICaption {
      
        public LogContent() {
            Params = new StringBuilder();
            Content = new StringBuilder();
            Sql = new StringBuilder();
            SqlParams = new StringBuilder();
        }

       
        public string LogName { get; set; }
       
        public string Level { get; set; }
      
        public string TraceId { get; set; }
     
        public string OperationTime { get; set; }
     
        /// </summary>
        public string Duration { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
   
        /// </summary>
        public string Host { get; set; }
        /// <summary>
    
        /// </summary>
        public string ThreadId { get; set; }
        /// <summary>
      
        /// </summary>
        public string Browser { get; set; }
        /// <summary>
      
        /// </summary>
        public string Url { get; set; }
        /// <summary>
    
        /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
     
        public string Tenant { get; set; }
        /// <summary>
      
        /// </summary>
        public string Application { get; set; }
        /// <summary>
     
        /// </summary>
        public string Module { get; set; }
        /// <summary>
      
        /// </summary>
        public string Class { get; set; }
        /// <summary>
     
        /// </summary>
        public string Method { get; set; }
        /// <summary>
     
        /// </summary>
        public StringBuilder Params { get; set; }
        /// <summary>
        
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
      
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
      
        /// </summary>
        public string Role { get; set; }
        /// <summary>
     
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
      
        /// </summary>
        public StringBuilder Content { get; set; }
        /// <summary>
    
        /// </summary>
        public StringBuilder Sql { get; set; }
        /// <summary>
    
        /// </summary>
        public StringBuilder SqlParams { get; set; }
        /// <summary>
     
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
     
        /// </summary>
        public Exception Exception { get; set; }
    }
}
