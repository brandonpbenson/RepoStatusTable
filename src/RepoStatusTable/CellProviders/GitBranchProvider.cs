using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.CellProviders
{
	public class GitBranchProvider : ICellProvider
	{
		private readonly IGitFacade _gitFacade;
		private readonly GitBranchProviderOptions _options;

		public GitBranchProvider( IOptions<GitBranchProviderOptions> options, IGitFacade gitFacade )
		{
			_options = options.Value;
			_gitFacade = gitFacade;
		}

		public string Heading => _options.Heading ?? "Branch";

		public bool IsEnabled => _options.Enable;

		public Task<Cell> GetCell( string path )
		{
			var branchName = _gitFacade.GetCurrentBranch( path );

			return Task.FromResult( new Cell(
				branchName
			) );
		}
	}
}