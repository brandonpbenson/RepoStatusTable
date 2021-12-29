using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RepoStatusTable.CellProviders;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.UnitTests.CellProviders;

public class GitBranchProviderTests : CellProviderTests
{
	[Test]
	public async Task GetCell_WithGitFacadeReturnsBranchName_ShouldReturnBranchNameUnmodified()
	{
		const string branchName = "SomeBranch";

		// Prepare
		var facade = new Mock<IGitFacade>( MockBehavior.Strict );
		facade.Setup( m => m.GetCurrentBranch( DirectoryPath ) ).Returns( branchName );

		var options = new GitBranchProviderOptions();
		var uut = new GitBranchProvider( new OptionsWrapper<GitBranchProviderOptions>( options ), facade.Object );

		// Execute
		var result = await uut.GetCell( DirectoryPath );

		// Verify
		Assert.AreEqual( branchName, result.Content );
	}


	[Test]
	public override void CellProvider_WithOptions_ShouldUse()
	{
		var options = new GitBranchProviderOptions();
		var uut = new GitBranchProvider( new OptionsWrapper<GitBranchProviderOptions>( options ),
			new Mock<IGitFacade>().Object );
		AssertCellProviderUsesOptions( uut, options );
	}
}