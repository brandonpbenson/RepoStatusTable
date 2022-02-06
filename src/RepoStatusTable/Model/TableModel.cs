using RepoStatusTable.CellProviders;
using RepoStatusTable.Options;
using RepoStatusTable.Utilities;

namespace RepoStatusTable.Model;

public class TableModel : ITableModel
{
	private readonly IEnumerable<ICellProvider> _cellProviders;
	private readonly IReposDirectoryUtility _reposDirectoryUtility;
	private readonly TableOptions _tableOptions;

	public TableModel( ICellProviderManager cellProviderManager, IReposDirectoryUtility reposDirectoryUtility,
		IOptions<TableOptions> tableOptions )
	{
		_cellProviders = cellProviderManager.GetOrderedListOfEnabledCellProviders();
		_reposDirectoryUtility = reposDirectoryUtility;
		_tableOptions = tableOptions.Value;
	}

	public IEnumerable<string> GetHeadings()
	{
		return _cellProviders.Select( p => p.Heading );
	}

	public async IAsyncEnumerable<IEnumerable<string>> GetTableAsync()
	{
		if ( !_cellProviders.Any() )
		{
			throw new ArgumentException( "No cell providers are enabled" );
		}

		foreach ( var dir in _reposDirectoryUtility.GetRepoDirectories() )
		{
			var row = ( await GetRowAsync( dir ) ).ToList();
			if ( _tableOptions.OnlyShowChanged == false || row.Any( r => r.IsChanged ) )
			{
				yield return row.Select( r => r.Content );
			}
		}
	}

	private async Task<IEnumerable<Cell>> GetRowAsync( string path )
	{
		var cellTasks = _cellProviders.Select( p => p.GetCell( path ) );
		return await Task.WhenAll( cellTasks );
	}
}