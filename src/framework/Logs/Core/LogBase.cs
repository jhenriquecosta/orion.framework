using System;
using Microsoft.Extensions.Logging;
using Orion.Framework.Logs.Abstractions;
using Orion.Framework.Sessions;
using HelperEnum = Orion.Framework.Helpers.HelperEnum;

namespace Orion.Framework.Logs.Core
{

    public abstract class LogBase<TContent> : ILog where TContent : class, ILogContent {
      
        private TContent _content;
        
        private TContent LogContent => _content ?? ( _content = GetContent() );

    
        protected LogBase( ILogProvider provider, ILogContext context, ISession session ) {
            Provider = provider;
            Context = context;
            Session = session ?? NullSession.Instance;
        }

       
        public ILogProvider Provider { get; }

      
        public ILogContext Context { get; }

       
        public ISession Session { get; set; }

       
        protected abstract TContent GetContent();

      
        public ILog Set<T>( Action<T> action ) where T : ILogContent {
            if( action == null )
                throw new ArgumentNullException( nameof( action ) );
            ILogContent content = LogContent;
            action( (T)content );
            return this;
        }

        protected virtual void Init( TContent content ) {
            content.LogName = Provider.LogName;
            content.TraceId = Context.TraceId;
            content.OperationTime = DateTime.Now.ToMillisecondString();
            content.Duration = Context.Stopwatch.Elapsed.Description();
            content.Ip = Context.Ip;
            content.Host = Context.Host;
            content.ThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            content.Browser = Context.Browser;
            content.Url = Context.Url;
            content.UserId = Session.UserId;
        }

      
        public bool IsDebugEnabled => Provider.IsDebugEnabled;

      
        public bool IsTraceEnabled => Provider.IsTraceEnabled;

       
        public virtual void Trace() {
            _content = LogContent;
            Execute( LogLevel.Trace, ref _content );
        }

       
        private void Execute( LogLevel level, ref TContent content ) {
            if ( content == null )
                return;
            if ( Enabled( level ) == false )
                return;
            try {
                content.Level = HelperEnum.GetName<LogLevel>( level );
                Init( content );
                Provider.WriteLog( level, content );
            }
            finally {
                content = null;
            }
        }

      
       
        private bool Enabled( LogLevel level ) {
            if ( level > LogLevel.Debug )
                return true;
            return IsDebugEnabled || IsTraceEnabled && level == LogLevel.Trace;
        }

     
        public virtual void Trace( string message ) {
            LogContent.Content( message );
            Trace();
        }

      
        public virtual void Debug() {
            _content = LogContent;
            Execute( LogLevel.Debug, ref _content );
        }

     
        public virtual void Debug( string message ) {
            LogContent.Content( message );
            Debug();
        }

      
        public virtual void Info() {
            _content = LogContent;
            Execute( LogLevel.Information, ref _content );
        }

       
        public virtual void Info( string message ) {
            LogContent.Content( message );
            Info();
        }

       
        public virtual void Warn() {
            _content = LogContent;
            Execute( LogLevel.Warning, ref _content );
        }

      
        public virtual void Warn( string message ) {
            LogContent.Content( message );
            Warn();
        }

      
        public virtual void Error() {
            _content = LogContent;
            Execute( LogLevel.Error, ref _content );
        }

     
        public virtual void Error( string message ) {
            LogContent.Content( message );
            Error();
        }

       
        public virtual void Fatal() {
            _content = LogContent;
            Execute( LogLevel.Critical, ref _content );
        }

      
        public virtual void Fatal( string message ) {
            LogContent.Content( message );
            Fatal();
        }
    }
}
