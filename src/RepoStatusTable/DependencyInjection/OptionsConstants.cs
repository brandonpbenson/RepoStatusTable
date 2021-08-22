namespace RepoStatusTable.DependencyInjection
{
	public static class OptionsConstants
	{
		public const string Repos = "Repos";
		public const string TableView = "TableView";
		public static readonly string SpectreTable = $"{TableView}:SpectreTable";

		public static class CellProviders
		{
			private const string Base = "CellProviders";
			public static readonly string DirectoryNameProvider = $"{Base}:DirectoryNameProvider";
			public static readonly string GitBranchProvider = $"{Base}:GitBranchProvider";
			public static readonly string GitStatusProvider = $"{Base}:GitStatusProvider";
			public static readonly string FileContentProvider = $"{Base}:FileContentProvider";
		}
	}
}