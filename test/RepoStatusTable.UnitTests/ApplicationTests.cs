using System.Threading.Tasks;
using RepoStatusTable.View;

namespace RepoStatusTable.UnitTests;

public class ApplicationTests
{
	[Test]
	public async Task RunAsync_ShouldRenderHeadlineAndTable()
	{
		var headlineView = new Mock<IHeadlineView>( MockBehavior.Strict );
		headlineView.Setup( m => m.RenderAsync() ).Returns( Task.CompletedTask );

		var tableView = new Mock<ITableView>( MockBehavior.Strict );
		tableView.Setup( m => m.RenderAsync() ).Returns( Task.CompletedTask );

		var uut = new Application( headlineView.Object, tableView.Object );

		await uut.RunAsync();

		headlineView.Verify( m => m.RenderAsync(), Times.Once );
		headlineView.Verify( m => m.RenderAsync(), Times.Once );
	}
}