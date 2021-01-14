namespace Orion.Framework.Security.Encryptors {
    /// <summary>
    /// 
    /// </summary>
    public class NullEncryptor : IEncryptor {
        /// <summary>
        /// 
        /// </summary>
        public static readonly IEncryptor Instance = new NullEncryptor();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public string Encrypt( string data ) {
            return string.Empty;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        public string Decrypt( string data ) {
            return string.Empty;
        }
    }
}
