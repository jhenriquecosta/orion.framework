﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Orion.Framework.Pdf.Reports.Core.Contracts;

namespace Orion.Framework.Pdf.Reports.DataSources
{
    /// <summary>
    /// XML DataSource class
    /// </summary>
    public class XmlDataSource : IDataSource
    {
        #region Fields (4)

        private readonly string _descendantsXPath;
        private readonly string _xml;
        private readonly IList<string> _xPathList;
        private int _index;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Converts the XML documents data to an IEnumerable of Pdf Cells Data
        /// </summary>
        /// <param name="xml">XML document's content</param>
        /// <param name="descendantsXPathSelect">Descendants XPath</param>
        /// <param name="itemsXPathList">XPath list of the required items</param>
        public XmlDataSource(string xml, string descendantsXPathSelect, IList<string> itemsXPathList)
        {
            _xml = xml;
            _xPathList = itemsXPathList;
            _descendantsXPath = descendantsXPathSelect;
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1)

        /// <summary>
        /// The data to render.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IList<CellData>> Rows()
        {
            using (var reader = new StringReader(_xml))
            {
                var xDoc = XDocument.Load(reader);
                var list = xDoc.XPathSelectElements(_descendantsXPath);

                foreach (var el in list)
                {
                    var pdfCellData = new List<CellData>();
                    foreach (var item in _xPathList)
                    {
                        var value = string.Empty;

                        var dataEval = (IEnumerable<object>)el.XPathEvaluate(item);
                        var data = dataEval.FirstOrDefault();
                        if (data is XAttribute attribute)
                        {
                            value = attribute.Value;
                        }
                        else
                        {
                            if (data is XElement element)
                            {
                                value = element.Value;
                            }
                        }

                        pdfCellData.Add(new CellData
                        {
                            PropertyName = item,
                            PropertyValue = value,
                            PropertyIndex = _index++,
                            PropertyType = value?.GetType()
                        });
                    }
                    yield return pdfCellData;
                }
            }
        }

        #endregion Methods
    }
}
