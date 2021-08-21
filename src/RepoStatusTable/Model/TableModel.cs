using System;
using System.Collections.Generic;
using System.Linq;
using RepoStatusTable.CellProviders;
using RepoStatusTable.Utilities;

namespace RepoStatusTable.Model
{
	public class TableModel : ITableModel
	{
		private readonly IEnumerable<ICellProvider> _cellProviders;
		private readonly IReposDirectoryUtility _reposDirectoryUtility;

		public TableModel( ICellProviderManager cellProviderManager, IReposDirectoryUtility reposDirectoryUtility )
		{
			_cellProviders = cellProviderManager.GetOrderedListOfEnabledCellProviders();
			_reposDirectoryUtility = reposDirectoryUtility;
		}

		public IEnumerable<string> GetHeadings() =>
			_cellProviders.Select( p => p.Heading );

		public async IAsyncEnumerable<IAsyncEnumerable<string>> GetTableAsync()
		{
			if ( !_cellProviders.Any() ) throw new ArgumentException( "No cell providers are enabled" );

			foreach ( var dir in _reposDirectoryUtility.GetRepoDirectories() )
			{
				yield return GetRowsAsync( dir );
			}
		}

		private async IAsyncEnumerable<string> GetRowsAsync( string path )
		{
			foreach ( var provider in _cellProviders )
			{
				var cell = await provider.GetCell( path );
				yield return cell.Content;
			}
		}
	}
}