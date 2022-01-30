namespace RepoStatusTable.Options;

[SuppressMessage( "ReSharper", "UnusedAutoPropertyAccessor.Global" )]
public class RepoOptions
{
	/// <summary>
	///     List of directories that may contain multiple VSC repositories
	/// </summary>
	public IList<string> RepoRoots { get; set; } = new List<string>();

	/// <summary>
	///     List of paths to directories
	/// </summary>
	/// <remarks>
	///     Each directory must contain a VSC repository
	/// </remarks>
	public IList<string> RepoDirs { get; set; } = new List<string>();

	/// <summary>
	///     Order according to which the repos should be sorted in the table
	/// </summary>
	/// <remarks>
	///     Can be either Ascending or Descending
	/// </remarks>
	public RepoOrder Order { get; set; }

	/// <summary>
	///     Order aspect according to which the repos should be sorted in the table
	/// </summary>
	/// <remarks>
	///     <list type="table">
	///         <item>
	///             <term>Alphabetically</term>
	///             <description>Order repos according to the alphabet</description>
	///         </item>
	///         <item>
	///             <term>LastModified</term>
	///             <description>Order repos depending to when they were last modified</description>
	///         </item>
	///     </list>
	/// </remarks>
	public RepoOrderBy OrderBy { get; set; }
}