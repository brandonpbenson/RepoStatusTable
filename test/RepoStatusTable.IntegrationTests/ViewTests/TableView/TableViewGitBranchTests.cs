using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RepoStatusTable.IntegrationTests.DependencyInjection;
using RepoStatusTable.IntegrationTests.FacadeMocks;
using RepoStatusTable.IntegrationTests.ViewAsserters;
using RepoStatusTable.View;

namespace RepoStatusTable.IntegrationTests.ViewTests.TableView;

public class TableViewGitBranchTests
{
	[Test]
	public async Task TableView_WithGitStatusProviderOnly_ShouldGetExpectedTable()
	{
		const string branch = "master";
		const string dir = "repo";
		const string path = $"/path/to/{dir}";

		var serviceProvider = new TestServiceProviderBuilder()
			.DeactivateDefaultCellProviders()
			.AddOrReplaceConfigKeyValuePair( "CellProviders:GitBranchProvider:Enable", "true" )
			.AddOrReplaceConfigKeyValuePair( "Repos:RepoDirs:0", path )
			.Build();

		new FileSystemFacadeMockSetup( serviceProvider )
			.DirectoryExistsReturnsForPath( path, true )
			.GetFullPathReturnsForPath( path, path );

		new GitFacadeMockSetup( serviceProvider )
			.IsVscRepoReturnsForPath( path, true )
			.GetBranchReturnsForPath( path, branch );

		new TableViewAsserterSetup( serviceProvider )
			.SetExpectedHeadings( new List<string> { "Branch" } )
			.SetOneExpectedCell( branch );

		var table = serviceProvider.GetRequiredService<ITableView>();
		await table.RenderAsync();
	}
}