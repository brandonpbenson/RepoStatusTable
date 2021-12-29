using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RepoStatusTable.CellProviders;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.UnitTests.CellProviders;

public class DirectoryNameProviderTests : CellProviderTests
{
	[Test]
	public async Task GetCell_WithFileSystemFacadeReturnsDirectoryName_ShouldReturnCellWithDirectoryName()
	{
		const string directoryName = "path";

		// Prepare
		var facade = new Mock<IFileSystemFacade>( MockBehavior.Strict );
		facade.Setup( m => m.GetDirectoryName( DirectoryPath ) ).Returns( directoryName );
		var options = new DirectoryNameProviderOptions();
		var uut = new DirectoryNameProvider( new OptionsWrapper<DirectoryNameProviderOptions>( options ),
			facade.Object );

		// Execute
		var result = await uut.GetCell( DirectoryPath );

		// Verify
		Assert.AreEqual( directoryName, result.Content );
	}

	[Test]
	public override void CellProvider_WithOptions_ShouldUse()
	{
		var options = new DirectoryNameProviderOptions();
		var uut = new DirectoryNameProvider( new OptionsWrapper<DirectoryNameProviderOptions>( options ),
			new Mock<IFileSystemFacade>().Object );
		AssertCellProviderUsesOptions( uut, options );
	}
}