namespace Orion.Framework.Logs.Abstractions {
    /// <summary>
    
    /// </summary>
    public interface ILogFormat {
        /// <summary>
      
        /// </summary>
        /// <param name="content"></param>
        string Format( ILogContent content );
    }
}
