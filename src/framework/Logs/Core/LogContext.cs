using System.Diagnostics;
using Orion.Framework.Contexts;
using Orion.Framework.Helpers;
using Orion.Framework.Logs.Abstractions;
using Orion.Framework.Logs.Internal;

namespace Orion.Framework.Logs.Core {
    /// <summary>
 
    /// </summary>
    public class LogContext : ILogContext {
        /// <summary>
   
        /// </summary>
        private LogContextInfo _info;
        /// <summary>
    
        /// </summary>
        private int _orderId;
        /// <summary>
     
        /// </summary>
        private IContext _context;

        /// <summary>
     
        /// </summary>
        public LogContext() {
            _orderId = 0;
        }

        /// <summary>
   
        /// </summary>
        public virtual IContext Context => _context ?? ( _context = ContextFactory.Create() );

        /// <summary>
      
        /// </summary>
        public string TraceId => $"{GetInfo().TraceId}-{++_orderId}";

        /// <summary>
     
        /// </summary>
        public Stopwatch Stopwatch => GetInfo().Stopwatch;

        /// <summary>
   
        /// </summary>
        public string Ip => GetInfo().Ip;
        /// <summary>
     
        /// </summary>
        public string Host => GetInfo().Host;
        /// <summary>
    
        /// </summary>
        public string Browser => GetInfo().Browser;
        /// <summary>
       
        /// </summary>
        public string Url => GetInfo().Url;

        /// <summary>
    
        /// </summary>
        private LogContextInfo GetInfo() {
            if ( _info != null )
                return _info;
            var key = "Orion.Framework.Logs.LogContext";
            _info = Context.Get<LogContextInfo>( key );
            if( _info != null )
                return _info;
            _info = CreateInfo();
            Context.Add( key, _info );
            return _info;
        }

        /// <summary>
     
        /// </summary>
        protected virtual LogContextInfo CreateInfo() {
            return new LogContextInfo {
                TraceId = GetTraceId(),
                Stopwatch = GetStopwatch(),
                Ip = WebHttp.Ip,
                Host = WebHttp.Host,
                Browser = WebHttp.Browser,
                Url = WebHttp.Url
            };
        }

        /// <summary>
      
        /// </summary>
        protected string GetTraceId() {
            var traceId = Context.TraceId;
            return string.IsNullOrWhiteSpace( traceId ) ? Id.Guid() : traceId;
        }

        /// <summary>
       
        /// </summary>
        protected Stopwatch GetStopwatch() {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            return stopwatch;
        }
    }
}
