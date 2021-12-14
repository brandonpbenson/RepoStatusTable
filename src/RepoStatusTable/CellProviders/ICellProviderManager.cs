namespace RepoStatusTable.CellProviders;

public interface ICellProviderManager
{
	IList<ICellProvider> GetOrderedListOfEnabledCellProviders();
}