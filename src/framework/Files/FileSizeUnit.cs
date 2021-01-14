using System.ComponentModel;


namespace Orion.Framework.Files {
    /// <summary>
    /// 
    /// </summary>
    public enum FileSizeUnit {
        /// <summary>
        /// 
        /// </summary>
        [Description( "B" )]
        Byte,
        /// <summary>
        /// K字节
        /// </summary>
        [Description( "KB" )]
        K,
        /// <summary>
        /// M字节
        /// </summary>
        [Description( "MB" )]
        M,
        /// <summary>
        /// G字节
        /// </summary>
        [Description( "GB" )]
        G
    }

    /// <summary>
    /// 
    /// </summary>
    public static class FileSizeUnitExtensions {
        /// <summary>
        /// 
        /// </summary>
        public static string Description( this FileSizeUnit? unit ) {
            return unit == null ? string.Empty : unit.Value.Description();
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public static int? Value( this FileSizeUnit? unit ) {
            return unit?.Value();
        }
    }
}