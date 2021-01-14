using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Orion.Framework;
using Orion.Framework.Settings;
using Orion.Framework.Caches;

using System.Linq;
using System.Net.Http;
using EasyCaching.Core.Configurations;
using System;
using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.DataLayer.NH.Dependency;
using Orion.Framework.Dependency;
using Orion.Framework.Web.Mvc.Extensions;
using EmbeddedBlazorContent;
using Blazorise.Material;
using Orion.Framework.Ui.FWorks.Blazor.Sf.Forms;
using Blazorise.Icons.Material;
using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.Pdf.Reports;
using Orion.Framework.Pdf.Reports.FluentInterface;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;

namespace Orion.Prometheus.Blazor
{

    public sealed class FWorkBootstrapping
    {
        private static IServiceCollection _services;
        private static IWebHostEnvironment _webHost;
        private static IApplicationBuilder _app;
        private IConfig nhConfig;

        public static FWorkBootstrapping With
        {
            get
            {
                return Nested.FWorkInstance;
            }
        }
        private class Nested
        {
            static Nested() { }
            internal static readonly FWorkBootstrapping FWorkInstance = new FWorkBootstrapping();
        }
        private FWorkBootstrapping()
        {

        }
        public FWorkBootstrapping Applications(IApplicationBuilder application)
        {
            _app = application;
            return this;
        }
        #region IServiceCollection
        public FWorkBootstrapping Services(IServiceCollection services, IWebHostEnvironment webHost)
        {
            _services = services;
            _webHost = webHost;
            return this;
        }
        public FWorkBootstrapping Settings()
        {
            var sysApp = new XTSysSettings();
            sysApp.Initialize(_services).AddSysSettings().AddSysFolders(_webHost.WebRootPath);
            _services.AddSingleton<IXTAppConfiguration, XTAppConfiguration>();
            _services.AddScoped<IXTSysSettings>(s => sysApp);
            AppHelper.Initialize(_services);
            return this;
        }
        public FWorkBootstrapping Blazor()
        {
            _services.AddSignalR(e => { e.MaximumReceiveMessageSize = 102400000; });
            _services.AddRazorPages();
            _services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; }).AddHubOptions((o) => { o.MaximumReceiveMessageSize = 1024 * 1024 * 100; });
            _services.AddControllers().AddNewtonsoftJson();
            _services.AddScoped<HttpClient>();
            _services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            _services.AddResponseCompression(opts => { opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" }); });
            return this;
        }
        public FWorkBootstrapping NLog()
        {
            _services.AddNLog();
            return this;
        }
        public FWorkBootstrapping Caches(Action<EasyCachingOptions> configAction)
        {
            _services.AddCache(configAction);
            return this;
        }

        public FWorkBootstrapping NHibernate<TContext>() where TContext : ISessionContext
        {
            nhConfig = new IocConfigNHibernate();
            _services.AddNHibernate().WithAuditInterceptor().AddContext<TContext>();
            return this;
        }
        public IServiceProvider BuildProvider(params IConfig[] configs)
        {   
            var provider = _services.AddOrionFramework(configs);
            return provider;
        }
        #endregion
        #region Application

        public FWorkBootstrapping UseErrorLog()
        {
            _app.UseErrorLog();
            return this;
        }
        public FWorkBootstrapping UseFramework()
        {
            _app.UseEmbeddedBlazorContent(typeof(PageForm).Assembly);
            _app.ApplicationServices.UseMaterialProviders().UseMaterialIcons();
          
            return this;
        }


        #endregion



    }


    #region ReportService
    public class ReportService : IReportService
    {
        private static int _IdLookup = 0;
        private static List<DataItemCombo> _listReports;

        public ReportService()
        {
            GetReports();
        }
        public List<DataItemCombo> ItemLookup(Expression<Func<DataItemCombo, bool>> expression)
        {

            var items = _listReports.AsQueryable().Where(expression);
            var fnReturn = items.ToList();
            return fnReturn;

        }

        public List<DataItemCombo> GetReports()
        {
            var list = Assembly.GetExecutingAssembly().GetTypes().Where(f => f.BaseType == typeof(ReportBase)).ToList();
            _IdLookup = 0;
            _listReports = new List<DataItemCombo>();
            foreach (var item in list)
            {
                _IdLookup++;
                var rpt = TypeHelper.CreateInstance(item.FullName);
                var _rpt = rpt as ReportBase;
                var dto = new DataItemCombo
                {
                    Id = _IdLookup,
                    Key = _IdLookup,
                    Text = _rpt.Descricao,
                    Descricao = item.Name,
                    Value = rpt
                };
                _listReports.Add(dto);
            }
            return _listReports;
        }
        public PdfReport ExecuteReport<T>(DataItemCombo dataItem, Expression<Func<T, bool>> expression = null)
        {
            if (dataItem == null) return null;
            var rpt = _listReports.SingleOrDefault(f => f.Key == dataItem.Key);
            var _rpt = rpt.Value as ReportBase;
            if (_rpt == null) return null;
            _rpt.SetFilter(expression);
            return _rpt.GetReport();

        }
    }
    #endregion
}
