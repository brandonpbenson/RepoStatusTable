using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using RepoStatusTable.Options;
using RepoStatusTable.Options.CellProvider;
using RepoStatusTable.Options.Validation;

namespace RepoStatusTable.DependencyInjection
{
	public partial class ServiceProviderBuilder
	{
		public ServiceProviderBuilder ConfigureOptions()
		{
			var configurationRoot = ConfigureConfiguration();

			AddOptionsSections( configurationRoot );
			AddValidation();

			return this;
		}

		private void AddOptionsSections( IConfiguration configurationRoot )
		{
			_collection.Configure<RepoOptions>( configurationRoot.GetSection( OptionsConstants.Repos ) );
			_collection.Configure<TableViewOptions>( configurationRoot.GetSection( OptionsConstants.TableView ) );

			// Cell providers
			_collection.Configure<DirectoryNameProviderOptions>(
				configurationRoot.GetSection( OptionsConstants.CellProviders.DirectoryNameProvider ) );
			_collection.Configure<GitBranchProviderOptions>(
				configurationRoot.GetSection( OptionsConstants.CellProviders.GitBranchProvider ) );
			_collection.Configure<GitStatusProviderOptions>(
				configurationRoot.GetSection( OptionsConstants.CellProviders.GitStatusProvider ) );
			_collection.Configure<FileContentProviderOptions>(
				configurationRoot.GetSection( OptionsConstants.CellProviders.FileContentProvider ) );
		}

		private void AddValidation()
		{
			_collection.TryAddEnumerable( ServiceDescriptor
				.Singleton<IValidateOptions<RepoOptions>, RepoOptionsValidator>() );
			_collection.TryAddEnumerable( ServiceDescriptor
				.Singleton<IValidateOptions<FileContentProviderOptions>, FileContentProviderOptionsValidator>() );
		}

		private static IConfigurationRoot ConfigureConfiguration()
		{
			var configurationBuilder = new ConfigurationBuilder();

			configurationBuilder.Sources.Clear();
			configurationBuilder
				.AddJsonFile( "config.json", true )
				.AddJsonFile( GetAppDataDirConfigPath(), true )
				.AddJsonFile( GetWorkingDirConfigPath(), true );

			var configRoot = configurationBuilder.Build();

			CheckConfigGiven( configRoot );

			return configRoot;
		}

		private static void CheckConfigGiven( IConfiguration configRoot )
		{
			if ( configRoot.GetChildren().ToList().Any() ) return;

			throw new ArgumentException( "No valid config given" );
		}

		private static string GetWorkingDirConfigPath()
		{
			var workingDir = Directory.GetCurrentDirectory();
			return Path.Join( workingDir, "rstconfig.json" );
		}

		private static string GetAppDataDirConfigPath()
		{
			var appdataDir = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
			return Path.Join( appdataDir, "RepoStatusTable", "config.json" );
		}
	}
}