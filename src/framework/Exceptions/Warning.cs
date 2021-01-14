using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Orion.Framework.Properties;

namespace Orion.Framework.Exceptions {
    /// <summary>
    /// 
    /// </summary>
    public class Warning : Exception {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public Warning( string message)
            : this( message, null ) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public Warning( Exception exception )
            : this( null, null, exception ) {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public Warning( string message, string code )
            : this( message, code, null ) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="exception"></param>
        public Warning( string message, string code, Exception exception )
            : base( message ?? "", exception ) {
            Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GetMessage() {
            return GetMessage( this );
        }

        /// <summary>
        /// 
        /// </summary>
        public static string GetMessage( Exception ex ) {
            var result = new StringBuilder();
            var list = GetExceptions( ex );
            foreach( var exception in list )
                AppendMessage( result, exception );
            return result.ToString().RemoveEnd( Environment.NewLine );
        }

        /// <summary>
        /// 
        /// </summary>
        private static void AppendMessage( StringBuilder result, Exception exception ) {
            if( exception == null )
                return;
            result.AppendLine( exception.Message );
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<Exception> GetExceptions() {
            return GetExceptions( this );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static IList<Exception> GetExceptions( Exception ex ) {
            var result = new List<Exception>();
            AddException( result, ex );
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private static void AddException( List<Exception> result, Exception exception ) {
            if( exception == null )
                return;
            result.Add( exception );
            AddException( result, exception.InnerException );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public string GetPrompt( LogLevel level ) {
            if( level == LogLevel.Error )
                return R.SystemError;
            return Message;
        }
    }
}
