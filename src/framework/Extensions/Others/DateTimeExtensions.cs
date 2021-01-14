using System;
using System.Globalization;

namespace Orion.Framework
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Pass in the GMT difference. Defaults to +1, whith is Nigerian time
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hourDifference">The -ve or +ve difference between GMT and your location. Eg. Nogeria is GMT+1</param>
        /// <returns></returns>
		public static DateTime GetLocalTime(this DateTime date, int hourDifference = 1)
        {
            return date.ToUniversalTime().AddHours(hourDifference);
        }

        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static string FormatDate(this DateTime? date, string format = "dd MMM yyyy")
        {
            return date.Value.ToString(format, CultureInfo.CreateSpecificCulture("pt-BR"));
        }
        public static string FormatDate(this DateTime date, string format = "dd MMM yyyy")
        {
            return date.ToString(format, CultureInfo.CreateSpecificCulture("pt-BR"));
        }
        public static bool IsBefore(this DateTime date, DateTime dateToCompare)
        {
            return date.Date < dateToCompare.Date;
        }

        public static bool IsOnTheSameDateAs(this DateTime date, DateTime dateToCompare)
        {
            return date.Date == dateToCompare.Date;
        }

        public static bool IsAfter(this DateTime date, DateTime dateToCompare)
        {
            return date.Date > dateToCompare.Date;
        }
        public static DateTime ToDate(this DateTime? date)
        {
            return date.Value.ToUniversalTime();
        }
        public static DateTime ToDate(this DateTime date)
        {
            return date.ToUniversalTime();
        }
        public static bool GreaterThanOrEqual(this DateTime? date,DateTime? other)
        {
            var date1 = date.ToDate();
            var date2 = other.ToDate();
            return date1 >= date2;
            
        }
        public static bool LessThanOrEqual(this DateTime? date, DateTime? other)
        {
            var date1 = date.ToDate();
            var date2 = other.ToDate();
            return date1 <= date2;
        }
    }
}
