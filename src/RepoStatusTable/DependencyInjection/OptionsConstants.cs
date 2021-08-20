namespace RepoStatusTable.DependencyInjection
{
	public static class OptionsConstants
	{
		public const string Repos = "Repos";
		public const string TableView = "TableView";

		public static class CellProviders
		{
			private const string Base = "CellProviders";
			public static readonly string DirectoryNameProvider = $"{Base}:DirectoryNameProvider";
			public static readonly string GitBranchProvider = $"{Base}:GitBranchProvider";
			public static readonly string GitStatusProvider = $"{Base}:GitStatusProvider";
		}
	}
}