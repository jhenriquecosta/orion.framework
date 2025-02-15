using Orion.Framework.Dependency;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Orion.Framework.DataLayer.UnitOfWorks.Contracts
{
	/// <summary>
	///     Used to get/set default options for a unit of work.
	/// </summary>
	public interface IUnitOfWorkDefaultOptions
	{
		/// <summary>
		///     Scope option.
		/// </summary>
		TransactionScopeOption Scope { get; set; }

		/// <summary>
		///     Should unit of works be transactional.
		///     Default: true.
		/// </summary>
		bool IsTransactional { get; set; }

		/// <summary>
		///     Gets/sets a timeout value for unit of works.
		/// </summary>
		TimeSpan? Timeout { get; set; }

		/// <summary>
		///     Gets/sets isolation level of transaction.
		///     This is used if <see cref="IsTransactional" /> is true.
		/// </summary>
		IsolationLevel? IsolationLevel { get; set; }

		/// <summary>
		///     Gets list of all data filter configurations.
		/// </summary>
		IReadOnlyList<DataFilterConfiguration> Filters { get; }

		/// <summary>
		///     A list of selectors to determine conventional Unit Of Work classes.
		/// </summary>
		List<Func<Type, bool>> ConventionalUowSelectors { get; }

		/// <summary>
		///     Enables or disables the EF lazy load, not supported in EF Core.
		/// </summary>
		bool IsLazyLoadEnabled { get; set; }

		/// <summary>
		///     Registers a data filter to unit of work system.
		/// </summary>
		/// <param name="filterName">Name of the filter.</param>
		/// <param name="isEnabledByDefault">Is filter enabled by default.</param>
		void RegisterFilter(string filterName, bool isEnabledByDefault);

		/// <summary>
		///     Overrides a data filter definition.
		/// </summary>
		/// <param name="filterName">Name of the filter.</param>
		/// <param name="isEnabledByDefault">Is filter enabled by default.</param>
		void OverrideFilter(string filterName, bool isEnabledByDefault);
	}
}
