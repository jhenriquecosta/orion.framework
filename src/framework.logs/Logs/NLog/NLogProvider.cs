using System;
using Microsoft.Extensions.Logging;
using NLogs = NLog;
using Orion.Framework.Logs.Abstractions;
using Orion.Framework.Logs.Formats;

namespace Orion.Framework.Logs.NLog {
    /// <summary>
   
    /// </summary>
    public class NLogProvider : ILogProvider {
        /// <summary>
    
        /// </summary>
        private readonly NLogs.ILogger _logger;
        /// <summary>
     
        /// </summary>
        private readonly ILogFormat _format;

        /// <summary>
     
        /// </summary>
        /// <param name="logName"></param>
        /// <param name="format"></param>
        public NLogProvider( string logName, ILogFormat format = null ) {
            _logger = GetLogger( logName );
            _format = format;
        }

        /// <summary>
     
        /// </summary>
        /// <param name="logName"></param>
        public static NLogs.ILogger GetLogger( string logName ) {
            return NLogs.LogManager.GetLogger( logName );
        }


        public string LogName => _logger.Name;


        public bool IsDebugEnabled => _logger.IsDebugEnabled;

        /// <summary>
     
        /// </summary>
        public bool IsTraceEnabled => _logger.IsTraceEnabled;

        /// <summary>
        
        /// </summary>
        /// <param name="level"></param>
        /// <param name="content"></param>
        public void WriteLog( LogLevel level, ILogContent content ) {
            var provider = GetFormatProvider();
            if( provider == null ) {
                _logger.Log( ConvertTo( level ), content );
                return;
            }
            _logger.Log( ConvertTo( level ), provider, content );
        }

        /// <summary>
     
        /// </summary>
        private NLogs.LogLevel ConvertTo( LogLevel level ) {
            switch( level ) {
                case LogLevel.Trace:
                    return NLogs.LogLevel.Trace;
                case LogLevel.Debug:
                    return NLogs.LogLevel.Debug;
                case LogLevel.Information:
                    return NLogs.LogLevel.Info;
                case LogLevel.Warning:
                    return NLogs.LogLevel.Warn;
                case LogLevel.Error:
                    return NLogs.LogLevel.Error;
                case LogLevel.Critical:
                    return NLogs.LogLevel.Fatal;
                default:
                    return NLogs.LogLevel.Off;
            }
        }

        /// <summary>
       
        /// </summary>
        private IFormatProvider GetFormatProvider() {
            if( _format == null )
                return null;
            return new FormatProvider( _format );
        }
    }
}
