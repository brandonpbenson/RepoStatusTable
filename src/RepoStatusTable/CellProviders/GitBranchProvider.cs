using System.Threading.Tasks;
using RepoStatusTable.Facade;

namespace RepoStatusTable.CellProviders
{
	public class GitBranchProvider : ICellProvider
	{
		private readonly IGitFacade _gitFacade;

		public GitBranchProvider( IGitFacade gitFacade )
		{
			_gitFacade = gitFacade;
		}

		public string Heading => "Branch";

		public Task<Cell> GetCell( string path )
		{
			var branchName = _gitFacade.GetCurrentBranch( path );

			return Task.FromResult( new Cell(
				branchName
			) );
		}
	}
}