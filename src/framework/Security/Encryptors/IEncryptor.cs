namespace Orion.Framework.Security.Encryptors {
    /// <summary>
    /// 
    /// </summary>
    public interface IEncryptor {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        string Encrypt( string data );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        string Decrypt( string data );
    }
}