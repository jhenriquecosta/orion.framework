using System.Collections.Generic;
using System.Linq;
using AspectCore.DynamicProxy.Parameters;
using Orion.Framework.Helpers;

namespace Orion.Framework.Logs.Extensions {
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="log"></param>
        public static void AppendTo( this Parameter parameter, ILog log ) {
            log.Params( parameter.Name, GetParameterValue( parameter ), parameter.ParameterInfo.ParameterType.FullName );
        }

        /// <summary>
        /// 
        /// </summary>
        private static string GetParameterValue( Parameter parameter ) {
            if( Reflection.IsGenericCollection( parameter.RawType ) == false )
                return parameter.Value.SafeString();
            if ( !( parameter.Value is IEnumerable<object> list ) )
                return parameter.Value.SafeString();
            return list.Select( t => t.SafeString() ).Join();
        }
    }
}
