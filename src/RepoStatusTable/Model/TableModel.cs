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

		public TableModel( IEnumerable<ICellProvider> cellProviders, IReposDirectoryUtility reposDirectoryUtility )
		{
			_cellProviders = cellProviders;
			_reposDirectoryUtility = reposDirectoryUtility;
		}

		public IEnumerable<string> GetHeadings() =>
			from provider in _cellProviders select provider.Heading;

		public async IAsyncEnumerable<IAsyncEnumerable<string>> GetTableAsync()
		{
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