namespace RepoStatusTable.Options.CellProvider;

[SuppressMessage( "ReSharper", "UnusedAutoPropertyAccessor.Global" )]
[SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" )]
public class GitBranchProviderOptions : ICellProviderOptions
{
	/// <summary>
	///     List of branches that are considered as not changed
	/// </summary>
	public IEnumerable<string> DefaultBranches { get; set; } = new List<string>();

	/// <inheritdoc />
	public bool Enable { get; set; } = true;

	/// <inheritdoc />
	public int? Position { get; set; }

	/// <inheritdoc />
	public string Heading { get; set; } = "Branch";
}