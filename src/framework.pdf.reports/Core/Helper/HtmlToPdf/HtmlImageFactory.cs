﻿using System;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;

namespace Orion.Framework.Pdf.Reports.Core.Helper.HtmlToPdf
{
    /// <summary>
    /// Custom HTML Image provider.
    /// </summary>
    public class HtmlImageFactory : IImageProvider
    {
        readonly Image _image;
        /// <summary>
        /// ctor.
        /// </summary>
        public HtmlImageFactory(Image image)
        {
            _image = image;
        }

        /// <summary>
        /// Returns a PDF image
        /// </summary>
        public Image GetImage(string src, Hashtable h, ChainedProperties cprops, IDocListener doc)
        {
            if(src.Equals("TotalPagesNumber", StringComparison.OrdinalIgnoreCase))
            {
                return _image;
            }
            return Image.GetInstance(src);
        }
    }
}