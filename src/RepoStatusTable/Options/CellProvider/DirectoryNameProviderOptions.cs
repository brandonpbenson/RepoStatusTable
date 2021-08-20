namespace RepoStatusTable.Options.CellProvider
{
	public class DirectoryNameProviderOptions : ICellProviderOptions
	{
		/// <inheritdoc />
		public bool Enable { get; set; } = true;

		/// <inheritdoc />
		public string? Heading { get; set; }
	}
}