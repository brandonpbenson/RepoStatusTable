using RepoStatusTable.Facade;
using RepoStatusTable.Options;

namespace RepoStatusTable.Utilities.ReposDirectory;

public class ReposDirectoryUtility : IReposDirectoryUtility
{
	private readonly IFileSystemFacade _fileSystemFacade;
	private readonly RepoOptions _repoOptions;
	private readonly IReposOrderProvider _reposOrderProvider;
	private readonly IVcsFacade _vcsFacade;

	public ReposDirectoryUtility(
		IOptions<RepoOptions> repoOptions,
		IFileSystemFacade fileSystemFacade,
		IVcsFacade vcsFacade,
		IReposOrderProvider reposOrderProvider )
	{
		_fileSystemFacade = fileSystemFacade;
		_vcsFacade = vcsFacade;
		_reposOrderProvider = reposOrderProvider;
		_repoOptions = repoOptions.Value;
	}

	/// <inheritdoc />
	public IEnumerable<string> GetRepoDirectories()
	{
		var repos = new List<string>();
		repos.AddRange( GetAllRepoDirs() );
		repos.AddRange( GetAllDirsInRoots() );

		var directories = repos.Where( d => _vcsFacade.IsVcsRepo( d ) );
		return _reposOrderProvider.OrderAccordingToOptions( directories );
	}

	private IEnumerable<string> GetAllRepoDirs()
	{
		return _repoOptions.RepoDirs
			.Where( _fileSystemFacade.DirectoryExists )
			.Select( _fileSystemFacade.GetFullPath );
	}

	private IEnumerable<string> GetAllDirsInRoots()
	{
		var rootDirs = _repoOptions.RepoRoots;
		return rootDirs
			.Select( _fileSystemFacade.GetDirectories )
			.SelectMany( p => p );
	}
}