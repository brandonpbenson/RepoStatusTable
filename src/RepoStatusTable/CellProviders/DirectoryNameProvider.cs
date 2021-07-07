using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using RepoStatusTable.Facade;

namespace RepoStatusTable.CellProviders
{
	public class DirectoryNameProvider : ICellProvider
	{
		private readonly IFileSystemFacade _fileSystemFacade;

		public DirectoryNameProvider( [NotNull] IFileSystemFacade filesystemFacade )
		{
			_fileSystemFacade = filesystemFacade;
		}

		public string Heading => "Name";

		public Task<Cell> GetCell( string directory )
		{
			return Task.FromResult( new Cell( _fileSystemFacade.GetDirectoryName( directory ) ) );
		}
	}
}