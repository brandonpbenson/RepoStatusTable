using System;
using RepoStatusTable.Options.SpectreConsole;

namespace RepoStatusTable.UnitTests.Options.SpectreConsole;

public class SpectreTableOptionsTests
{
	[TestCase( "AsciiDoubleHead" )]
	[TestCase( "MinimalHeavyHead" )]
	[TestCase( "DoubleEdge" )]
	public void GetTableBorder_WithValidBorderName_ShouldReturnBorder( string name )
	{
		var uut = new SpectreTableOptions
		{
			Border = name
		};

		var result = uut.GetTableBorder();

		Assert.AreEqual( name + "TableBorder", result.GetType().Name );
	}

	[TestCase( "AsciiDoubleHead" )]
	[TestCase( "MinimalHeavyHead" )]
	[TestCase( "DoubleEdge" )]
	public void GetTableBorder_WithLowerCaseBorderName_ShouldReturnBorder( string name )
	{
		var uut = new SpectreTableOptions
		{
			Border = name.ToLower()
		};

		var result = uut.GetTableBorder();

		Assert.AreEqual( name + "TableBorder", result.GetType().Name );
	}

	[Test]
	public void GetTableBorder_WithInvalidBorderName_ShouldThrowException()
	{
		var uut = new SpectreTableOptions
		{
			Border = "nonesense"
		};

		Assert.Throws<ArgumentException>( () => uut.GetTableBorder() );
	}
}