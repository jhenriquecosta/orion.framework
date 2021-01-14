using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Orion.Framework.Helpers;
using Orion.Framework.Logs;
using Orion.Framework.Logs.Extensions;

namespace Orion.Framework.Web.Mvc.Filters {
   
    public class HtmlAttribute : ActionFilterAttribute {
       
        public string Path { get; set; }

        /// <summary>
        /// ，：Typings/app/{area}/{controller}/{controller}-{action}.component.html
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// ，：/Home/Index
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// ，：true
        /// </summary>
        public bool IsPartialView { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public override async Task OnResultExecutionAsync( ResultExecutingContext context, ResultExecutionDelegate next ) {
            await WriteViewToFileAsync( context );
            await base.OnResultExecutionAsync( context, next );
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task WriteViewToFileAsync( ResultExecutingContext context ) {
            try {
                var html = await RenderToStringAsync( context );
                if( string.IsNullOrWhiteSpace( html ) )
                    return;
                var path = WebHttp.GetPhysicalPath( string.IsNullOrWhiteSpace( Path ) ? GetPath( context ) : Path );
                var directory = System.IO.Path.GetDirectoryName( path );
                if( string.IsNullOrWhiteSpace( directory ) )
                    return;
                if( Directory.Exists( directory ) == false )
                    Directory.CreateDirectory( directory );
                System.IO.File.WriteAllText( path, html );
            }
            catch( Exception ex ) {
                ex.Log( Log.GetLog().Caption( "" ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected async Task<string> RenderToStringAsync( ResultExecutingContext context ) {
            string viewName = "";
            object model = null;
            bool isPage = false;
            if( context.Result is ViewResult result ) {
                viewName = result.ViewName;
                viewName = string.IsNullOrWhiteSpace( viewName ) ? context.RouteData.Values["action"].SafeString() : viewName;
                model = result.Model;
            }

            if (context.Result is PageResult pageResult) {
                if (context.ActionDescriptor is PageActionDescriptor pageActionDescriptor) {
                    isPage = true;
                    model = pageResult.Model;
                    viewName = pageActionDescriptor.RelativePath;
                }
            }
            var razorViewEngine = Ioc.Create<IRazorViewEngine>();
            var compositeViewEngine = Ioc.Create<ICompositeViewEngine>();
            var tempDataProvider = Ioc.Create<ITempDataProvider>();
            var serviceProvider = Ioc.Create<IServiceProvider>();
            var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
            var actionContext = new ActionContext( httpContext, context.RouteData, new ActionDescriptor() );
            using( var stringWriter = new StringWriter() )
            {
                var viewResult = isPage
                    ? GetView(compositeViewEngine, viewName)
                    : GetView(razorViewEngine, actionContext, viewName);
                if( viewResult.View == null )
                    throw new ArgumentNullException( $"： {viewName}" );
                var viewDictionary = new ViewDataDictionary( new EmptyModelMetadataProvider(), new ModelStateDictionary() ) { Model = model };
                var viewContext = new ViewContext( actionContext, viewResult.View, viewDictionary, new TempDataDictionary( actionContext.HttpContext, tempDataProvider ), stringWriter, new HtmlHelperOptions() );
                await viewResult.View.RenderAsync( viewContext );
                return stringWriter.ToString();
            }
        }

        
        /// <returns></returns>
        private ViewEngineResult GetView(IRazorViewEngine razorViewEngine,ActionContext actionContext,string viewName) {
            return razorViewEngine.FindView(actionContext, viewName, true);
        }

      
        private ViewEngineResult GetView(ICompositeViewEngine compositeViewEngine, string path) {
            return compositeViewEngine.GetView("~/", $"~{path}", true);
        }

       
        protected virtual string GetPath( ResultExecutingContext context ) {
            if (context.ActionDescriptor is PageActionDescriptor pageActionDescriptor) {
                var paths = pageActionDescriptor.ViewEnginePath.TrimStart('/').Split('/');
                if (paths.Length >= 3) {
                    return Template.Replace("{area}", paths[0])
                        .Replace("{controller}", paths[1])
                        .Replace("{action}", JoinActionUrl(paths));
                }
                return string.Empty;
            }
            var area = context.RouteData.Values["area"].SafeString();
            var controller = context.RouteData.Values["controller"].SafeString();
            var action = context.RouteData.Values["action"].SafeString();
            var path = Template.Replace( "{area}", area ).Replace( "{controller}", controller ).Replace( "{action}", action );
            return path.ToLower();
        }

       
        private string JoinActionUrl(string[] paths) {
            var result = new StringBuilder();
            for (var i = 2; i < paths.Length; i++) {
                result.Append(paths[i]);
                result.Append("/");
            }
            return result.ToString().TrimEnd('/');
        }
    }
}
