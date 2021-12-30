using System;
using RepoStatusTable.Options.SpectreConsole;

namespace RepoStatusTable.UnitTests.Options.SpectreConsole;

public class SpectreColorOptionsTests
{
	[TestCase( "010203", 1, 2, 3 )]
	[TestCase( "1a2b3c", 26, 43, 60 )]
	[TestCase( "ffffff", 255, 255, 255 )]
	[TestCase( "#010203", 1, 2, 3 )]
	[TestCase( "#1a2b3c", 26, 43, 60 )]
	[TestCase( "#ffffff", 255, 255, 255 )]
	public void GetSpectreConsoleColor_WithHexOption_ShouldReturnColor( string hex, int r, int g, int b )
	{
		var uut = new SpectreColorOptions
		{
			Notation = ColorNotation.Hexadecimal,
			Value = hex
		};

		var result = uut.GetSpectreConsoleColor();

		Assert.AreEqual( r, result.R );
		Assert.AreEqual( g, result.G );
		Assert.AreEqual( b, result.B );
	}

	[TestCase( "maroon", 128, 0, 0 )]
	[TestCase( "olive", 128, 128, 0 )]
	[TestCase( "darkturquoise", 0, 215, 215 )]
	public void GetSpectreConsoleColor_WithNameOption_ShouldReturnColor( string name, int r, int g, int b )
	{
		var uut = new SpectreColorOptions
		{
			Notation = ColorNotation.Name,
			Value = name
		};

		var result = uut.GetSpectreConsoleColor();

		Assert.AreEqual( r, result.R );
		Assert.AreEqual( g, result.G );
		Assert.AreEqual( b, result.B );
	}

	[Test]
	public void GetSpectreConsoleColor_WithRgbOption_ShouldReturnRgbColor()
	{
		var uut = new SpectreColorOptions
		{
			Notation = ColorNotation.Rgb,
			Value = "1,2,3"
		};

		var result = uut.GetSpectreConsoleColor();

		Assert.AreEqual( 1, result.R );
		Assert.AreEqual( 2, result.G );
		Assert.AreEqual( 3, result.B );
	}

	[TestCase( "" )]
	[TestCase( "1,2" )]
	[TestCase( "1,2,3,4" )]
	[TestCase( "nonesense" )]
	public void GetSpectreConsoleColor_WithInvalidRgbValue_ShouldThrowArgumentException( string value )
	{
		var uut = new SpectreColorOptions
		{
			Notation = ColorNotation.Rgb,
			Value = value
		};

		Assert.Throws<ArgumentException>( () => uut.GetSpectreConsoleColor() );
	}

	[TestCase( "" )]
	[TestCase( "fffff" )]
	[TestCase( "fffffff" )]
	[TestCase( "1,2,3" )]
	[TestCase( "nonesense" )]
	public void GetSpectreConsoleColor_WithInvalidHexValue_ShouldThrowArgumentException( string value )
	{
		var uut = new SpectreColorOptions
		{
			Notation = ColorNotation.Hexadecimal,
			Value = value
		};

		Assert.Throws<ArgumentException>( () => uut.GetSpectreConsoleColor() );
	}

	[TestCase( "" )]
	[TestCase( "ffffff" )]
	[TestCase( "1,2,3" )]
	[TestCase( "unknowncolor" )]
	public void GetSpectreConsoleColor_WithInvalidName_ShouldThrowArgumentException( string value )
	{
		var uut = new SpectreColorOptions
		{
			Notation = ColorNotation.Name,
			Value = value
		};

		Assert.Throws<ArgumentException>( () => uut.GetSpectreConsoleColor() );
	}
}