using System;
using Orion.Framework.Logs.Abstractions;

namespace Orion.Framework.Logs.Core {
    /// <summary>
   
    /// </summary>
    public class NullLog : ILog {
        /// <summary>
        
        /// </summary>
        public static readonly ILog Instance = new NullLog();

        /// <summary>
      
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="action"></param>
        public ILog Set<TContent>( Action<TContent> action ) where TContent : ILogContent {
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDebugEnabled => false;

        /// <summary>
        /// 
        /// </summary>
        public bool IsTraceEnabled => false;

        /// <summary>
        /// 
        /// </summary>
        public void Trace() {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Trace( string message ) {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Debug() {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Debug( string message ) {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Info() {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Info( string message ) {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Warn() {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Warn( string message ) {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Error() {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Error( string message ) {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Fatal() {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Fatal( string message ) {
        }
    }
}
