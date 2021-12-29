using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RepoStatusTable.CellProviders;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.UnitTests.CellProviders;

public class GitStatusProviderTests : CellProviderTests
{
	[Test]
	public async Task GetCell_WithGitFacadeReturnsStatus_ShouldReturnStatusString()
	{
		var status = new Dictionary<string, int>
		{
			{ "Added", 11 },
			{ "Modified", 12 },
			{ "Missing", 13 },
			{ "Untracked", 14 }
		};

		var expected = $"+{status["Added"]} " +
		               $"!{status["Modified"]} " +
		               $"-{status["Missing"]} " +
		               $"?{status["Untracked"]}";

		// Prepare
		var facade = new Mock<IGitFacade>( MockBehavior.Strict );
		facade.Setup( m => m.GetStatus( DirectoryPath ) ).Returns( status );

		var options = new GitStatusProviderOptions();
		var uut = new GitStatusProvider( new OptionsWrapper<GitStatusProviderOptions>( options ), facade.Object );

		// Execute
		var result = await uut.GetCell( DirectoryPath );

		// Verify
		Assert.AreEqual( expected, result.Content );
	}

	[Test]
	public override void CellProvider_WithOptions_ShouldUse()
	{
		var options = new GitStatusProviderOptions();
		var uut = new GitStatusProvider( new OptionsWrapper<GitStatusProviderOptions>( options ),
			new Mock<IGitFacade>().Object );
		AssertCellProviderUsesOptions( uut, options );
	}
}