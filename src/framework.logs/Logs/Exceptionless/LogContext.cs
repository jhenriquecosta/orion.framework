using System;
using Orion.Framework.Helpers;
using Orion.Framework.Logs.Internal;

namespace Orion.Framework.Logs.Exceptionless
{
    /// <summary>

    /// </summary>
    public class LogContext : Orion.Framework.Logs.Core.LogContext {
        /// <summary>
      
        /// </summary>
        protected override LogContextInfo CreateInfo() {
            return new LogContextInfo {
                TraceId = Guid.NewGuid().ToString(),
                Stopwatch = GetStopwatch(),
                Url = WebHttp.Url
            };
        }
    }
}
