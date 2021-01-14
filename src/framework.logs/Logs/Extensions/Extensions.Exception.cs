using System;
using Orion.Framework.Exceptions;
using Orion.Framework.Logs;

namespace Orion.Framework 
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="log"></param>
        public static void Log( this Exception exception, ILog log ) {
            exception = exception.GetRawException();
            if( exception is Warning warning ) {
                log.Exception( exception, warning.Code ).Warn();
                return;
            }
            log.Exception( exception ).Error();
        }
    }
}
