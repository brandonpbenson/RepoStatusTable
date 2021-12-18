using Microsoft.Extensions.Options;
using RepoStatusTable.CellProviders;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.UnitTests.CellProviders;

public class FileContentProviderTests : CellProviderTests
{
	[Test]
	public override void CellProvider_WithOptions_ShouldUse()
	{
		var options = new FileContentProviderOptions();
		var uut = new FileContentProvider( new OptionsWrapper<FileContentProviderOptions>( options ),
			new Mock<IFileSystemFacade>().Object );
		AssertCellProviderUsesOptions( uut, options );
	}
}