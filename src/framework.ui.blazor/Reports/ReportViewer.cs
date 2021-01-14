using Microsoft.AspNetCore.Components;
using Orion.Framework.Caches;
using Orion.Framework.Pdf.Reports.FluentInterface;
using Orion.Framework.Settings;
using System;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Reports
{

    public class ReportViewerService : IReportViewerService
    {
        private readonly NavigationManager _navigator;
        private readonly ICache _cache;
        private readonly IXTSysSettings _settings;
        public ReportViewerService(NavigationManager navigation, ICache cache,IXTSysSettings settings)
        {
            this._settings = settings;
            this._cache = cache;
            this._navigator = navigation;
        }
        public async Task ViewerAsync(PdfReport pdfReport)
        {
            var guid = Guid.NewGuid();
            var rptName = guid.ToString();
           // pdfReport.Generate(d => d.AsPdfFile(XTUtilities.GetOutputFileName(rptName)));
            await _cache.TryAddAsync<string>(guid.ToString(), rptName);
           
            await Task.Delay(100);
            var url = $"reports/viewer/{guid}";
            _navigator.NavigateTo(url);
        }
        public async Task<string> GetReport(string key)
        {

            var rpt = await _cache.GetAsync<string>(key);
            _cache.Remove(key);
            return rpt;
           
        }
    }
}
