using System;
using Orion.Framework.Logs.Abstractions;

namespace Orion.Framework.Logs {
    /// <summary>
  
    /// </summary>
    public interface ILog {
        /// <summary>
      
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="action"></param>
        ILog Set<TContent>( Action<TContent> action ) where TContent : ILogContent;
        /// <summary>
        /// 
        /// </summary>
        bool IsDebugEnabled { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsTraceEnabled { get; }
        /// <summary>
        /// 
        /// </summary>
        void Trace();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Trace( string message );
        /// <summary>
        /// 
        /// </summary>
        void Debug();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Debug( string message );
        /// <summary>
        /// 
        /// </summary>
        void Info();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Info( string message );
        /// <summary>
        /// 
        /// </summary>
        void Warn();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Warn( string message );
        /// <summary>
        /// 
        /// </summary>
        void Error();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Error( string message );
        /// <summary>
        /// 
        /// </summary>
        void Fatal();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Fatal( string message );
    }
}
