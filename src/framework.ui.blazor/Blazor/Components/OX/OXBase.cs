using Microsoft.AspNetCore.Components;
using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.Pdf.Reports;
using Orion.Framework.Ui.Blazor.Reports;

namespace Orion.Framework.Ui.Blazor.Components
{
    public abstract class OXBase : FWorkBlazorComponent
    {
        [Inject] public IReportService ReportService { get; set; }
        [Inject] public IReportViewerService ReportViewer { get; set; }
        [Inject] public AppDataTransfer AppDataTransfer { get; set; }
    }

}
