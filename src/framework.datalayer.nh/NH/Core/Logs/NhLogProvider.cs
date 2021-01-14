using Microsoft.Extensions.Logging;

namespace Zeus.DataLayer.NHibernate.Logs {
    
    public class NhLogProvider : ILoggerProvider {
       
        public ILogger CreateLogger( string category ) {
            return category.StartsWith( "NHibernate" ) ? new NhLog() : NullLogger.Instance;
        }

       
        public void Dispose() {
        }
    }
}
