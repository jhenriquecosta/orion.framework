using System.IO;
using System.Text;
using System.Threading.Tasks;
using Orion.Framework.Exceptions;

namespace Orion.Framework.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class File
    {

        public static void CreateDirectory(string physicalPath)
        {
            var directory =physicalPath;
            if (string.IsNullOrEmpty(directory))
                throw new Warning("Erro");
            if (Directory.Exists(directory) == false)
                Directory.CreateDirectory(directory);
        }
        /// <>
        /// </summary>
        /// <param name="stream"></param>
        public static async Task<byte[]> ToBytesAsync(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, 0, buffer.Length);
            return buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public static byte[] ToBytes(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>        
        public static byte[] ToBytes(string data)
        {
            return ToBytes(data, Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        public static byte[] ToBytes(string data, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(data))
                return new byte[] { };
            return encoding.GetBytes(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public static byte[] Read(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return null;
            var fileInfo = new FileInfo(filePath);
            using (var reader = new BinaryReader(fileInfo.Open(FileMode.Open)))
            {
                return reader.ReadBytes((int)fileInfo.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <param name="bufferSize"></param>
        /// <param name="isCloseStream">，</param>
        public static string ToString(Stream stream, Encoding encoding = null, int bufferSize = 1024 * 2, bool isCloseStream = true)
        {
            if (stream == null)
                return string.Empty;
            if (encoding == null)
                encoding = Encoding.UTF8;
            if (stream.CanRead == false)
                return string.Empty;
            using (var reader = new StreamReader(stream, encoding, true, bufferSize, !isCloseStream))
            {
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);
                var result = reader.ReadToEnd();
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding">=</param>
        /// <param name="bufferSize">=</param>
        /// <param name="isCloseStream">=</param>
        public static async Task<string> ToStringAsync(Stream stream, Encoding encoding = null, int bufferSize = 1024 * 2, bool isCloseStream = true)
        {
            if (stream == null)
                return string.Empty;
            if (encoding == null)
                encoding = Encoding.UTF8;
            if (stream.CanRead == false)
                return string.Empty;
            using (var reader = new StreamReader(stream, encoding, true, bufferSize, !isCloseStream))
            {
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);
                var result = await reader.ReadToEndAsync();
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        public static async Task<string> CopyToStringAsync(Stream stream, Encoding encoding = null)
        {
            if (stream == null)
                return string.Empty;
            if (encoding == null)
                encoding = Encoding.UTF8;
            if (stream.CanRead == false)
                return string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                using (var reader = new StreamReader(memoryStream, encoding))
                {
                    if (stream.CanSeek)
                        stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(memoryStream);
                    if (memoryStream.CanSeek)
                        memoryStream.Seek(0, SeekOrigin.Begin);
                    var result = await reader.ReadToEndAsync();
                    if (stream.CanSeek)
                        stream.Seek(0, SeekOrigin.Begin);
                    return result;
                }
            }
        }
    }
}
