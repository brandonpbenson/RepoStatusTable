using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using RepoStatusTable.CellProviders;
using RepoStatusTable.Facade;
using RepoStatusTable.Model;
using RepoStatusTable.Options;
using RepoStatusTable.Options.CellProvider;
using RepoStatusTable.Options.Validation;
using RepoStatusTable.Utilities;
using RepoStatusTable.View;

namespace RepoStatusTable
{
	public static class Bindings
	{
		public static ServiceProvider CreateBindings()
		{
			return new ServiceProviderBuilder()
				.ConfigureOptions()
				.ConfigureServices()
				.Build();
		}
	}


	public class ServiceProviderBuilder
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
			_collection.AddSingleton<ITableView, SpectreConsoleTableView>();

			// Facades
			_collection.AddSingleton<IVcsFacade, GitFacade>();
			_collection.AddSingleton<IGitFacade, GitFacade>();
			_collection.AddSingleton<IFileSystemFacade, FileSystemFacade>();

			// Cell Providers
			_collection.AddSingleton<ICellProvider, DirectoryNameProvider>();
			_collection.AddSingleton<ICellProvider, GitBranchProvider>();
			_collection.AddSingleton<ICellProvider, GitStatusProvider>();

			// Utilities
			_collection.AddSingleton<IReposDirectoryUtility, ReposDirectoryUtility>();

			return this;
		}


		public ServiceProviderBuilder ConfigureOptions()
		{
			var configurationRoot = ConfigureConfiguration();
			_collection.Configure<RepoOptions>( configurationRoot.GetSection( "Repos" ) );
			_collection.Configure<DirectoryNameProviderOptions>(
				configurationRoot.GetSection( "CellProviders:DirectoryNameProvider" ) );
			_collection.Configure<GitBranchProviderOptions>(
				configurationRoot.GetSection( "CellProviders:GitBranchProvider" ) );
			_collection.Configure<GitStatusProviderOptions>(
				configurationRoot.GetSection( "CellProviders:GitStatusProvider" ) );

			_collection.TryAddEnumerable(
				ServiceDescriptor.Singleton
					<IValidateOptions<RepoOptions>, RepoOptionsValidator>() );

			return this;
		}

		private static IConfigurationRoot ConfigureConfiguration()
		{
			var configurationBuilder = new ConfigurationBuilder();

			var homeDir = Environment.GetFolderPath( Environment.SpecialFolder.UserProfile );
			var homeDirConfig = Path.Join( homeDir, "RepoStatusTableConfig.json" );

			var workingDir = Directory.GetCurrentDirectory();
			var workingDirConfig = Path.Join( workingDir, "RepoStatusTableConfig.json" );

			configurationBuilder.Sources.Clear();
			configurationBuilder
				.AddJsonFile( "config.json", true )
				.AddJsonFile( homeDirConfig, true )
				.AddJsonFile( workingDirConfig, true );

			var configRoot = configurationBuilder.Build();

			if ( configRoot.GetChildren().ToList().Count == 0 )
			{
				throw new ArgumentException( "No valid config given" );
			}

			return configRoot;
		}
	}
}