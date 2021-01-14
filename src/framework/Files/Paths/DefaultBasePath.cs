namespace Orion.Framework.Files.Paths {
    /// <summary>
    /// 
    /// </summary>
    public class DefaultBasePath : IBasePath {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public DefaultBasePath( string path ) {
            _path = path;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetPath() {
            return _path;
        }
    }
}
