using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RepoStatusTable.IntegrationTests.DependencyInjection;
using RepoStatusTable.IntegrationTests.FacadeMocks;
using RepoStatusTable.IntegrationTests.ViewAsserters;
using RepoStatusTable.View;

namespace RepoStatusTable.IntegrationTests.ViewTests.TableView;

public class TableViewDirectoryNameTests
{
	[Test]
	public async Task TableView_WithDirectoryNameProviderOnly_ShouldGetExpectedTable()
	{
		const string dir = "repo";
		const string path = $"/path/to/{dir}";

		var serviceProvider = new TestServiceProviderBuilder()
			.DeactivateDefaultCellProviders()
			.AddOrReplaceConfigKeyValuePair( "CellProviders:DirectoryNameProvider:Enable", "true" )
			.AddOrReplaceConfigKeyValuePair( "Repos:RepoDirs:0", path )
			.Build();

		new GitFacadeMockSetup( serviceProvider ).IsVscRepoReturnsForPath( path, true );
		new FileSystemFacadeMockSetup( serviceProvider )
			.DirectoryExistsReturnsForPath( path, true )
			.GetFullPathReturnsForPath( path, path )
			.GetDirectoryName( path, dir );

		new TableViewAsserterSetup( serviceProvider )
			.SetExpectedHeadings( new List<string> { "Name" } )
			.SetOneExpectedCell( dir );

		var table = serviceProvider.GetRequiredService<ITableView>();
		await table.RenderAsync();
	}
}