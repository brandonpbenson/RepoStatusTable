using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RepoStatusTable.IntegrationTests.DependencyInjection;
using RepoStatusTable.IntegrationTests.FacadeMocks;
using RepoStatusTable.IntegrationTests.ViewAsserters;
using RepoStatusTable.View;

namespace RepoStatusTable.IntegrationTests.ViewTests.TableView;

public class TableViewGitStatusTests
{
	[Test]
	public async Task TableView_WithGitStatusProviderOnly_ShouldGetExpectedTable()
	{
		const int added = 1;
		const int modified = 2;
		const int missing = 3;
		const int untracked = 4;

		const string dir = "repo";
		const string path = $"/path/to/{dir}";

		var serviceProvider = new TestServiceProviderBuilder()
			.DeactivateDefaultCellProviders()
			.AddOrReplaceConfigKeyValuePair( "CellProviders:GitStatusProvider:Enable", "true" )
			.AddOrReplaceConfigKeyValuePair( "Repos:RepoDirs:0", path )
			.Build();

		new FileSystemFacadeMockSetup( serviceProvider )
			.DirectoryExistsReturnsForPath( path, true )
			.GetFullPathReturnsForPath( path, path );

		new GitFacadeMockSetup( serviceProvider )
			.IsVscRepoReturnsForPath( path, true )
			.GetStatusReturnsForPath( path, added, modified, missing, untracked );

		new TableViewAsserterSetup( serviceProvider )
			.SetExpectedHeadings( new List<string> { "Status" } )
			.SetOneExpectedCell( $"+{added} !{modified} -{missing} ?{untracked}" );

		var table = serviceProvider.GetRequiredService<ITableView>();
		await table.RenderAsync();
	}
}