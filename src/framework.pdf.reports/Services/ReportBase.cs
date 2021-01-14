using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Orion.Framework.Pdf.Reports.Core.Contracts;
using Orion.Framework.Pdf.Reports.FluentInterface;

namespace Orion.Framework.Pdf.Reports
{
    public abstract class ReportBase : IReport
    {
        static object filter;
        public string Descricao { get; set; }
        public void SetFilter<T>(Expression<Func<T,bool>> expression=null)
        {
            filter = expression;
        }
        public T GetFilter<T>()
        {
            return (T)filter;
        }
        public virtual PdfReport GetReport()
        {
            throw new NotImplementedException();
        }
    }
    public abstract class PageHeaderBase : IPageHeader
    {
        public IPdfFont PdfRptFont { set; get; }
        public string Descricao { get; set; }

        public virtual PdfGrid RenderingGroupHeader(Document pdfDoc, PdfWriter pdfWriter, IList<CellData> newGroupInfo, IList<SummaryCellData> summaryData)
        {
            throw new NotImplementedException();
        }

        public virtual PdfGrid RenderingReportHeader(Document pdfDoc, PdfWriter pdfWriter, IList<SummaryCellData> summaryData)
        {
            throw new NotImplementedException();
        }
    }
}
