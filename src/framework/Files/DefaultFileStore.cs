using System.IO;
using System.Threading.Tasks;
using Orion.Framework.Exceptions;
using Orion.Framework.Files.Paths;
using Orion.Framework.Helpers;

namespace Orion.Framework.Files {
    /// <summary>
    /// 
    /// </summary>
    public class DefaultFileStore : IFileStore {
        /// <summary>
        /// 
        /// </summary>
        private readonly IPathGenerator _generator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathGenerator"></param>
        public DefaultFileStore( IPathGenerator pathGenerator ) {
            _generator = pathGenerator;
        }

        /// <summary>
        ///
        /// </summary>
        public async Task<string> SaveAsync() {
            var fileControl = WebHttp.GetFile();
            var path = _generator.Generate( fileControl.FileName );
            var physicalPath = WebHttp.GetWebRootPath( path );
            var directory = Path.GetDirectoryName( physicalPath );
            if( string.IsNullOrEmpty( directory ) )
                throw new Warning( "Erro" );
            if( Directory.Exists( directory ) == false )
                Directory.CreateDirectory( directory );
            using( var stream = new FileStream( physicalPath, FileMode.Create ) ) {
                await fileControl.CopyToAsync( stream );
            }
            return path;
        }
    }
}
