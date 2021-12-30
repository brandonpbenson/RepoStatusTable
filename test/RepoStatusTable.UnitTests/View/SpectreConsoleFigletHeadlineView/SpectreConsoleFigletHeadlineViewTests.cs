using System.Threading.Tasks;
using RepoStatusTable.Model;
using RepoStatusTable.View.SpectreConsoleFigletHeadlineView;
using Spectre.Console;

namespace RepoStatusTable.UnitTests.View.SpectreConsoleFigletHeadlineView;

public class SpectreConsoleFigletHeadlineViewTests
{
	[Test]
	public void RenderAsync_WithProperSetup_ShouldReturnCompletedTask()
	{
		// Prepare
		var headlineModel = new Mock<IHeadlineModel>( MockBehavior.Strict );
		headlineModel.Setup( m => m.GetHeadline() )
			.Returns( "test" )
			.Verifiable();

		var factory = new Mock<ISpectreFigletFactory>( MockBehavior.Strict );
		factory.Setup( m => m.CreateFromOptions( "test" ) )
			.Returns( new FigletText( "test" ) )
			.Verifiable();

		var uut = new RepoStatusTable.View.SpectreConsoleFigletHeadlineView.SpectreConsoleFigletHeadlineView(
			headlineModel.Object,
			factory.Object );

		// Execute
		var result = uut.RenderAsync();

		// Verify
		Assert.AreEqual( Task.CompletedTask, result );

		headlineModel.Verify();
		headlineModel.VerifyNoOtherCalls();

		factory.Verify();
		factory.VerifyNoOtherCalls();
	}
}