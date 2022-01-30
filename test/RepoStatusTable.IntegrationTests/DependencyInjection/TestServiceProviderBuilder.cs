using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using RepoStatusTable.CellProviders;
using RepoStatusTable.DependencyInjection;
using RepoStatusTable.Facade;
using RepoStatusTable.IntegrationTests.FacadeMocks;
using RepoStatusTable.IntegrationTests.ViewAsserters;
using RepoStatusTable.Model;
using RepoStatusTable.Options;
using RepoStatusTable.Options.CellProvider;
using RepoStatusTable.Options.SpectreConsole;
using RepoStatusTable.Options.Validation;
using RepoStatusTable.Utilities;
using RepoStatusTable.Utilities.ReposDirectory;
using RepoStatusTable.View;
using RepoStatusTable.View.SpectreConsoleFigletHeadlineView;
using RepoStatusTable.View.SpectreConsoleTableView;

namespace RepoStatusTable.IntegrationTests.DependencyInjection;

public class TestServiceProviderBuilder
{
	private readonly ServiceCollection _collection = new();
	private readonly Dictionary<string, string> _optionsDict = new();

	public TestServiceProviderBuilder AddOrReplaceConfigKeyValuePair( string key, string value )
	{
		_optionsDict.Remove( key );
		_optionsDict.Add( key, value );
		return this;
	}

	public ServiceProvider Build()
	{
		AddOrReplaceConfigKeyValuePair( "Table:RenderMethod", "TableAsserter" );
		ConfigureOptions();
		ConfigureRegularServices();
		ConfigureTestServices();
		return _collection.BuildServiceProvider();
	}

	public TestServiceProviderBuilder DeactivateDefaultCellProviders()
	{
		AddOrReplaceConfigKeyValuePair( "CellProviders:DirectoryNameProvider:Enable", "false" );
		AddOrReplaceConfigKeyValuePair( "CellProviders:GitBranchProvider:Enable", "false" );
		AddOrReplaceConfigKeyValuePair( "CellProviders:GitStatusProvider:Enable", "false" );
		return this;
	}

	private void ConfigureRegularServices()
	{
		_collection.AddSingleton<IApplication, Application>();

		// Model and view
		_collection.AddSingleton<IHeadlineModel, HeadlineModel>();
		_collection.AddSingleton<IHeadlineView, HeadlineViewProxy>();

		_collection.AddSingleton<ITableModel, TableModel>();
		_collection.AddSingleton<ITableView, TableViewProxy>();

		// Cell Providers
		_collection.AddSingleton<ICellProviderManager, CellProviderManager>();
		_collection.AddSingleton<ICellProvider, DirectoryNameProvider>();
		_collection.AddSingleton<ICellProvider, GitBranchProvider>();
		_collection.AddSingleton<ICellProvider, GitStatusProvider>();
		_collection.AddSingleton<ICellProvider, FileContentProvider>();

		// Utilities
		_collection.AddSingleton<IReposDirectoryUtility, ReposDirectoryUtility>();
		_collection.AddSingleton<IReposOrderProvider, ReposOrderProvider>();

		// Factories
		_collection.AddSingleton<ISpectreFigletFactory, SpectreFigletFactory>();
		_collection.AddSingleton<ISpectreTableFactory, SpectreTableFactory>();
	}

	private void ConfigureTestServices()
	{
		// Facades
		_collection.AddSingleton<IVcsFacade, GitFacadeMock>();
		_collection.AddSingleton<IGitFacade, GitFacadeMock>();
		_collection.AddSingleton<IFileSystemFacade, FileSystemFacadeMock>();

		// Model and view
		_collection.AddSingleton<IHeadlineViewStrategy, HeadlineViewAsserter>();
		_collection.AddSingleton<ITableViewStrategy, TableViewAsserter>();
	}

	private void ConfigureOptions()
	{
		var configBuilder = new ConfigurationBuilder();
		configBuilder.Sources.Clear();
		configBuilder.AddInMemoryCollection( _optionsDict );
		var configRoot = configBuilder.Build();
		AddOptionsSections( configRoot );
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
}