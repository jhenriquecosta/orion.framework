using System.Linq;
using Exceptionless;
using Microsoft.Extensions.Logging;
using el = Exceptionless;
using NLogs = NLog;
using Orion.Framework.Logs.Abstractions;
using Orion.Framework.Logs.Contents;
using Orion.Framework.Logs.NLog;

namespace Orion.Framework.Logs.Exceptionless {
  
    public class ExceptionlessProvider : ILogProvider {
       
        private readonly NLogs.ILogger _logger;
      
        private readonly el.ExceptionlessClient _client;
      
        private int _line;

      
        public ExceptionlessProvider( string logName ) {
            _logger = NLogProvider.GetLogger( logName );
            _client = el.ExceptionlessClient.Default;
        }

       
        public string LogName => _logger.Name;

       
        public bool IsDebugEnabled => _logger.IsDebugEnabled;

      
        public bool IsTraceEnabled => _logger.IsTraceEnabled;

    
        public void WriteLog( LogLevel level, ILogContent content ) {
            InitLine();
            var builder = CreateBuilder( level, content );
            SetUser( content );
            SetSource( builder, content );
            SetReferenceId( builder, content );
            AddProperties( builder, content as ILogConvert );
            builder.Submit();
        }

      
        private void InitLine() {
            _line = 1;
        }

      
        private EventBuilder CreateBuilder( LogLevel level, ILogContent content ) {
            if( content.Exception != null )
                return _client.CreateException( content.Exception );
            return _client.CreateLog( GetMessage( content ), ConvertTo( level ) );
        }

      
        private string GetMessage( ILogContent content ) {
            if ( content is ICaption caption && string.IsNullOrWhiteSpace( caption.Caption ) == false )
                return caption.Caption;
            if( content.Content.Length > 0 )
                return content.Content.ToString();
            return content.TraceId;
        }

        
        private el.Logging.LogLevel ConvertTo( LogLevel level ) {
            switch( level ) {
                case LogLevel.Trace:
                    return el.Logging.LogLevel.Trace;
                case LogLevel.Debug:
                    return el.Logging.LogLevel.Debug;
                case LogLevel.Information:
                    return el.Logging.LogLevel.Info;
                case LogLevel.Warning:
                    return el.Logging.LogLevel.Warn;
                case LogLevel.Error:
                    return el.Logging.LogLevel.Error;
                case LogLevel.Critical:
                    return el.Logging.LogLevel.Fatal;
                default:
                    return el.Logging.LogLevel.Off;
            }
        }

        private void SetUser( ILogContent content ) {
            if ( string.IsNullOrWhiteSpace( content.UserId ) )
                return;
            _client.Configuration.SetUserIdentity( content.UserId );
        }

       
        private void SetSource( EventBuilder builder, ILogContent content ) {
            if ( string.IsNullOrWhiteSpace( content.Url ) )
                return;
            builder.SetSource( content.Url );
        }

        
        private void SetReferenceId( EventBuilder builder, ILogContent content ) {
            builder.SetReferenceId( content.TraceId );
        }

        /// </summary>
        private void AddProperties( EventBuilder builder, ILogConvert content ) {
            if ( content == null )
                return;
            foreach ( var parameter in content.To().OrderBy( t => t.SortId ) ) {
                if ( string.IsNullOrWhiteSpace( parameter.Value.SafeString() ) )
                    continue;
                builder.SetProperty( $"{GetLine()}. {parameter.Text}", parameter.Value );
            }
        }

        
        private string GetLine() {
            return _line++.ToString().PadLeft( 2, '0' );
        }
    }
}
