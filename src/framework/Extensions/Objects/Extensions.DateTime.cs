using System;
using System.Text;

namespace Orion.Framework {
    
    public static partial class Extensions {

        /// <summary>
        /// Gets the age based on Birthday.
        /// </summary>
        /// <param name="birthday">The birth date.</param>
        /// <returns>A integer for age.</returns>
        public static int GetAge(this DateTime birthday)
        {
            var today = DateTime.Today;

            var age = today.Year - birthday.Year;
            if (today.Month < birthday.Month)
            {
                age = age - 1;
            }
            else if (today.Month == birthday.Month)
            {
                if (today.Day < birthday.Day)
                {
                    age = age - 1;
                }
            }

            return age;
        }

        
        public static string ToDateTimeString( this DateTime dateTime, bool removeSecond = false ) {
            if( removeSecond )
                return dateTime.ToString( "yyyy-MM-dd HH:mm" );
            return dateTime.ToString( "yyyy-MM-dd HH:mm:ss" );
        }

         
        public static string ToDateTimeString( this DateTime? dateTime, bool removeSecond = false ) {
            if( dateTime == null )
                return string.Empty;
            return ToDateTimeString( dateTime.Value, removeSecond );
        }

        
        public static string ToDateString( this DateTime dateTime ) {
            return dateTime.ToString( "dd-MM-yyyy" );
        }

        
        public static string ToDateString( this DateTime? dateTime ) {
            if( dateTime == null )
                return string.Empty;
            return ToDateString( dateTime.Value );
        }
 
        public static string ToTimeString( this DateTime dateTime ) {
            return dateTime.ToString( "HH:mm:ss" );
        }

       
        public static string ToTimeString( this DateTime? dateTime ) {
            if( dateTime == null )
                return string.Empty;
            return ToTimeString( dateTime.Value );
        }

       
        public static string ToMillisecondString( this DateTime dateTime ) {
            return dateTime.ToString( "yyyy-MM-dd HH:mm:ss.fff" );
        }

       
        public static string ToMillisecondString( this DateTime? dateTime ) {
            if( dateTime == null )
                return string.Empty;
            return ToMillisecondString( dateTime.Value );
        }

        
        
        
       

        
        

       
        
       
        public static string Description( this TimeSpan span ) {
            StringBuilder result = new StringBuilder();
            if( span.Days > 0 )
                result.AppendFormat( "{0}", span.Days );
            if( span.Hours > 0 )
                result.AppendFormat( "{0}", span.Hours );
            if( span.Minutes > 0 )
                result.AppendFormat( "{0}", span.Minutes );
            if( span.Seconds > 0 )
                result.AppendFormat( "{0}", span.Seconds );
            if( span.Milliseconds > 0 )
                result.AppendFormat( "{0}", span.Milliseconds );
            if ( result.Length > 0 )
                return result.ToString();
            return $"{span.TotalSeconds * 1000}";
        }
    }
}
