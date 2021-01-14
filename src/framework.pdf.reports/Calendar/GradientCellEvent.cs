using iTextSharp.text;
using iTextSharp.text.pdf;
using Orion.Framework.Pdf.Reports.Core.Helper;

namespace Orion.Framework.Pdf.Reports.Calendar
{
    /// <summary>
    /// Draw's CellLayout
    /// </summary>
    public class GradientCellEvent : IPdfPCellEvent
    {
        /// <summary>
        /// Gradient's Start Color. Set it to null to make it disappear.
        /// </summary>
        public BaseColor GradientStartColor { set; get; }

        /// <summary>
        /// Gradient's End Color. Set it to null to make it disappear.
        /// </summary>
        public BaseColor GradientEndColor { set; get; }

        /// <summary>
        /// Draw CellLayout
        /// </summary>
        public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
        {
            position.DrawGradientBackground(canvases, GradientStartColor, GradientEndColor);
        }
    }
}
