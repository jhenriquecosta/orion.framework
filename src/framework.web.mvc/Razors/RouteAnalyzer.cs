using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Orion.Framework.Web.Mvc.Filters;

namespace Orion.Framework.Web.Mvc.Razors {
   
    public class RouteAnalyzer : IRouteAnalyzer {
      
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        
        private readonly IPageLoader _pageLoader;

        
        public RouteAnalyzer( IActionDescriptorCollectionProvider actionDescriptorCollectionProvider , IPageLoader pageLoader ) {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _pageLoader = pageLoader;
        }

       
        public IEnumerable<RouteInformation> GetAllRouteInformations() {
            List<RouteInformation> list = new List<RouteInformation>();

            var actionDescriptors = this._actionDescriptorCollectionProvider.ActionDescriptors.Items;
            foreach( var actionDescriptor in actionDescriptors ) {
                RouteInformation info = new RouteInformation();
                if( actionDescriptor.RouteValues.ContainsKey( "area" ) ) {
                    info.AreaName = actionDescriptor.RouteValues["area"];
                }
            
                if( actionDescriptor is PageActionDescriptor pageActionDescriptor )
                {
                    var compiledPage = _pageLoader.Load(pageActionDescriptor);
                    info.Path = pageActionDescriptor.ViewEnginePath;
                    info.Invocation = pageActionDescriptor.RelativePath;
                    SetHtmlInfo(info, compiledPage);
                    if (!list.Exists(x => x.Invocation == info.Invocation))
                    {
                        list.Add(info);
                    }
                    continue;
                }
            
                if( actionDescriptor.AttributeRouteInfo != null ) {
                    info.Path = $"/{actionDescriptor.AttributeRouteInfo.Template}";
                }
                // Controller/Action 
                if( actionDescriptor is ControllerActionDescriptor controllerActionDescriptor ) {
                    if( info.Path.IsEmpty() ) {
                        info.Path = $"/{controllerActionDescriptor.ControllerName}/{controllerActionDescriptor.ActionName}";
                    }
                    SetHtmlInfo( info, controllerActionDescriptor );
                    info.ControllerName = controllerActionDescriptor.ControllerName;
                    info.ActionName = controllerActionDescriptor.ActionName;
                    info.Invocation = $"{controllerActionDescriptor.ControllerName}Controller.{controllerActionDescriptor.ActionName}";
                }
                info.Invocation += $"({actionDescriptor.DisplayName})";
                list.Add( info );
            }

            return list;
        }

       
        private void SetHtmlInfo( RouteInformation routeInformation,
            ControllerActionDescriptor controllerActionDescriptor ) {
            var htmlAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<HtmlAttribute>() ??
                                controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<HtmlAttribute>();
            if( htmlAttribute == null )
                return;
            routeInformation.FilePath = htmlAttribute.Path;
            routeInformation.TemplatePath = htmlAttribute.Template;
            routeInformation.IsPartialView = htmlAttribute.IsPartialView;
            routeInformation.ViewName = htmlAttribute.ViewName;
        }

        private void SetHtmlInfo(RouteInformation routeInformation,
            CompiledPageActionDescriptor compiledPageActionDescriptor)
        {
            routeInformation.IsPageRoute = true;
            var htmlAttribute = compiledPageActionDescriptor.PageTypeInfo.GetCustomAttribute<HtmlAttribute>() ??
                                compiledPageActionDescriptor.DeclaredModelTypeInfo.GetCustomAttribute<HtmlAttribute>();
            if (htmlAttribute == null)
                return;
            routeInformation.FilePath = htmlAttribute.Path;
            routeInformation.TemplatePath = htmlAttribute.Template;
            routeInformation.IsPartialView = htmlAttribute.IsPartialView;
            routeInformation.ViewName = htmlAttribute.ViewName;
        }
    }
}
