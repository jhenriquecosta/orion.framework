using System;
using System.Linq;
using System.Text;
using Orion.Framework.Exceptions;
using Orion.Framework.Logs.Abstractions;
using Orion.Framework.Logs.Contents;
using Orion.Framework.Properties;

namespace Orion.Framework.Logs.Formats
{
    /// <summary>
    /// 
    /// </summary>
    public class ContentFormat : ILogFormat {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logContent"></param>
        public string Format( ILogContent logContent ) {
            if( !( logContent is LogContent content ) )
                return string.Empty;
            return Format( content );
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly ILogFormat Instance = new ContentFormat();

        /// <summary>
        /// 
        /// </summary>
        protected virtual string Format( LogContent content ) {
            int line = 1;
            var result = new StringBuilder();
            Line1( result, content, ref line );
            Line2( result, content, ref line );
            Line3( result, content, ref line );
            Line4( result, content, ref line );
            Line5( result, content, ref line );
            Line6( result, content, ref line );
            Line7( result, content, ref line );
            Line8( result, content, ref line );
            Line9( result, content, ref line );
            Line10( result, content, ref line );
            Line11( result, content, ref line );
            Line12( result, content, ref line );
            Line13( result, content, ref line );
            Line14( result, content, ref line );
            Finish( result );
            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void AppendLine( StringBuilder result, LogContent content, Action<StringBuilder, LogContent> action, ref int line ) {
            Append( result, content, action, ref line );
            result.AppendLine();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Append( StringBuilder result, LogContent content, Action<StringBuilder, LogContent> action, ref int line ) {
            result.AppendFormat( "{0}. ", line++ );
            action( result, content );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Append( StringBuilder result, string caption, string value ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return;
            result.AppendFormat( "{0}: {1}   ", caption, value );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line1( StringBuilder result, LogContent content, ref int line ) {
            AppendLine( result, content, ( r, c ) => {
                r.AppendFormat( "{0}: {1} >> ", c.Level, c.LogName );
                r.AppendFormat( "{0}: {1}   ", LogResource.TraceId, c.TraceId );
                r.AppendFormat( "{0}: {1}   ", LogResource.OperationTime, c.OperationTime );
                if( string.IsNullOrWhiteSpace( c.Duration ) )
                    return;
                r.AppendFormat( "{0}: {1} ", LogResource.Duration, c.Duration );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line2( StringBuilder result, LogContent content, ref int line ) {
            AppendLine( result, content, ( r, c ) => {
                Append( r, "Ip", c.Ip );
                Append( r, LogResource.Host, c.Host );
                Append( r, LogResource.ThreadId, c.ThreadId );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line3( StringBuilder result, LogContent content, ref int line ) {
            if( string.IsNullOrWhiteSpace( content.Browser ) )
                return;
            AppendLine( result, content, ( r, c ) => Append( r, LogResource.Browser, c.Browser ), ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line4( StringBuilder result, LogContent content, ref int line ) {
            if( string.IsNullOrWhiteSpace( content.Url ) )
                return;
            AppendLine( result, content, ( r, c ) => r.Append( "Url: " + c.Url ), ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line5( StringBuilder result, LogContent content, ref int line ) {
            if( string.IsNullOrWhiteSpace( content.UserId ) && string.IsNullOrWhiteSpace( content.Operator )
                && string.IsNullOrWhiteSpace( content.Role ) )
                return;
            AppendLine( result, content, ( r, c ) => {
                Append( r, LogResource.UserId, c.UserId );
                Append( r, LogResource.Operator, c.Operator );
                Append( r, LogResource.Role, c.Role );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line6( StringBuilder result, LogContent content, ref int line ) {
            if( string.IsNullOrWhiteSpace( content.BusinessId ) && string.IsNullOrWhiteSpace( content.Tenant )
                 && string.IsNullOrWhiteSpace( content.Application ) && string.IsNullOrWhiteSpace( content.Module ) )
                return;
            AppendLine( result, content, ( r, c ) => {
                Append( r, LogResource.BusinessId, c.BusinessId );
                Append( r, LogResource.Tenant, c.Tenant );
                Append( r, LogResource.Application, c.Application );
                Append( r, LogResource.Module, c.Module );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line7( StringBuilder result, LogContent content, ref int line ) {
            if( string.IsNullOrWhiteSpace( content.Class ) && string.IsNullOrWhiteSpace( content.Method ) )
                return;
            AppendLine( result, content, ( r, c ) => {
                Append( r, LogResource.Class, c.Class );
                Append( r, LogResource.Method, c.Method );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line8( StringBuilder result, LogContent content, ref int line ) {
            if( content.Params.Length == 0 )
                return;
            Append( result, content, ( r, c ) => {
                r.AppendLine( $"{LogResource.Params}:" );
                r.Append( c.Params );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line9( StringBuilder result, LogContent content, ref int line ) {
            if( string.IsNullOrWhiteSpace( content.Caption ) )
                return;
            AppendLine( result, content, ( r, c ) => {
                r.AppendFormat( "{0}: {1}", LogResource.Caption, c.Caption );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line10( StringBuilder result, LogContent content, ref int line ) {
            if( content.Content.Length == 0 )
                return;
            Append( result, content, ( r, c ) => {
                r.AppendLine( $"{LogResource.Content}:" );
                r.Append( c.Content );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line11( StringBuilder result, LogContent content, ref int line ) {
            if( content.Sql.Length == 0 )
                return;
            Append( result, content, ( r, c ) => {
                r.AppendLine( $"{LogResource.Sql}:" );
                r.Append( c.Sql );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line12( StringBuilder result, LogContent content, ref int line ) {
            if( content.SqlParams.Length == 0 )
                return;
            Append( result, content, ( r, c ) => {
                r.AppendLine( $"{LogResource.SqlParams}:" );
                r.Append( c.SqlParams );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line13( StringBuilder result, LogContent content, ref int line ) {
            if( content.Exception == null )
                return;
            AppendLine( result, content, ( r, c ) => {
                r.AppendLine( $"{LogResource.Exception}: {GetExceptionTypes( c.Exception )} { GetErrorCode( c.ErrorCode ) }" );
                r.Append( $"   { Warning.GetMessage( c.Exception ) }" );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetExceptionTypes( Exception exception ) {
            return Warning.GetExceptions( exception ).Select( t => t.GetType() ).Join();
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetErrorCode( string errorCode ) {
            if( string.IsNullOrWhiteSpace( errorCode ) )
                return string.Empty;
            return $"-- {LogResource.ErrorCode}: {errorCode}";
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Line14( StringBuilder result, LogContent content, ref int line ) {
            if( content.Exception == null )
                return;
            AppendLine( result, content, ( r, c ) => {
                r.AppendLine( $"{LogResource.StackTrace}:" );
                r.Append( c.Exception.StackTrace );
            }, ref line );
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Finish( StringBuilder result ) {
            for( int i = 0; i < 125; i++ )
                result.Append( "-" );
        }
    }
}
