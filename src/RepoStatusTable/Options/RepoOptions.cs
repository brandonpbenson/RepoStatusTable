using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RepoStatusTable.Options
{
	[SuppressMessage( "ReSharper", "CollectionNeverUpdated.Global" )]
	[SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" )]
	public class RepoOptions
	{
		/// <summary>
		///     List of directories that may contain multiple VSC repositories
		/// </summary>
		public IList<string> ReposRoot { get; set; } = new List<string>();

		/// <summary>
		///     List of paths to directories
		/// </summary>
		/// <remarks>
		///     Each directory must contain a VSC repository
		/// </remarks>
		public IList<string> RepoDirs { get; set; } = new List<string>();
	}
}