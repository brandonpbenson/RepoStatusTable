using System.Collections.Generic;
using System.Linq;

namespace RepoStatusTable.CellProviders;

public class CellProviderManager : ICellProviderManager
{
	private readonly IEnumerable<ICellProvider> _cellProviders;

	public CellProviderManager( IEnumerable<ICellProvider> cellProviders )
	{
		_cellProviders = cellProviders;
	}

	public IList<ICellProvider> GetOrderedListOfEnabledCellProviders()
	{
		var enabled = GetEnabledCellProviders( _cellProviders ).ToList();
		return GetOrderedCellProviders( enabled ).ToList();
	}

	private static IEnumerable<ICellProvider> GetOrderedCellProviders( IList<ICellProvider> cellProviders )
	{
		var orderedCellProviders =
			cellProviders.Where( c => c.Position is not null ).OrderBy( c => c.Position ).ToList();
		return orderedCellProviders.Concat( cellProviders.Where( c => c.Position is null ) );
	}

	private static IEnumerable<ICellProvider> GetEnabledCellProviders( IEnumerable<ICellProvider> cellProviders )
	{
		return cellProviders.Where( p => p.IsEnabled );
	}
}