using System;
using System.Linq.Expressions;
using Orion.Framework.Pdf.Reports.FluentInterface;
using Orion.Framework.Domains.Services;

namespace Orion.Framework.Pdf.Reports
{
    public interface IReport : IDomainService
    {
        string Descricao { get; set; }
        void SetFilter<T>(Expression<Func<T, bool>> expression = null);
        T GetFilter<T>();
        PdfReport GetReport();
    }
}
