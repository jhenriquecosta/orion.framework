using System;
using Orion.Framework.Logs.Abstractions;

namespace Orion.Framework.Logs.Formats {
    /// <summary>
    /// 
    /// </summary>
    public class FormatProvider : IFormatProvider, ICustomFormatter {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogFormat _format;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        public FormatProvider( ILogFormat format ) {
            _format = format ?? throw new ArgumentNullException( nameof( format ) );
        }

        /// <summary>
        /// 
        /// </summary>
        public string Format( string format, object arg, IFormatProvider formatProvider ) {
            if( !( arg is ILogContent content ) )
                return string.Empty;
            return _format.Format( content );
        }

        /// <summary>
        /// 
        /// </summary>
        public object GetFormat( Type formatType ) {
            return formatType == typeof( ICustomFormatter ) ? this : null;
        }
    }
}
