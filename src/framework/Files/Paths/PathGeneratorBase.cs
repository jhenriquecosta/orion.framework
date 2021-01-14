using System;
using System.IO;
using Orion.Framework.Helpers;
using Orion.Framework.Randoms;

namespace Orion.Framework.Files.Paths {
    /// <summary>
    /// 
    /// </summary>
    public abstract class PathGeneratorBase : IPathGenerator {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRandomGenerator _randomGenerator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="randomGenerator"></param>
        protected PathGeneratorBase( IRandomGenerator randomGenerator ) {
            _randomGenerator = randomGenerator ?? GuidRandomGenerator.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public string Generate( string fileName ) {
            if( string.IsNullOrWhiteSpace( fileName ) )
                throw new ArgumentNullException( nameof( fileName ) );
            return GeneratePath( GetFileName( fileName ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        protected abstract string GeneratePath( string fileName );

        /// <summary>
        /// 
        /// </summary>
        private string GetFileName( string fileName ) {
            var name = Path.GetFileNameWithoutExtension( fileName );
            var extension = Path.GetExtension( fileName )?.TrimStart( '.' );
            if( string.IsNullOrWhiteSpace( extension ) ) {
                extension = fileName;
                name = string.Empty;
            }
            if( string.IsNullOrWhiteSpace( name ) )
                name = _randomGenerator.Generate();
            name = FilterFileName( name );
            return $"{name}-{Time.GetDateTime():HHmmss}.{extension}";
        }

        /// <summary>
        /// 
        /// </summary>
        private static string FilterFileName( string fileName ) {
           return fileName = Regex.Replace( fileName, "\\W", "" );
         // return Orion.Framework.Helpers.String.PinYin( fileName );
        }
    }
}
