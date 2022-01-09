using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RepoStatusTable.IntegrationTests.DependencyInjection;
using RepoStatusTable.IntegrationTests.ViewAsserters;
using RepoStatusTable.View;

namespace RepoStatusTable.IntegrationTests.ViewTests;

public class TableViewBaseTests
{
	[Test]
	public async Task TableView_WithNoOptions_ShouldGetDefaultCellProviderHeadings()
	{
		var serviceProvider = new TestServiceProviderBuilder().Build();

		new TableViewAsserterSetup( serviceProvider ).SetExpectedHeadings( new List<string>
		{
			"Name", "Branch", "Status"
		} );

		var tableView = serviceProvider.GetRequiredService<ITableView>();
		await tableView.RenderAsync();
	}
}