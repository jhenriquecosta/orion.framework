using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.DataLayer.NHibernate.Loquacious.Convention.Utils.Helpers
{
    /// <summary>
    /// Provides methods to filter items
    /// </summary>
    /// <typeparam name="TItem">The type of the item to filter</typeparam>
    internal class FilterHelper<TItem>
    {
        #region Public Properties

        /// <summary>
        /// The filters used to include items
        /// </summary>
        public IList<Func<TItem, bool>> IncludeFilters { get; private set; }

        /// <summary>
        /// The filters used to exclude items
        /// </summary>
        public IList<Func<TItem, bool>> ExcludeFilters { get; private set; }

        /// <summary>
        /// Gets the list of included items
        /// </summary>
        public IList<TItem> IncludedItems { get; private set; }

        /// <summary>
        /// Gets the list of excluded items
        /// </summary>
        public IList<TItem> ExcludedItems { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyFilterHelper"/> class
        /// </summary>
        public FilterHelper()
        {
            this.IncludeFilters = new List<Func<TItem, bool>>();
            this.ExcludeFilters = new List<Func<TItem, bool>>();
            this.IncludedItems = new List<TItem>();
            this.ExcludedItems = new List<TItem>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Applies the filters to the a given source list
        /// </summary>
        /// <param name="sourceList">The source list</param>
        public IList<TItem> ApplyFilters(IList<TItem> sourceList)
        {
            IList<TItem> result = IncludeFilters.Any() 
                ? sourceList.Where(a => IncludeFilters.All(f => f.Invoke(a)))
                    .Except(sourceList.Where(a => ExcludeFilters.Any(f => f.Invoke(a))))
                    .ToList()
                : new List<TItem>();
            return result
                .Concat(IncludedItems)
                .Except(ExcludedItems)
                .ToList();
        }

        #endregion

    }
}
