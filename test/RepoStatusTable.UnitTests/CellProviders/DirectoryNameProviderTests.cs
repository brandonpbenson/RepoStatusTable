using Microsoft.Extensions.Options;
using RepoStatusTable.CellProviders;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.UnitTests.CellProviders;

public class DirectoryNameProviderTests : CellProviderTests
{
	[Test]
	public override void CellProvider_WithOptions_ShouldUse()
	{
		var options = new DirectoryNameProviderOptions();
		var uut = new DirectoryNameProvider( new OptionsWrapper<DirectoryNameProviderOptions>( options ),
			new Mock<IFileSystemFacade>().Object );
		AssertCellProviderUsesOptions( uut, options );
	}
}