using RepoStatusTable.Facade;
using RepoStatusTable.Options;

namespace RepoStatusTable.Utilities;

public class ReposDirectoryUtility : IReposDirectoryUtility
{
	private readonly IFileSystemFacade _fileSystemFacade;
	private readonly RepoOptions _repoOptions;
	private readonly IVcsFacade _vcsFacade;

	public ReposDirectoryUtility(
		IOptions<RepoOptions> repoOptions,
		IFileSystemFacade fileSystemFacade,
		IVcsFacade vcsFacade )
	{
		_fileSystemFacade = fileSystemFacade;
		_vcsFacade = vcsFacade;
		_repoOptions = repoOptions.Value;
	}

	/// <inheritdoc />
	public IEnumerable<string> GetRepoDirectories()
	{
		var repos = new List<string>();
		repos.AddRange( GetAllRepoDirs() );
		repos.AddRange( GetAllDirsInRoots() );

		return repos.Where( d =>
			_fileSystemFacade.DirectoryExists( d )
			&& _vcsFacade.IsVcsRepo( d )
		);
	}

	private IEnumerable<string> GetAllRepoDirs()
	{
		return _repoOptions.RepoDirs.Select( _fileSystemFacade.GetFullPath );
	}

	private IEnumerable<string> GetAllDirsInRoots()
	{
		var rootDirs = _repoOptions.RepoRoots;
		return rootDirs
			.Select( _fileSystemFacade.GetDirectories )
			.SelectMany( p => p );
	}
}