﻿using System.IO;
using Orion.Framework.Pdf.Reports.Core.Helper;

namespace Orion.Framework.Pdf.Reports.FluentInterface
{
    /// <summary>
    /// Pdf RptFile Builder Class.
    /// </summary>
    public class FileBuilder
    {
        readonly PdfReport _pdfReport;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="pdfReport"></param>
        public FileBuilder(PdfReport pdfReport)
        {
            _pdfReport = pdfReport;
        }

        /// <summary>
        /// Sets produced PDF file's path and name.
        /// It can be null if you are using an in memory stream.
        /// </summary>
        /// <param name="fileName">produced PDF file's path and name</param>
        public void AsPdfFile(string fileName)
        {
            fileName.CheckDirectoryExists();
            _pdfReport.DataBuilder.SetFileName(fileName);
        }

        /// <summary>
        /// Sets the PDF file's stream.
        /// It can be null. In this case a new FileStream will be used automatically and you need to provide the FileName.
        /// </summary>
        /// <param name="pdfStreamOutput">the PDF file's stream</param>
        /// <param name="closeStream">
        /// Close the document by closing the underlying stream. Its default value is true.
        /// If you want to access the PDF stream after it has been created, set it to false.
        /// </param>
        public void AsPdfStream(Stream pdfStreamOutput, bool closeStream = true)
        {
            _pdfReport.DataBuilder.SetStreamOutput(pdfStreamOutput);
            _pdfReport.DataBuilder.CloseStream = closeStream;
        }

        /// <summary>
        /// Sets the output PDF file's byte array.
        /// </summary>
        /// <param name="outputPdfBytes">output data</param>
        public void AsByteArray(out byte[] outputPdfBytes)
        {
            outputPdfBytes = _pdfReport.GenerateAsByteArray();
        }
    }
}