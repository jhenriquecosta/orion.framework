using System;
using Orion.Framework.Properties;

namespace Orion.Framework.Exceptions {
    /// <summary>
    ///  
    /// </summary>
    public class ConcurrencyException : Warning {
        /// <summary>
        ///  
        /// </summary>
        private readonly string _message;

        /// <summary>
        ///  
        /// </summary>
        public ConcurrencyException()
            : this( "" ) {
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="message"> </param>
        public ConcurrencyException( string message )
            : this( message, null ) {
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="exception">异常</param>
        public ConcurrencyException( Exception exception )
            : this( "", exception ) {
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="message"> </param>
        /// <param name="exception"> </param>
        public ConcurrencyException( string message, Exception exception )
            : this( message, exception, "" ) {
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="message"> </param>
        /// <param name="exception">异 常</param>
        /// <param name="code">  param>
        public ConcurrencyException( string message, Exception exception, string code )
            : base( message, code, exception ) {
            _message = message;
        }

        /// <summary>
        ///  
        /// </summary>
        public override string Message => $"{LibraryResource.ConcurrencyExceptionMessage}.{_message}";
    }
}
