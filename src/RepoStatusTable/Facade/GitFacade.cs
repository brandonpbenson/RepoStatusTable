using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace RepoStatusTable.Facade;

/// <summary>
///     Facade for the LibGit2Sharp library.
///     Handles all calls that access Git functionality.
/// </summary>
public interface IGitFacade : IVcsFacade
{
	/// <summary>
	///     Get the current branch of the repository head
	/// </summary>
	/// <param name="path">Path to a directory which may contain a Git repo</param>
	/// <returns>
	///     Branch name of the current repository head
	/// </returns>
	string GetCurrentBranch( string path );

	/// <summary>
	///     Get the status of the repository at <paramref name="path" />
	/// </summary>
	/// <param name="path">Path to a Git repo</param>
	/// <returns>
	///     The Git status of the Git repo at <paramref name="path" /> as dictionary:
	///     Added, modified, missing and untracked files.
	/// </returns>
	IDictionary<string, int> GetStatus( string path );
}

public class GitFacade : IGitFacade
{
	/// <inheritdoc />
	/// <see cref="IsGitRepo" />
	public bool IsVcsRepo( string path )
	{
		return IsGitRepo( path );
	}


	/// <inheritdoc />
	public string GetCurrentBranch( string path )
	{
		using var repo = new Repository( path );
		var branches = repo.Branches;
		var currentBranch = branches
			.Where( b => b.IsCurrentRepositoryHead )
			.Select( b => b.FriendlyName )
			.FirstOrDefault();
		return currentBranch ?? "";
	}

	/// <inheritdoc />
	public IDictionary<string, int> GetStatus( string path )
	{
		using var repo = new Repository( path );
		var status = repo.RetrieveStatus( new StatusOptions() );
		return new Dictionary<string, int>
		{
			{ "Added", status.Added.Count() },
			{ "Modified", status.Modified.Count() },
			{ "Missing", status.Missing.Count() },
			{ "Untracked", status.Untracked.Count() }
		};
	}

	/// <summary>
	///     Check whether a directory contains a Git repo
	/// </summary>
	/// <param name="path">Path to a directory which may contain a Git repo</param>
	/// <returns>
	///     Whether the directory at <paramref name="path" /> contains a Git repo
	/// </returns>
	private static bool IsGitRepo( string path )
	{
		return Repository.IsValid( path );
	}
}