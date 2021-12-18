using Microsoft.Extensions.Options;
using RepoStatusTable.CellProviders;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.UnitTests.CellProviders;

public class GitBranchProviderTests : CellProviderTests
{
	[Test]
	public override void CellProvider_WithOptions_ShouldUse()
	{
		var options = new GitBranchProviderOptions();
		var uut = new GitBranchProvider( new OptionsWrapper<GitBranchProviderOptions>( options ),
			new Mock<IGitFacade>().Object );
		AssertCellProviderUsesOptions( uut, options );
	}
}