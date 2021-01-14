using Orion.Framework;

namespace Orion.Framework.Files 
{
    /// <summary>
    /// 
    /// </summary>
    public struct FileSize {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="unit"></param>
        public FileSize( long size, FileSizeUnit unit = FileSizeUnit.Byte ) {
            _size = GetSize( size, unit );
        }

        /// <summary>
        /// 
        /// </summary>
        private static long GetSize( long size, FileSizeUnit unit ) {
            switch( unit ) {
                case FileSizeUnit.K:
                    return size * 1024;
                case FileSizeUnit.M:
                    return size * 1024 * 1024;
                case FileSizeUnit.G:
                    return size * 1024 * 1024 * 1024;
                default:
                    return size;
            }
        }

        private readonly long _size;
        /// <summary>
        /// 
        /// </summary>
        public long Size => _size;

        /// <summary>
        /// 
        /// </summary>
        public int GetSize() {
            return (int)Size;
        }

        /// <summary>
        ///
        /// </summary>
        public double GetSizeByK() {
            return Orion.Framework.Helpers.TypeConvert.ToDouble( _size / 1024.0, 2 );
        }

        /// <summary>
        /// 
        /// </summary>
        public double GetSizeByM() {
            return Orion.Framework.Helpers.TypeConvert.ToDouble( _size / 1024.0 / 1024.0, 2 );
        }

        /// <summary>
        /// 
        /// </summary>
        public double GetSizeByG() {
            return Orion.Framework.Helpers.TypeConvert.ToDouble( _size / 1024.0 / 1024.0 / 1024.0, 2 );
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ToString() {
            if( _size >= 1024 * 1024 * 1024 )
                return $"{GetSizeByG()} {FileSizeUnit.G.Description()}";
            if( _size >= 1024 * 1024 )
                return $"{GetSizeByM()} {FileSizeUnit.M.Description()}";
            if( _size >= 1024 )
                return $"{GetSizeByK()} {FileSizeUnit.K.Description()}";
            return $"{_size} {FileSizeUnit.Byte.Description()}";
        }
    }
}