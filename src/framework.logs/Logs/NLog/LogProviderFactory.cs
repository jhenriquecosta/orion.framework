using Orion.Framework.Logs.Abstractions;

namespace Orion.Framework.Logs.NLog {
    /// <summary>
   
    /// </summary>
    public class LogProviderFactory : ILogProviderFactory {
        /// <summary>
     
        /// </summary>
        /// <param name="logName"></param>
        /// <param name="format"></param>
        public ILogProvider Create( string logName, ILogFormat format = null ) {
            return new NLogProvider( logName, format );
        }
    }
}
