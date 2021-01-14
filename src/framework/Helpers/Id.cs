using System;

namespace Orion.Framework.Helpers {

    public static class Id {
      
        private static string _id;


        public static void SetId( string id ) {
            _id = id;
        }


        public static void Reset() {
            _id = null;
        }

 
        public static string ObjectId() {
            return string.IsNullOrWhiteSpace( _id ) ? Orion.Framework.Helpers.Internal.ObjectId.GenerateNewStringId() : _id;
        }

      
        public static string Guid() {
            return string.IsNullOrWhiteSpace( _id ) ? System.Guid.NewGuid().ToString( "N" ) : _id;
        }

   
        public static Guid GetGuid() {
            return string.IsNullOrWhiteSpace( _id ) ? System.Guid.NewGuid() : _id.ToGuid();
        }
    }
}
