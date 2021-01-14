using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orion.Framework;
using Orion.Framework.Domains.Enums;
using Orion.Framework.Utilities;
using Orion.Framework.Web.Mvc.Extensions;

namespace Orion.Prometheus.Blazor
{
    public class Startup
    {
        
        IWebHostEnvironment _webEnviroment;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _webEnviroment = env;
        }
       
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return FWorkBootstrapping.With
            .Services(services, _webEnviroment)
            .Settings()
            .Blazor()
            .NLog()
            .Caches(options => options.UseSQLite(option => { }))
            .BuildProvider();
        }

        public void ConfigureDevelopment(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            CommonConfig(app);
        }
        public void ConfigureProduction(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/Error");
            CommonConfig(app);
        }
        private void CommonConfig(IApplicationBuilder app)
        {
          
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseResponseCompression();
            ConfigFWorks(app);
            ConfigRoute(app);
        }
        private void ConfigFWorks(IApplicationBuilder app)
        {
            FWorkBootstrapping.With.Applications(app)
           .UseErrorLog()
           .UseFramework();
            ConfigMenu();
            //var menuService = Ioc.Create<SysComponentService>();
            //var items = AsyncUtil.RunSync(()=> menuService.GetMenuAsync());
            //AppHelper.AddCache("sys_menu", items);

        }

        private void ConfigMenu()
        {
            //var menuService = Ioc.Create<SysComponentService>();
            //var items = AsyncUtil.RunSync(()=> menuService.GetMenuAsync());
            //AppHelper.AddCache("sys_menu", items);
            
        }

        private void ConfigRoute(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");

            });
        }
        
    }
   
}
