using System;
using System.Runtime.ExceptionServices;
using Orion.Framework.Exceptions.Prompts;

namespace Orion.Framework {
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions 
    {

        /// <summary>
        /// Uses <see cref="ExceptionDispatchInfo.Capture"/> method to re-throws exception
        /// while preserving stack trace.
        /// </summary>
        /// <param name="exception">Exception to be re-thrown</param>
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public static Exception GetRawException( this Exception exception ) {
            if( exception == null )
                return null;
            if( exception is AspectCore.DynamicProxy.AspectInvocationException aspectInvocationException ) {
                if( aspectInvocationException.InnerException == null )
                    return aspectInvocationException;
                return GetRawException( aspectInvocationException.InnerException );
            }
            return exception;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public static string GetPrompt( this Exception exception ) {
            return ExceptionPrompt.GetPrompt( exception );
        }
    }
}
