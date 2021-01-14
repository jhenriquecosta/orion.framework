using System.Collections.Generic;
using System.Linq;
using Orion.Framework.Logs;
using Orion.Framework.Logs.Abstractions;

namespace Orion.Framework {
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public static ILog Content( this ILog log ) {
            return log.Set<ILogContent>( content => content.Content( "" ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="value"></param>
        public static ILog Content( this ILog log, string value ) {
            return log.Set<ILogContent>( content => content.Content( value ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="dictionary"></param>
        public static ILog Content( this ILog log, IDictionary<string, object> dictionary ) {
            if( dictionary == null )
                return log;
            return Content( log, dictionary.ToDictionary( t => t.Key, t => t.Value.SafeString() ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="dictionary"></param>
        public static ILog Content( this ILog log, IDictionary<string, string> dictionary ) {
            if( dictionary == null )
                return log;
            foreach( var keyValue in dictionary )
                log.Set<ILogContent>( content => content.Content( $"{keyValue.Key} : {keyValue.Value}" ) );
            return log;
        }
    }
}
