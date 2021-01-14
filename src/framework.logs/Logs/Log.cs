using Orion.Framework.Helpers;
using Orion.Framework.Logs.Abstractions;
using Orion.Framework.Logs.Core;
using Orion.Framework.Logs.Exceptionless;
using Orion.Framework.Sessions;

namespace Orion.Framework.Logs
{

    public class Log : LogBase<LogContent> {
     
        private readonly string _class;
       
        public static readonly ILog Null = NullLog.Instance;

      
        public Log( ILogProviderFactory providerFactory, ILogContext context, ILogFormat format, ISession session ) : base( providerFactory.Create( "", format ), context, session ) {
        }

      
        private Log( ILogProvider provider, ILogContext context, ISession session, string @class ) : base( provider, context, session ) {
            _class = @class;
        }

       
        protected override LogContent GetContent() {
            return new LogContent { Class = _class };
        }

      
        protected override void Init( LogContent content ) {
            base.Init( content );
            content.Tenant = Session.GetTenantName();
            content.Application = Session.GetApplicationName();
            content.Operator = Session.GetFullName();
            content.Role = Session.GetRoleName();
        }

    
        public static ILog GetLog() {
            return GetLog( string.Empty );
        }

        
        public static ILog GetLog( object instance ) {
            if( instance == null )
                return GetLog();
            var className = instance.GetType().ToString();
            return GetLog( className, className );
        }

       
        public static ILog GetLog( string logName ) {
            return GetLog( logName, string.Empty );
        }

      
        private static ILog GetLog( string logName, string @class ) {
            var providerFactory = GetLogProviderFactory();
            var format = GetLogFormat();
            var context = GetLogContext();
            var session = GetSession();
            return new Log( providerFactory.Create( logName, format ), context, session, @class );
        }

     
        private static ILogProviderFactory GetLogProviderFactory()
        {
            try 
            {
                return Ioc.Create<ILogProviderFactory>();
            }
            catch {
                return new NLog.LogProviderFactory();
            }
        }

      
        private static ILogFormat GetLogFormat() {
            try {
                return Ioc.Create<ILogFormat>();
            }
            catch {
                return Orion.Framework.Logs.Formats.ContentFormat.Instance;
            }
        }

      
        private static ILogContext GetLogContext() {
            try {
                return Ioc.Create<ILogContext>();
            }
            catch {
                return NullLogContext.Instance;
            }
        }

        
        private static ISession GetSession() {
            try {
                return Ioc.Create<ISession>();
            }
            catch {
                return Sessions.Session.Instance;
            }
        }
    }
}
