using System.IO;

namespace Orion.Framework.Helpers {
    /// <summary>
    /// 
    /// </summary>
    public static class Url {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urls"></param>
        public static string Combine( params string[] urls ) {
            return Path.Combine( urls ).Replace( @"\", "/" );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        public static string Join( string url, string param ) {
            return $"{GetUrl( url )}{param}";
        }

        /// <summary>
        /// 
        /// </summary>
        private static string GetUrl( string url ) {
            if( !url.Contains( "?" ) )
                return $"{url}?";
            if( url.EndsWith( "?" ) )
                return url;
            if( url.EndsWith( "&" ) )
                return url;
            return $"{url}&";
        }
    }
}
