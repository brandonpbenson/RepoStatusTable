namespace RepoStatusTable.Options.CellProvider
{
	public class GitBranchProviderOptions : ICellProviderOptions
	{
		/// <inheritdoc />
		public bool Enable { get; set; } = true;

		/// <inheritdoc />
		public int? Position { get; set; }

		/// <inheritdoc />
		public string? Heading { get; set; }
	}
}