using RepoStatusTable.CellProviders;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.UnitTests.CellProviders;

public abstract class CellProviderTests
{
	protected const string DirectoryPath = "/a/path";

	// ReSharper disable once UnusedMemberInSuper.Global
	// Used as test method
	public abstract void CellProvider_WithOptions_ShouldUse();

	protected static void AssertCellProviderUsesOptions( ICellProvider uut, ICellProviderOptions options )
	{
		const string heading = "DirectoryNameOptionsHeading";
		const int position = 999;

		options.Heading = heading;
		options.Enable = false;
		options.Position = 999;

		Assert.AreEqual( heading, uut.Heading );
		Assert.IsFalse( uut.IsEnabled );
		Assert.AreEqual( position, uut.Position );
	}
}