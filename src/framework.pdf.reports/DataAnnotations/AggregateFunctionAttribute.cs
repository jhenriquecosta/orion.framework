using System;
using Orion.Framework.Pdf.Reports.Core.Contracts;

namespace Orion.Framework.Pdf.Reports.DataAnnotations
{
    /// <summary>
    /// Defining how a property of MainTableDataSource should be rendered as a column's cell.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class AggregateFunctionAttribute : Attribute
    {
        /// <summary>
        /// Defining how a property of MainTableDataSource should be rendered as a column's cell.
        /// </summary>
        /// <param name="aggregateFunction">Using predefined aggregate functions.</param>
        public AggregateFunctionAttribute(AggregateFunction aggregateFunction)
        {
            AggregateFunction = aggregateFunction;
        }

        /// <summary>
        /// Using predefined aggregate functions.
        /// </summary>
        public AggregateFunction AggregateFunction { get; private set; }
    }
}