using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using RepoStatusTable.Options;
using RepoStatusTable.Options.CellProvider;
using RepoStatusTable.Options.SpectreConsole;
using RepoStatusTable.Options.Validation;

namespace RepoStatusTable.DependencyInjection;

public partial class ServiceProviderBuilder
{
	public ServiceProviderBuilder ConfigureOptions()
	{
		var configurationRoot = ConfigureConfiguration();
		AddOptionsSections( configurationRoot );
		return this;
	}

	private void AddOptionsSections( IConfiguration configurationRoot )
	{
		// VCS Repos
		_collection.AddOptions<RepoOptions>()
			.Bind( configurationRoot.GetSection( OptionsConstants.Repos ) );
		_collection.TryAddEnumerable( ServiceDescriptor
			.Singleton<IValidateOptions<RepoOptions>, RepoOptionsValidator>() );

		// Output Model
		_collection.AddOptions<HeadlineOptions>()
			.Bind( configurationRoot.GetSection( OptionsConstants.Headline ) )
			.ValidateDataAnnotations();
		_collection.AddOptions<TableViewOptions>()
			.Bind( configurationRoot.GetSection( OptionsConstants.Table ) );

		// Spectre Console
		_collection.AddOptions<SpectreFigletOptions>()
			.Bind( configurationRoot.GetSection( OptionsConstants.SpectreFiglet ) );
		_collection.AddOptions<SpectreTableOptions>()
			.Bind( configurationRoot.GetSection( OptionsConstants.SpectreTable ) );

		// Cell providers
		_collection.AddOptions<DirectoryNameProviderOptions>()
			.Bind( configurationRoot.GetSection( OptionsConstants.CellProviders.DirectoryNameProvider ) );
		_collection.AddOptions<GitBranchProviderOptions>()
			.Bind( configurationRoot.GetSection( OptionsConstants.CellProviders.GitBranchProvider ) );
		_collection.AddOptions<GitStatusProviderOptions>()
			.Bind( configurationRoot.GetSection( OptionsConstants.CellProviders.GitStatusProvider ) );
		_collection.AddOptions<FileContentProviderOptions>()
			.Bind( configurationRoot.GetSection( OptionsConstants.CellProviders.FileContentProvider ) )
			.ValidateDataAnnotations();
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
		if ( configRoot.GetChildren().ToList().Any() )
		{
			return;
		}

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