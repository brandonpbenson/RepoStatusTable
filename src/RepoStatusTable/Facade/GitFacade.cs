using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace RepoStatusTable.Facade
{
	public interface IGitFacade : IVcsFacade
	{
		string GetCurrentBranch( string path );

		IDictionary<string, int> GetStatus( string path );
	}

	public class GitFacade : IGitFacade
	{
		public bool IsVcsRepo( string path )
		{
			return IsGitRepo( path );
		}

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

		public IDictionary<string, int> GetStatus( string path )
		{
			using var repo = new Repository( path );
			var status = repo.RetrieveStatus( new StatusOptions() );
			return new Dictionary<string, int>
			{
				{"Added", status.Added.Count()},
				{"Modified", status.Modified.Count()},
				{"Missing", status.Missing.Count()},
				{"Untracked", status.Untracked.Count()}
			};
		}

		private static bool IsGitRepo( string path )
		{
			return Repository.IsValid( path );
		}
	}
}