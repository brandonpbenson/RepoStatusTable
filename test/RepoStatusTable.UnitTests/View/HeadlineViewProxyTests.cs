using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RepoStatusTable.Options;
using RepoStatusTable.View;

namespace RepoStatusTable.UnitTests.View;

public class HeadlineViewProxyTests
{
	[Test]
	public async Task RenderAsync_WithHeadlineEnabled_ShouldCallStrategy()
	{
		var options = new HeadlineOptions
		{
			Enable = true
		};

		var headlineViewStrategy = new Mock<IHeadlineViewStrategy>();
		var uut = new HeadlineViewProxy( new OptionsWrapper<HeadlineOptions>( options ), headlineViewStrategy.Object );

		await uut.RenderAsync();

		headlineViewStrategy.Verify( m => m.RenderAsync(), Times.Once );
	}

	[Test]
	public void RenderAsync_WithHeadlineDisabled_ShouldReturnCompletedTask()
	{
		var options = new HeadlineOptions
		{
			Enable = false
		};

		var headlineViewStrategy = new Mock<IHeadlineViewStrategy>( MockBehavior.Strict );
		var uut = new HeadlineViewProxy( new OptionsWrapper<HeadlineOptions>( options ), headlineViewStrategy.Object );

		var result = uut.RenderAsync();

		Assert.AreEqual( Task.CompletedTask, result );
		headlineViewStrategy.Verify( m => m.RenderAsync(), Times.Never );
	}
}