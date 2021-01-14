using System.Collections.Generic;

namespace Orion.Framework.Pdf.Reports.Core.Contracts
{
	/// <summary>
	/// Orion.Framework.Pdf.Reports's DataSource Contract
	/// </summary>
	public interface IDataSource
	{
		/// <summary>
		/// The data to render.
		/// </summary>
		/// <returns></returns>
		IEnumerable<IList<CellData>> Rows();
	}
}
