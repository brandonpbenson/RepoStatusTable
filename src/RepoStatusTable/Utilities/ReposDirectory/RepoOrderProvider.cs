using RepoStatusTable.Facade;
using RepoStatusTable.Options;

namespace RepoStatusTable.Utilities.ReposDirectory;

public interface IReposOrderProvider
{
	public IEnumerable<string> OrderAccordingToOptions( IEnumerable<string> directories );
}

public class ReposOrderProvider : IReposOrderProvider
{
	private readonly IFileSystemFacade _fileSystemFacade;
	private readonly IOptions<RepoOptions> _repoOptions;

	public ReposOrderProvider( IOptions<RepoOptions> repoOptions, IFileSystemFacade fileSystemFacade )
	{
		_repoOptions = repoOptions;
		_fileSystemFacade = fileSystemFacade;
	}

	public IEnumerable<string> OrderAccordingToOptions( IEnumerable<string> directories )
	{
		var orderFunc = GetCompareFunc();

		return _repoOptions.Value.Order switch
		{
			RepoOrder.Ascending => directories.OrderBy( orderFunc ),
			RepoOrder.Descending => directories.OrderByDescending( orderFunc ),
			_ => throw new ArgumentOutOfRangeException()
		};
	}

	private Func<string, IComparable> GetCompareFunc()
	{
		return _repoOptions.Value.OrderBy switch
		{
			RepoOrderBy.Alphabetically => d => d,
			RepoOrderBy.LastModified => d => _fileSystemFacade.GetLastWriteTime( d ),
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}