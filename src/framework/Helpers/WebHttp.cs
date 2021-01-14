using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using Orion.Framework.Security.Core.Principals;

namespace Orion.Framework.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebHttp
    {

        #region 

        /// <summary>
        /// 
        /// </summary>
        static WebHttp()
        {
            try
            {
                HttpContextAccessor = Ioc.Create<IHttpContextAccessor>();
                Environment = Ioc.Create<IHostingEnvironment>();
               
            }
            catch
            {
            }
        }

        #endregion

        #region 

        /// <summary>
        /// 
        /// </summary>
        public static IHttpContextAccessor HttpContextAccessor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static HttpContext HttpContext => HttpContextAccessor?.HttpContext;

        /// <summary>
        /// 
        /// </summary>
        public static HttpRequest Request => HttpContext?.Request;

        /// <summary>
        /// 
        /// </summary>
        public static HttpResponse Response => HttpContext?.Response;

        /// <summary>
        /// 
        /// </summary>
        public static IHostingEnvironment Environment { get; set; }

        public static bool IsWebContext()
        {
            return WebHttp.HttpContext?.RequestServices != null;
        }


        #endregion

        #region User()

        /// <summary>
        /// 
        /// </summary>
        public static ClaimsPrincipal User
        {
            get
            {
                if (HttpContext == null)
                    return UnauthenticatedPrincipal.Instance;
                if (HttpContext.User is ClaimsPrincipal principal)
                    return principal;
                return UnauthenticatedPrincipal.Instance;
            }
        }

        #endregion

        #region Identity()

        /// <summary>
        /// 
        /// </summary>
        public static ClaimsIdentity Identity
        {
            get
            {
                if (User.Identity is ClaimsIdentity identity)
                    return identity;
                return UnauthenticatedIdentity.Instance;
            }
        }

        #endregion

        #region Body()

        /// <summary>
        /// 
        /// </summary>
        public static string Body
        {
            get
            {
                Request.EnableRewind();
                return File.ToString(Request.Body, isCloseStream: false);
            }
        }

        #endregion
        #region GetBodyAsync()

        /// <summary>
        /// 
        /// </summary>
        public static async Task<string> GetBodyAsync()
        {
            Request.EnableRewind();
            return await File.ToStringAsync(Request.Body, isCloseStream: false);
        }

        #endregion

        #region Client(  )

        /// <summary>
        /// ，
        /// </summary>
        public static Orion.Framework.Web.Clients.WebClient Client()
        {
            return new Orion.Framework.Web.Clients.WebClient();
        }

        /// <summary>
        /// Web
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        public static Orion.Framework.Web.Clients.WebClient<TResult> Client<TResult>() where TResult : class
        {
            return new Orion.Framework.Web.Clients.WebClient<TResult>();
        }

        #endregion

        #region Url()

        /// <summary>
        /// 
        /// </summary>
        public static string Url => Request?.GetDisplayUrl();

        #endregion

        #region Ip()

        /// <summary>
        /// 
        /// </summary>
        private static string _ip;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        public static void SetIp(string ip)
        {
            _ip = ip;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ResetIp()
        {
            _ip = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Ip
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ip) == false)
                    return _ip;
                var list = new[] { "127.0.0.1", "::1" };
                var result = HttpContext?.Connection?.RemoteIpAddress.SafeString();
                if (string.IsNullOrWhiteSpace(result) || list.Contains(result))
                    result = Common.IsWindows ? GetLanIp() : GetLanIp(NetworkInterfaceType.Ethernet);
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    return hostAddress.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        private static string GetLanIp(NetworkInterfaceType type)
        {
            try
            {
                foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (item.NetworkInterfaceType != type || item.OperationalStatus != OperationalStatus.Up)
                        continue;
                    var ipProperties = item.GetIPProperties();
                    if (ipProperties.GatewayAddresses.FirstOrDefault() == null)
                        continue;
                    foreach (var ip in ipProperties.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            return ip.Address.ToString();
                    }
                }
            }
            catch
            {
                return string.Empty;
            }

            return string.Empty;
        }

        #endregion

        #region Host(主机)

        /// <summary>
        /// 
        /// </summary>
        public static string Host => HttpContext == null ? Dns.GetHostName() : GetClientHostName();

        /// <summary>
        /// 
        /// </summary>
        private static string GetClientHostName()
        {
            var address = GetRemoteAddress();
            if (string.IsNullOrWhiteSpace(address))
                return Dns.GetHostName();
            var result = Dns.GetHostEntry(IPAddress.Parse(address)).HostName;
            if (result == "localhost.localdomain")
                result = Dns.GetHostName();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private static string GetRemoteAddress()
        {
            return Request?.Headers["HTTP_X_FORWARDED_FOR"] ?? Request?.Headers["REMOTE_ADDR"];
        }

        #endregion

        #region Browser()

        /// <summary>
        /// 浏览器
        /// </summary>
        public static string Browser => Request?.Headers["User-Agent"];

        #endregion

        #region RootPath()

        /// <summary>
        /// 根路径
        /// </summary>
        public static string RootPath => Environment?.ContentRootPath;

        #endregion 

        #region WebRootPath()

        /// <summary>
        /// Web根路径，即wwwroot
        /// </summary>
        public static string WebRootPath => Environment?.WebRootPath;

        #endregion 

        #region GetFiles()

        /// <summary>
        /// 
        /// </summary>
        public static List<IFormFile> GetFiles()
        {
            var result = new List<IFormFile>();
            var files = Request.Form.Files;
            if (files == null || files.Count == 0)
                return result;
            result.AddRange(files.Where(file => file?.Length > 0));
            return result;
        }

        #endregion

        #region GetFile()

        /// <summary>
        /// 
        /// </summary>
        public static IFormFile GetFile()
        {
            var files = GetFiles();
            return files.Count == 0 ? null : files[0];
        }

        #endregion

        #region GetParam()

        /// <summary>
        /// ，：->->
        /// </summary>
        /// <param name="name"></param>
        public static string GetParam(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;
            if (Request == null)
                return string.Empty;
            var result = string.Empty;
            if (Request.Query != null)
                result = Request.Query[name];
            if (string.IsNullOrWhiteSpace(result) == false)
                return result;
            if (Request.Form != null)
                result = Request.Form[name];
            if (string.IsNullOrWhiteSpace(result) == false)
                return result;
            if (Request.Headers != null)
                result = Request.Headers[name];
            return result;
        }

        #endregion

        #region UrlEncode()

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="isUpper"></param>
        public static string UrlEncode(string url, bool isUpper = false)
        {
            return UrlEncode(url, Encoding.UTF8, isUpper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding"></param>
        /// <param name="isUpper"></param>
        public static string UrlEncode(string url, string encoding, bool isUpper = false)
        {
            encoding = string.IsNullOrWhiteSpace(encoding) ? "UTF-8" : encoding;
            return UrlEncode(url, Encoding.GetEncoding(encoding), isUpper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding"></param>
        /// <param name="isUpper">"</param>
        public static string UrlEncode(string url, Encoding encoding, bool isUpper = false)
        {
            var result = HttpUtility.UrlEncode(url, encoding);
            if (isUpper == false)
                return result;
            return GetUpperEncode(result);
        }

        /// <summary>
        /// 
        /// </summary>
        private static string GetUpperEncode(string encode)
        {
            var result = new StringBuilder();
            int index = int.MinValue;
            for (int i = 0; i < encode.Length; i++)
            {
                string character = encode[i].ToString();
                if (character == "%")
                    index = i;
                if (i - index == 1 || i - index == 2)
                    character = character.ToUpper();
                result.Append(character);
            }
            return result.ToString();
        }

        #endregion

        #region UrlDecode()

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url</param>
        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding"></param>
        public static string UrlDecode(string url, Encoding encoding)
        {
            return HttpUtility.UrlDecode(url, encoding);
        }

        #endregion

        #region DownloadAsync()

       
        public static async Task DownloadFileAsync(string filePath, string fileName)
        {
            await DownloadFileAsync(filePath, fileName, Encoding.UTF8);
        }

       
        public static async Task DownloadFileAsync(string filePath, string fileName, Encoding encoding)
        {
            var bytes = File.Read(filePath);
            await DownloadAsync(bytes, fileName, encoding);
        }

      
        public static async Task DownloadAsync(Stream stream, string fileName)
        {
            await DownloadAsync(stream, fileName, Encoding.UTF8);
        }

        public static async Task DownloadAsync(Stream stream, string fileName, Encoding encoding)
        {
            await DownloadAsync(File.ToBytes(stream), fileName, encoding);
        }

       
        public static async Task DownloadAsync(byte[] bytes, string fileName)
        {
            await DownloadAsync(bytes, fileName, Encoding.UTF8);
        }

     
        public static async Task DownloadAsync(byte[] bytes, string fileName, Encoding encoding)
        {
            if (bytes == null || bytes.Length == 0)
                return;
            fileName = fileName.Replace(" ", "");
            fileName = UrlEncode(fileName, encoding);
            Response.ContentType = "application/octet-stream";
            Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");
            Response.Headers.Add("Content-Length", bytes.Length.ToString());
            await Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }

        #endregion
        #region PATH
        public static string GetPhysicalPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return string.Empty;
            var rootPath =RootPath;
            if (string.IsNullOrWhiteSpace(rootPath))
                return Path.GetFullPath(relativePath);
            return $"{RootPath}\\{relativePath.Replace("/", "\\").TrimStart('\\')}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePath"></param>
        public static string GetWebRootPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return string.Empty;
            var rootPath = WebRootPath;
            if (string.IsNullOrWhiteSpace(rootPath))
                return Path.GetFullPath(relativePath);
            return $"{WebRootPath}\\{relativePath.Replace("/", "\\").TrimStart('\\')}";
        }
        #endregion
    }
}