using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Orion.Framework.Logs;
using Orion.Framework.Logs.Extensions;

namespace Orion.Framework.Web.Mvc.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class RealIpMiddleware {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        private readonly RealIpOptions _options;

        /// <summary>
        /// see cref="RealIpMiddleware"/>
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public RealIpMiddleware(RequestDelegate next, IOptions<RealIpOptions> options) {
            _next = next;
            _options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public async Task Invoke(HttpContext context) {
            var headers = context.Request.Headers;
            try {
                if (headers.ContainsKey(_options.HeaderKey)) {
                    context.Connection.RemoteIpAddress = IPAddress.Parse(
                        _options.HeaderKey.ToLower() == "x-forwarded-for"
                            ? headers["X-Forwarded-For"].ToString().Split(',')[0]
                            : headers[_options.HeaderKey].ToString());
                    WriteLog(context, context.Connection.RemoteIpAddress);
                }
            }
            finally {
                await _next(context);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="address">>
        private void WriteLog(HttpContext context, IPAddress address) {
            if(context==null)
                return;
            var log = Log.GetLog(this);
            if (!log.IsDebugEnabled)
                return;
            log.Caption("Ip")
                .Content($"Address : {address}")
                .Debug();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RealIpOptions {
        /// <summary>
        /// 
        /// </summary>
        public string HeaderKey { get; set; } = "X-Forwarded-For";
    }
}
