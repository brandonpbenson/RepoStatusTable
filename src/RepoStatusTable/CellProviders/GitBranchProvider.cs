using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.CellProviders;

public class GitBranchProvider : ICellProvider
{
	private readonly IGitFacade _gitFacade;
	private readonly GitBranchProviderOptions _options;

	public GitBranchProvider( IOptions<GitBranchProviderOptions> options, IGitFacade gitFacade )
	{
		_options = options.Value;
		_gitFacade = gitFacade;
	}

	public string Heading => _options.Heading;

	public bool IsEnabled => _options.Enable;

	public int? Position => _options.Position;

	public Task<Cell> GetCell( string path )
	{
		var branchName = _gitFacade.GetCurrentBranch( path );

		return Task.FromResult( new Cell(
			branchName,
			IsChanged( branchName )
		) );
	}

	private bool IsChanged( string branch )
	{
		return !_options.DefaultBranches?.Contains( branch ) ?? true;
	}
}