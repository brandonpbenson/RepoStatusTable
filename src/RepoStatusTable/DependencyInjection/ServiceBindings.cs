using Microsoft.Extensions.DependencyInjection;
using RepoStatusTable.CellProviders;
using RepoStatusTable.Facade;
using RepoStatusTable.Model;
using RepoStatusTable.Utilities;
using RepoStatusTable.View;
using RepoStatusTable.View.SpectreConsoleTableView;

namespace RepoStatusTable.DependencyInjection
{
	public partial class ServiceProviderBuilder
	{
		private readonly ServiceCollection _collection = new();

		public ServiceProvider Build()
		{
			return _collection.BuildServiceProvider();
		}

		public ServiceProviderBuilder ConfigureServices()
		{
			_collection.AddSingleton<IApplication, Application>();

			// Model and view
			_collection.AddSingleton<ITableModel, TableModel>();
			_collection.AddSingleton<ITableViewStrategy, SpectreConsoleTableViewLive>();
			_collection.AddSingleton<ITableViewStrategy, SpectreConsoleTableViewDefault>();
			_collection.AddSingleton<ITableView, TableViewProxy>();

			// Facades
			_collection.AddSingleton<IVcsFacade, GitFacade>();
			_collection.AddSingleton<IGitFacade, GitFacade>();
			_collection.AddSingleton<IFileSystemFacade, FileSystemFacade>();

			// Cell Providers
			_collection.AddSingleton<ICellProvider, DirectoryNameProvider>();
			_collection.AddSingleton<ICellProvider, GitBranchProvider>();
			_collection.AddSingleton<ICellProvider, GitStatusProvider>();
			_collection.AddSingleton<ICellProvider, FileContentProvider>();

			// Utilities
			_collection.AddSingleton<IReposDirectoryUtility, ReposDirectoryUtility>();

			return this;
		}
	}
}