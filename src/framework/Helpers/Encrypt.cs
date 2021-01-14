using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Orion.Framework.Helpers.Internal;

namespace Orion.Framework.Helpers {
    /// <summary>
    /// 
    /// ：
    /// 1. 
    /// 2.  https://github.com/stulzq/DotnetCore.RSA/blob/master/DotnetCore.RSA/RSAHelper.cs
    /// </summary>
    public static class Encrypt {

        #region Md5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static string Md5By16( string value ) {
            return Md5By16( value, Encoding.UTF8 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        public static string Md5By16( string value, Encoding encoding ) {
            return Md5( value, encoding, 4, 8 );
        }

        /// <summary>
        /// 
        /// </summary>
        private static string Md5( string value, Encoding encoding, int? startIndex, int? length ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return string.Empty;
            var md5 = new MD5CryptoServiceProvider();
            string result;
            try {
                var hash = md5.ComputeHash( encoding.GetBytes( value ) );
                result = startIndex == null ? BitConverter.ToString( hash ) : BitConverter.ToString( hash, startIndex.SafeValue(), length.SafeValue() );
            }
            finally {
                md5.Clear();
            }
            return result.Replace( "-", "" );
        }

        /// <summary>
        /// ，
        /// </summary>
        /// <param name="value"></param>
        public static string Md5By32( string value ) {
            return Md5By32( value, Encoding.UTF8 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        public static string Md5By32( string value, Encoding encoding ) {
            return Md5( value, encoding, null, null );
        }

        #endregion

        #region DES加密

        /// <summary>
        /// 
        /// </summary>
        public static string DesKey = "#s^un2ye21fcv%|f0XpR,+vh";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static string DesEncrypt( object value ) {
            return DesEncrypt( value, DesKey );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key">,24位</param>
        public static string DesEncrypt( object value, string key ) {
            return DesEncrypt( value, key, Encoding.UTF8 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key">,</param>
        /// <param name="encoding"></param>
        public static string DesEncrypt( object value, string key, Encoding encoding ) {
            string text = value.SafeString();
            if( ValidateDes( text, key ) == false )
                return string.Empty;
            using( var transform = CreateDesProvider( key ).CreateEncryptor() ) {
                return GetEncryptResult( text, encoding, transform );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static bool ValidateDes( string text, string key ) {
            if( string.IsNullOrWhiteSpace( text ) || string.IsNullOrWhiteSpace( key ) )
                return false;
            return key.Length == 24;
        }

        /// <summary>
        ///
        /// </summary>
        private static TripleDESCryptoServiceProvider CreateDesProvider( string key ) {
            return new TripleDESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes( key ), Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
        }

        /// <summary>
        ///
        /// </summary>
        private static string GetEncryptResult( string value, Encoding encoding, ICryptoTransform transform ) {
            var bytes = encoding.GetBytes( value );
            var result = transform.TransformFinalBlock( bytes, 0, bytes.Length );
            return System.Convert.ToBase64String( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static string DesDecrypt( object value ) {
            return DesDecrypt( value, DesKey );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public static string DesDecrypt( object value, string key ) {
            return DesDecrypt( value, key, Encoding.UTF8 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key">,</param>
        /// <param name="encoding"></param>
        public static string DesDecrypt( object value, string key, Encoding encoding ) {
            string text = value.SafeString();
            if( !ValidateDes( text, key ) )
                return string.Empty;
            using( var transform = CreateDesProvider( key ).CreateDecryptor() ) {
                return GetDecryptResult( text, encoding, transform );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static string GetDecryptResult( string value, Encoding encoding, ICryptoTransform transform ) {
            var bytes = System.Convert.FromBase64String( value );
            var result = transform.TransformFinalBlock( bytes, 0, bytes.Length );
            return encoding.GetString( result );
        }

        #endregion

        #region AES

        /// <summary>
        /// 
        /// </summary>
        private static byte[] _iv;
        /// <summary>
        /// 
        /// </summary>
        private static byte[] Iv {
            get {
                if( _iv == null ) {
                    var size = 16;
                    _iv = new byte[size];
                    for( int i = 0; i < size; i++ )
                        _iv[i] = 0;
                }
                return _iv;
            }
        }

        /// <summary>
        /// AES
        /// </summary>
        public static string AesKey = "QaP1AF8utIarcBqdhYTZpVGbiNQ9M6IL";

        /// <summary>
        /// AE
        /// </summary>
        /// <param name="value"></param>
        public static string AesEncrypt( string value ) {
            return AesEncrypt( value, AesKey );
        }

        /// <summary>
        /// AES
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public static string AesEncrypt( string value, string key ) {
            return AesEncrypt( value, key, Encoding.UTF8 );
        }

        /// <summary>
        /// AES
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <param name="encoding"></param>
        public static string AesEncrypt( string value, string key, Encoding encoding ) {
            if( string.IsNullOrWhiteSpace( value ) || string.IsNullOrWhiteSpace( key ) )
                return string.Empty;
            var rijndaelManaged = CreateRijndaelManaged( key );
            using( var transform = rijndaelManaged.CreateEncryptor( rijndaelManaged.Key, rijndaelManaged.IV ) ) {
                return GetEncryptResult( value, encoding, transform );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static RijndaelManaged CreateRijndaelManaged( string key ) {
            return new RijndaelManaged {
                Key = System.Convert.FromBase64String( key ),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                IV = Iv
            };
        }

        /// <summary>
        /// AES
        /// </summary>
        /// <param name="value"></param>
        public static string AesDecrypt( string value ) {
            return AesDecrypt( value, AesKey );
        }

        /// <summary>
        /// AES
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public static string AesDecrypt( string value, string key ) {
            return AesDecrypt( value, key, Encoding.UTF8 );
        }

        /// <summary>
        /// AES
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <param name="encoding"></param>
        public static string AesDecrypt( string value, string key, Encoding encoding ) {
            if( string.IsNullOrWhiteSpace( value ) || string.IsNullOrWhiteSpace( key ) )
                return string.Empty;
            var rijndaelManaged = CreateRijndaelManaged( key );
            using( var transform = rijndaelManaged.CreateDecryptor( rijndaelManaged.Key, rijndaelManaged.IV ) ) {
                return GetDecryptResult( value, encoding, transform );
            }
        }

        #endregion

        #region RSA

        /// <summary>
        /// RSA SHA1 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public static string RsaSign( string value, string key ) {
            return RsaSign( value, key, Encoding.UTF8 );
        }

        /// <summary>
        /// RSASHA1 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <param name="encoding"></param>
        public static string RsaSign( string value, string key, Encoding encoding ) {
            return RsaSign( value, key, encoding, RSAType.RSA );
        }

        /// <summary>
        /// RSASHA256 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public static string Rsa2Sign( string value, string key ) {
            return Rsa2Sign( value, key, Encoding.UTF8 );
        }

        /// <summary>
        /// RSA SHA256 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <param name="encoding"></param>
        public static string Rsa2Sign( string value, string key, Encoding encoding ) {
            return RsaSign( value, key, encoding, RSAType.RSA2 );
        }

        /// <summary>
        /// Rsa
        /// </summary>
        private static string RsaSign( string value, string key, Encoding encoding, RSAType type ) {
            if( string.IsNullOrWhiteSpace( value ) || string.IsNullOrWhiteSpace( key ) )
                return string.Empty;
            var rsa = new RsaHelper( type, encoding, key );
            return rsa.Sign( value );
        }

        /// <summary>
        /// Rsa
        /// </summary>
        /// <param name="value"></param>
        /// <param name="publicKey"></param>
        /// <param name="sign"></param>
        public static bool RsaVerify( string value, string publicKey, string sign ) {
            return RsaVerify( value, publicKey, sign, Encoding.UTF8 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="publicKey"></param>
        /// <param name="sign"></param>
        /// <param name="encoding"></param>
        public static bool RsaVerify( string value, string publicKey, string sign, Encoding encoding ) {
            return RsaVerify( value, publicKey, sign, encoding, RSAType.RSA );
        }

        /// <summary>
        /// Rsa
        /// </summary>
        /// <param name="value"></param>
        /// <param name="publicKey"></param>
        /// <param name="sign"></param>
        public static bool Rsa2Verify( string value, string publicKey, string sign ) {
            return Rsa2Verify( value, publicKey, sign, Encoding.UTF8 );
        }

        /// <summary>
        /// Rsa
        /// </summary>
        /// <param name="value"></param>
        /// <param name="publicKey"></param>
        /// <param name="sign"></param>
        /// <param name="encoding"></param>
        public static bool Rsa2Verify( string value, string publicKey, string sign, Encoding encoding ) {
            return RsaVerify( value, publicKey, sign, encoding, RSAType.RSA2 );
        }

        /// <summary>
        /// 
        /// </summary>
        private static bool RsaVerify( string value, string publicKey, string sign, Encoding encoding, RSAType type ) {
            if( string.IsNullOrWhiteSpace( value ) || string.IsNullOrWhiteSpace( publicKey ) || string.IsNullOrWhiteSpace( sign ) )
                return false;
            var rsa = new RsaHelper( type, encoding, publicKey: publicKey );
            return rsa.Verify( value, sign );
        }

        #endregion

        #region HmacSha256加密

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public static string HmacSha256( string value, string key ) {
            return HmacSha256( value, key,Encoding.UTF8 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <param name="encoding"></param>
        public static string HmacSha256( string value,string key, Encoding encoding ) {
            if( string.IsNullOrWhiteSpace( value ) || string.IsNullOrWhiteSpace( key ) )
                return string.Empty;
            var sha256 = new HMACSHA256( encoding.GetBytes( key ) );
            var hash = sha256.ComputeHash( encoding.GetBytes( value ) );
            return string.Join( "", hash.ToList().Select( t => t.ToString( "x2" ) ).ToArray() );
        }

        #endregion
    }
}
