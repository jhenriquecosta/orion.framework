using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Helpers {
    /// <summary>
    /// 
    /// </summary>
    public partial class String {

      
        public static string Join<T>( IEnumerable<T> list, string quotes = "", string separator = "," ) {
            if( list == null )
                return string.Empty;
            var result = new StringBuilder();
            foreach( var each in list )
                result.AppendFormat( "{0}{1}{0}{2}", quotes, each, separator );
            if( separator == "" )
                return result.ToString();
            return result.ToString().TrimEnd( separator.ToCharArray() );
        }

      

   
        public static string FirstLowerCase( string value ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return string.Empty;
            return $"{value.Substring( 0, 1 ).ToLower()}{value.Substring( 1 )}";
        }

  

    
        public static string FirstUpperCase( string value ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return string.Empty;
            return $"{value.Substring( 0, 1 ).ToUpper()}{value.Substring( 1 )}";
        }

      
        public static string RemoveEnd( string value, string removeValue ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return string.Empty;
            if( string.IsNullOrWhiteSpace( removeValue ) )
                return value.SafeString();
            if( value.ToLower().EndsWith( removeValue.ToLower() ) )
                return value.Remove( value.Length - removeValue.Length, removeValue.Length );
            return value;
        }

     

        public static string SplitWordGroup( string value, char separator = '-' ) {
            var pattern = @"([A-Z])(?=[a-z])|(?<=[a-z])([A-Z]|[0-9]+)";
            return string.IsNullOrWhiteSpace( value ) ? string.Empty : System.Text.RegularExpressions.Regex.Replace( value, pattern, $"{separator}$1$2" ).TrimStart( separator ).ToLower();
        }

        
    }
}
