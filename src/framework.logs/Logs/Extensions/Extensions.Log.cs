using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orion.Framework.Exceptions;
using Orion.Framework.Helpers;
using Orion.Framework.Logs;
using Orion.Framework.Logs.Contents;
using Orion.Framework.Properties;

namespace Orion.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="businessId"></param>
        public static ILog BusinessId( this ILog log, string businessId ) {
            return log.Set<LogContent>( content => {
                if( string.IsNullOrWhiteSpace( content.BusinessId ) == false )
                    content.BusinessId += ",";
                content.BusinessId += businessId;
            } );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="module"></param>
        public static ILog Module( this ILog log, string module ) {
            return log.Set<LogContent>( content => content.Module = module );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="class"></param>
        public static ILog Class( this ILog log, string @class ) {
            return log.Set<LogContent>( content => content.Class = @class );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="method"></param>
        public static ILog Method( this ILog log, string method ) {
            return log.Set<LogContent>( content => content.Method = method );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="value"></param>
        public static ILog Params( this ILog log, string value ) {
            return log.Set<LogContent>( content => content.AppendLine( content.Params, value ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public static ILog Params( this ILog log, string name, string value, string type = null ) {
            return log.Set<LogContent>( content => {
                if( string.IsNullOrWhiteSpace( type ) ) {
                    content.AppendLine( content.Params, $"{LogResource.ParameterName}: {name}, {LogResource.ParameterValue}: {value}" );
                    return;
                }
                content.AppendLine( content.Params, $"{LogResource.ParameterType}: {type}, {LogResource.ParameterName}: {name}, {LogResource.ParameterValue}: {value}" );
            } );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="dictionary"></param>
        public static ILog Params( this ILog log, IDictionary<string, object> dictionary ) {
            if( dictionary == null || dictionary.Count == 0 )
                return log;
            foreach( var item in dictionary )
                Params( log, item.Key, item.Value.SafeString() );
            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="caption"></param>
        public static ILog Caption( this ILog log, string caption ) {
            return log.Set<LogContent>( content => content.Caption = caption );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="value"></param>
        public static ILog Sql( this ILog log, string value ) {
            return log.Set<LogContent>( content => content.Sql.AppendLine( value ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="value">值param>
        public static ILog SqlParams( this ILog log, string value ) {
            return log.Set<LogContent>( content => content.AppendLine( content.SqlParams, value ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="list"></param>
        public static ILog SqlParams( this ILog log, IEnumerable<KeyValuePair<string, object>> list ) {
            if( list == null )
                return log;
            var dictionary = list.ToList();
            if( dictionary.Count == 0 )
                return log;
            var result = new StringBuilder();
            foreach( var item in dictionary )
                result.AppendLine( $"   {item.Key} : {GetParamLiterals( item.Value )} : {item.Value?.GetType()}," );
            return SqlParams( log, result.ToString().RemoveEnd( $",{Common.Line}" ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private static string GetParamLiterals( object value ) {
            if( value == null )
                return "''";
            switch( value.GetType().Name.ToLower() ) {
                case "boolean":
                    return Helpers.TypeConvert.ToBool( value ) ? "1" : "0";
                case "int16":
                case "int32":
                case "int64":
                case "single":
                case "double":
                case "decimal":
                    return value.SafeString();
                default:
                    return $"'{value}'";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="exception"></param>
        /// <param name="errorCode"></param>
        public static ILog Exception( this ILog log, Exception exception, string errorCode = null ) {
            if( exception == null )
                return log;
            return log.Set<LogContent>( content => {
                content.Exception = exception;
                content.ErrorCode = errorCode;
            } );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="exception"></param>
        public static ILog Exception( this ILog log, Warning exception ) {
            if( exception == null )
                return log;
            return Exception( log, exception, exception.Code );
        }
    }
}
