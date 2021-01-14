using Orion.Framework.Dependency;
using Orion.Framework.Pdf.Reports.FluentInterface;
using System.Threading.Tasks;

namespace Orion.Framework.Ui.Blazor.Reports
{
    public interface IReportViewerService : IScopeDependency
    {
        Task ViewerAsync(PdfReport pdfReport);
        Task<string> GetReport(string key);
    }
}
