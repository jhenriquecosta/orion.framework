namespace Orion.Framework.Logs.Abstractions {
    /// <summary>
   
    /// </summary>
    public interface ILogProviderFactory {
        /// <summary>
     
        /// </summary>
        /// <param name="logName"></param>
        /// <param name="format"></param>
        ILogProvider Create( string logName, ILogFormat format = null );
    }
}
