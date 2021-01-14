using System;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Framework {

    public static partial class Extensions {
     
        public static string SafeString( this object input ) {
            return input?.ToString().Trim() ?? string.Empty;
        }

       
        public static bool ToBool( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToBool( obj );
        }

  
        public static bool? ToBoolOrNull( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToBoolOrNull( obj );
        }

      
        public static int ToInt( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToInt( obj );
        }

    
        public static int? ToIntOrNull( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToIntOrNull( obj );
        }

     
        public static long ToLong( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToLong( obj );
        }

  
        public static long? ToLongOrNull( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToLongOrNull( obj );
        }

    
        public static double ToDouble( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToDouble( obj );
        }

      
        public static double? ToDoubleOrNull( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToDoubleOrNull( obj );
        }

     
        public static decimal ToDecimal( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToDecimal( obj );
        }

     
        public static decimal? ToDecimalOrNull( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToDecimalOrNull( obj );
        }

       
        public static DateTime ToDate( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToDate( obj );
        }

     
        public static DateTime? ToDateOrNull( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToDateOrNull( obj );
        }

      
        public static Guid ToGuid( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToGuid( obj );
        }

      
        public static Guid? ToGuidOrNull( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToGuidOrNull( obj );
        }

        public static List<int> ToIntList(this string obj)
        {
            return Orion.Framework.Helpers.TypeConvert.ToIntList(obj);
        }
        public static List<int> ToIntList(this IList<string> obj)
        {
            if (obj == null)
                return new List<int>();
            return obj.Select(t => t.ToInt()).ToList();
        }

        public static List<Guid> ToGuidList( this string obj ) {
            return Orion.Framework.Helpers.TypeConvert.ToGuidList( obj );
        }

     
        public static List<Guid> ToGuidList( this IList<string> obj ) {
            if( obj == null )
                return new List<Guid>();
            return obj.Select( t => t.ToGuid() ).ToList();
        }
        public static T To<T>(this object obj)
        {
            return Orion.Framework.Helpers.TypeConvert.To<T>(obj);
        }
    }
}
