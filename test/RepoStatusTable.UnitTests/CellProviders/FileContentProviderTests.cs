using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RepoStatusTable.CellProviders;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.UnitTests.CellProviders;

public class FileContentProviderTests : CellProviderTests
{
	private const string TextLine1 = "FirstLine";
	private const string TextLine2 = "SecondLine";
	private const string FilePath = "SomeFile.txt";
	private const string CompletePath = DirectoryPath + FilePath;

	[TestCase( 1, TextLine1 )]
	[TestCase( 2, TextLine1 + "\n" + TextLine2 )]
	public async Task GetCell_WithOptions_ShouldOnlyReturnExpectedNumberOfLines( int nol, string expectation )
	{
		var textFile = new[] { TextLine1, TextLine2 };

		// Prepare
		var facade = new Mock<IFileSystemFacade>( MockBehavior.Strict );
		facade.Setup( m => m.Join( DirectoryPath, FilePath ) ).Returns( CompletePath );
		facade.Setup( m => m.ReadText( CompletePath ) ).Returns( textFile );

		var options = new FileContentProviderOptions
		{
			Path = FilePath,
			NumberOfLines = nol
		};

		var uut = new FileContentProvider( new OptionsWrapper<FileContentProviderOptions>( options ), facade.Object );

		// Execute
		var result = await uut.GetCell( DirectoryPath );

		// Verify
		Assert.AreEqual( expectation, result.Content );
	}

	[TestCaseSource( nameof(ExceptionTestCases) )]
	public async Task GetCell_WithFileFacadeThrows_ShouldReturnEmptyString( Exception e )
	{
		// Prepare
		var facade = new Mock<IFileSystemFacade>( MockBehavior.Strict );
		facade.Setup( m => m.Join( DirectoryPath, FilePath ) ).Returns( CompletePath );
		facade.Setup( m => m.ReadText( CompletePath ) ).Throws( e );

		var options = new FileContentProviderOptions
		{
			Path = FilePath,
			NumberOfLines = 1,
			IgnoreMissingFiles = true
		};

		var uut = new FileContentProvider( new OptionsWrapper<FileContentProviderOptions>( options ), facade.Object );

		// Execute
		var result = await uut.GetCell( DirectoryPath );

		// Verify
		Assert.AreEqual( string.Empty, result.Content );
	}

	[TestCaseSource( nameof(ExceptionTestCases) )]
	public void GetCell_WithFileFacadeThrowsAndShouldNotIgnoreMissingFiles_ShouldThrowArgumentException( Exception e )
	{
		// Prepare
		var facade = new Mock<IFileSystemFacade>( MockBehavior.Strict );
		facade.Setup( m => m.Join( DirectoryPath, FilePath ) ).Returns( CompletePath );
		facade.Setup( m => m.ReadText( CompletePath ) ).Throws( e );

		var options = new FileContentProviderOptions
		{
			Path = FilePath,
			NumberOfLines = 1,
			IgnoreMissingFiles = false
		};

		var uut = new FileContentProvider( new OptionsWrapper<FileContentProviderOptions>( options ), facade.Object );

		// Execute
		Assert.ThrowsAsync<ArgumentException>( () => uut.GetCell( DirectoryPath ) );
	}

	private static IEnumerable<TestCaseData> ExceptionTestCases()
	{
		yield return new TestCaseData( new UnauthorizedAccessException() );
		yield return new TestCaseData( new FileNotFoundException() );
	}

	[Test]
	public override void CellProvider_WithOptions_ShouldUse()
	{
		var options = new FileContentProviderOptions();
		var uut = new FileContentProvider( new OptionsWrapper<FileContentProviderOptions>( options ),
			new Mock<IFileSystemFacade>().Object );
		AssertCellProviderUsesOptions( uut, options );
	}
}