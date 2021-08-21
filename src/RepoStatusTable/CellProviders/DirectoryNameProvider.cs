using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.CellProviders
{
	public class DirectoryNameProvider : ICellProvider
	{
		private readonly IFileSystemFacade _fileSystemFacade;
		private readonly DirectoryNameProviderOptions _options;

		public DirectoryNameProvider( IOptions<DirectoryNameProviderOptions> options,
			IFileSystemFacade fileSystemFacade )
		{
			_fileSystemFacade = fileSystemFacade;
			_options = options.Value;
		}

		public string Heading => _options.Heading;

		public bool IsEnabled => _options.Enable;

		public int? Position => _options.Position;

		public Task<Cell> GetCell( string directory )
		{
			return Task.FromResult( new Cell( _fileSystemFacade.GetDirectoryName( directory ) ) );
		}
	}
}