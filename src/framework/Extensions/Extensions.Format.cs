namespace Orion.Framework {
  
    public static partial class Extensions {
   
        public static string Description( this bool value ) {
            return value ? "1" : "0";
        }

      
        public static string Description( this bool? value ) {
            return value == null ? "" : Description( value.Value );
        }
    }
}
