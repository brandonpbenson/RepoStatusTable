using System.Collections.Generic;

namespace RepoStatusTable.Utilities;

public interface IReposDirectoryUtility
{
	/// <summary>
	///     Find all directory paths from the configuration which contain a VCS repository
	/// </summary>
	/// <returns>
	///     All configured directory paths that contain a VCS repository
	/// </returns>
	IEnumerable<string> GetRepoDirectories();
}