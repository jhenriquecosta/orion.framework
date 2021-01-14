using Microsoft.Extensions.Logging;

namespace Orion.Framework.Logs.Abstractions {
   
    public interface ILogProvider {
       
        string LogName { get; }
      
        bool IsDebugEnabled { get; }
      
        bool IsTraceEnabled { get; }
    
        void WriteLog( LogLevel level, ILogContent content );
    }
}
