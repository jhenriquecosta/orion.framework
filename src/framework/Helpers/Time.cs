using System;

namespace Orion.Framework.Helpers {
    /// <summary>
    /// 
    /// </summary>
    public static class Time {
        /// <summary>
        /// 
        /// </summary>
        private static DateTime? _dateTime;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        public static void SetTime( DateTime? dateTime ) {
            _dateTime = dateTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        public static void SetTime( string dateTime ) {
            _dateTime = Orion.Framework.Helpers.TypeConvert.ToDateOrNull( dateTime );
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Reset() {
            _dateTime = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static DateTime GetDateTime() {
            if( _dateTime == null )
                return DateTime.Now;
            return _dateTime.Value;
        }

        /// <summary>
        /// 获取当前日期,
        /// </summary>
        public static DateTime GetDate() {
            return GetDateTime().Date;
        }

        /// <summary>
        /// 
        /// </summary>
        public static long GetUnixTimestamp() {
            return GetUnixTimestamp( DateTime.Now );
        }

        
        public static long GetUnixTimestamp( DateTime time ) {
            var start = TimeZoneInfo.ConvertTime( new DateTime( 1970, 1, 1 ), TimeZoneInfo.Local );
            long ticks = ( time - start.Add( new TimeSpan( 8, 0, 0 ) ) ).Ticks;
            return Orion.Framework.Helpers.TypeConvert.ToLong( ticks / TimeSpan.TicksPerSecond );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        public static DateTime GetTimeFromUnixTimestamp( long timestamp ) {
            var start = TimeZoneInfo.ConvertTime( new DateTime( 1970, 1, 1 ), TimeZoneInfo.Local );
            TimeSpan span = new TimeSpan( long.Parse( timestamp + "0000000" ) );
            return start.Add( span ).Add( new TimeSpan( 8, 0, 0 ) );
        }
    }
}