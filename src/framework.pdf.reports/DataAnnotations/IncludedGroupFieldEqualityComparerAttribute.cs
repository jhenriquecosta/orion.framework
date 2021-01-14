﻿using System;

namespace Orion.Framework.Pdf.Reports.DataAnnotations
{
    /// <summary>
    /// Defining how a property of MainTableDataSource should be rendered as a column's cell.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class IncludedGroupFieldEqualityComparerAttribute : Attribute
    {
        /// <summary>
        /// Defining how a property of MainTableDataSource should be rendered as a column's cell.
        /// </summary>
        /// <param name="propertyName">Name of the corresponding column's property.</param>
        public IncludedGroupFieldEqualityComparerAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        /// <summary>
        /// Name of the corresponding column's property.
        /// </summary>
        public string PropertyName { private set; get; }
    }
}