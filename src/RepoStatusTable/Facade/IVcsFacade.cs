namespace RepoStatusTable.Facade
{
	public interface IVcsFacade
	{
		/// <summary>
		/// Check whether a directory contains a VCS repo
		/// </summary>
		/// <param name="path">Path to a directory which may contain a VCS repo</param>
		/// <returns>
		/// Whether the directory at <paramref name="path"/> contains a VCS repo
		/// </returns>
		public bool IsVcsRepo( string path );
	}
}