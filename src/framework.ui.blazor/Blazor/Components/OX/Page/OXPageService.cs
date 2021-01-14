using Microsoft.AspNetCore.Components;
using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.Ui.Blazor.Reports;
using Orion.Framework.Pdf.Reports;

namespace Orion.Framework.Ui.Blazor.Components
{

    public abstract class OXPageService : OXPageDialog
    {

        [Inject] public IReportService ReportService { get; set; }
        [Inject] public IReportViewerService ReportViewer { get; set; }
        [Inject] public AppDataTransfer AppDataTransfer { get; set; }
    }

}
