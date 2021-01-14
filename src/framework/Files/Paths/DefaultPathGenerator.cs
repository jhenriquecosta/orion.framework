using Orion.Framework.Randoms;

namespace Orion.Framework.Files.Paths {
    /// <summary>
    /// 
    /// </summary>
    public class DefaultPathGenerator : PathGeneratorBase {
        /// <summary>
        /// 
        /// </summary>
        private readonly IBasePath _basePath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="randomGenerator"></param>
        public DefaultPathGenerator( IBasePath basePath, IRandomGenerator randomGenerator = null ) : base( randomGenerator ) {
            _basePath = basePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        protected override string GeneratePath( string fileName ) {
            return $"{_basePath.GetPath()}/{fileName}";
        }
    }
}
