using System;

namespace Orion.Framework.Pdf.Reports.DataAnnotations
{
    /// <summary>
    /// Defining how a property of MainTableDataSource should be rendered as a column's cell.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class WidthAttribute : Attribute
    {
        /// <summary>
        /// Defining how a property of MainTableDataSource should be rendered as a column's cell.
        /// </summary>
        /// <param name="width">The column's width according to the Orion.Framework.Pdf.ReportsPageSetup.MainTableColumnsWidthsType value.</param>
        public WidthAttribute(float width)
        {
            Width = width;
        }

        /// <summary>
        /// The column's width according to the Orion.Framework.Pdf.ReportsPageSetup.MainTableColumnsWidthsType value.
        /// </summary>
        public float Width { private set; get; }
    }
}