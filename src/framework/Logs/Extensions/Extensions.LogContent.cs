using System.Text;
using Orion.Framework.Logs.Abstractions;

namespace Orion.Framework {
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions {
        /// <summary>
        /// 
        /// </summary>
        public static void Append( this ILogContent content, StringBuilder result, string value ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return;
            result.Append( value );
        }

        /// <summary>
        /// 
        /// </summary>
        public static void AppendLine( this ILogContent content, StringBuilder result, string value ) {
            content.Append( result, value );
            result.AppendLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="value"></param>
        public static void Content( this ILogContent content, string value ) {
            content.AppendLine( content.Content, value );
        }
    }
}
