namespace Orion.Framework.Files {
    /// <summary>
    /// 
    /// </summary>
    public class FileInfo {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="size"></param>
        /// <param name="fileName"></param>
        /// <param name="id"></param>
        public FileInfo( string path, long? size, string fileName = null, string id = null ) {
            Path = path;
            Size = new FileSize( size.SafeValue() );
            Extension = System.IO.Path.GetExtension( path )?.TrimStart( '.' );
            FileName = string.IsNullOrWhiteSpace( fileName ) ? System.IO.Path.GetFileName( path ) : fileName;
            Id = id;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// 
        /// </summary>
        public string Path { get; }
        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; }
        /// <summary>
        /// 
        /// </summary>
        public string Extension { get; }
        /// <summary>
        /// 
        /// </summary>
        public FileSize Size { get; }
    }
}
