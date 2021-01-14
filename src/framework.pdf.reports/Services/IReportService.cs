using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Orion.Framework.Pdf.Reports.FluentInterface;
using Orion.Framework.Domains.Services;
using Orion.Framework.Domains.ValueObjects;

namespace Orion.Framework.Pdf.Reports
{
    public interface IReportService : IDomainService
    {
        List<DataItemCombo> GetReports();
        PdfReport ExecuteReport<T>(DataItemCombo dataItem, Expression<Func<T, bool>> expression = null);
        List<DataItemCombo> ItemLookup(Expression<Func<DataItemCombo, bool>> expression = null);

    }
}
