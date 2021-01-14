using System;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Framework {
   
    public static partial class Extensions {
      
        public static void CheckNull( this object obj, string parameterName ) {
            if( obj == null )
                throw new ArgumentNullException( parameterName );
        }

       
        public static bool IsEmpty( this string value ) {
            return string.IsNullOrWhiteSpace( value );
        }

       
        public static bool IsEmpty( this Guid value ) {
            return value == Guid.Empty;
        }

   
        public static bool IsEmpty( this Guid? value ) {
            if ( value == null )
                return true;
            return value == Guid.Empty;
        }

      
        public static bool IsEmpty<T>( this IEnumerable<T> value ) {
            if ( value == null )
                return true;
            return !value.Any();
        }
    }
}
