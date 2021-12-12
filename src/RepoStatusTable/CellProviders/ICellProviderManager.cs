using System.Collections.Generic;

namespace RepoStatusTable.CellProviders;

public interface ICellProviderManager
{
	IList<ICellProvider> GetOrderedListOfEnabledCellProviders();
}