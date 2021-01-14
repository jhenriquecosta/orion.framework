using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Orion.Framework.Helpers;
using Orion.Framework.Logs;
using Orion.Framework.Logs.Extensions;

namespace Orion.Framework.Web.Mvc.Razors {
    /// <summary>
    /// 
    /// </summary>
    public class DefaultRazorHtmlGenerator : IRazorHtmlGenerator {
        private readonly IRouteAnalyzer _routeAnalyzer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeAnalyzer"></param>
        public DefaultRazorHtmlGenerator( IRouteAnalyzer routeAnalyzer ) {
            _routeAnalyzer = routeAnalyzer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Generate() {
            foreach( var routeInformation in _routeAnalyzer.GetAllRouteInformations() ) {
                // 
                if( routeInformation.Path.StartsWith( "/api" ) ) {
                    continue;
                }
                await WriteViewToFileAsync( routeInformation );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private async Task<string> RenderActionViewToStringAsync(RouteInformation info) {
            var razorViewEngine = Ioc.Create<IRazorViewEngine>();
            var tempDataProvider = Ioc.Create<ITempDataProvider>();
            var serviceProvider = Ioc.Create<IServiceProvider>();
            var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
            var actionContext = new ActionContext(httpContext, GetRouteData(info), new ActionDescriptor());
            var viewResult = GetView(razorViewEngine, actionContext, info);
            if (!viewResult.Success) {
                throw new InvalidOperationException($" {info.ActionName}");
            }
            using (var stringWriter = new StringWriter()) {
                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, new TempDataDictionary(actionContext.HttpContext, tempDataProvider), stringWriter, new HtmlHelperOptions());
                await viewResult.View.RenderAsync(viewContext);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private async Task<string> RenderPageViewToStringAsync(RouteInformation info) {
            var engine = Ioc.Create<ICompositeViewEngine>();
            var serviceProvider = Ioc.Create<IServiceProvider>();
            var tempDataProvider = Ioc.Create<ITempDataProvider>();
            var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
            var viewResult = GetView(engine, info);
            if (!viewResult.Success) {
                throw new InvalidOperationException($" {info.Invocation}");
            }

            using (var output = new StringWriter()) {
                var actionContext = new ActionContext(httpContext, GetRouteData(info), new ActionDescriptor());
                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, new TempDataDictionary(actionContext.HttpContext, tempDataProvider), output, new HtmlHelperOptions());
                await viewResult.View.RenderAsync(viewContext);
                return output.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task WriteViewToFileAsync( RouteInformation info ) {
            try
            {
                var html = info.IsPageRoute
                    ? await RenderPageViewToStringAsync(info)
                    : await RenderActionViewToStringAsync(info);
                if( string.IsNullOrWhiteSpace( html ) )
                    return;
                var path = Orion.Framework.Helpers.WebHttp.GetPhysicalPath( string.IsNullOrWhiteSpace( info.FilePath ) ? GetPath( info ) : info.FilePath );
                var directory = System.IO.Path.GetDirectoryName( path );
                if( string.IsNullOrWhiteSpace( directory ) )
                    return;
                if( Directory.Exists( directory ) == false )
                    Directory.CreateDirectory( directory );
                System.IO.File.WriteAllText( path, html );
            }
            catch( Exception ex ) {
                ex.Log( Log.GetLog().Caption( "HTML" ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual string GetPath( RouteInformation info ) {
            if (!info.IsPageRoute) {
                var area = info.AreaName.SafeString();
                var controller = info.ControllerName.SafeString();
                var action = info.ActionName.SafeString();
                var path = info.TemplatePath.Replace("{area}", area).Replace("{controller}", controller).Replace("{action}", action);
                return path.ToLower();
            }

            if (!string.IsNullOrWhiteSpace(info.FilePath)) {
                return info.FilePath.ToLower();
            }
            var paths = info.Path.TrimStart('/').Split('/');
            if (paths.Length >= 3) {
                return info.TemplatePath.Replace("{area}", paths[0])
                    .Replace("{controller}", paths[1])
                    .Replace("{action}", JoinActionUrl(paths));
            }
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        private string JoinActionUrl(string[] paths)
        {
            var result = new StringBuilder();
            for (var i = 2; i < paths.Length; i++)
            {
                result.Append(paths[i]);
                result.Append("/");
            }
            return result.ToString().TrimEnd('/');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="razorViewEngine"></param>
        /// <param name="actionContext">>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual ViewEngineResult GetView( IRazorViewEngine razorViewEngine, ActionContext actionContext, RouteInformation info ) {
            return razorViewEngine.FindView( actionContext, info.ViewName.IsEmpty() ? info.ActionName : info.ViewName,
                !info.IsPartialView );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual ViewEngineResult GetView(ICompositeViewEngine engine, RouteInformation info) {
            return engine.GetView("~/", $"~{info.Invocation}", !info.IsPartialView);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual RouteData GetRouteData( RouteInformation info ) {
            var routeData = new RouteData();
            if( !info.AreaName.IsEmpty() ) {
                routeData.Values.Add( "area", info.AreaName );
            }
            if( !info.ControllerName.IsEmpty() ) {
                routeData.Values.Add( "controller", info.ControllerName );
            }
            if( !info.ActionName.IsEmpty() ) {
                routeData.Values.Add( "action", info.ActionName );
            }

            if (info.IsPageRoute)
            {
                routeData.Values.Add("page", info.Path);
            }
            return routeData;
        }
    }
}
