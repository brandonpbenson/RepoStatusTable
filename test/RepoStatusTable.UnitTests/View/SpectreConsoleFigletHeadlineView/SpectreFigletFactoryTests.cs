using Microsoft.Extensions.Options;
using RepoStatusTable.Options.SpectreConsole;
using RepoStatusTable.View.SpectreConsoleFigletHeadlineView;
using Spectre.Console;

namespace RepoStatusTable.UnitTests.View.SpectreConsoleFigletHeadlineView;

public class SpectreFigletFactoryTests
{
	[Test]
	public void CreateFromOptions_WithOptions_ShouldReturnFigletText()
	{
		var options = new SpectreFigletOptions
		{
			Alignment = Justify.Left,
			Color = new SpectreColorOptions
			{
				Notation = ColorNotation.Name,
				Value = "maroon"
			}
		};

		var uut = new SpectreFigletFactory( new OptionsWrapper<SpectreFigletOptions>( options ) );

		var result = uut.CreateFromOptions( "test" );

		Assert.AreEqual( Justify.Left, result.Alignment );
		Assert.AreEqual( Color.Maroon, result.Color );
	}

	[Test]
	public void CreateFromOptions_WithEmptyOptions_ShouldUseDefaults()
	{
		var options = new SpectreFigletOptions();

		var uut = new SpectreFigletFactory( new OptionsWrapper<SpectreFigletOptions>( options ) );

		var result = uut.CreateFromOptions( "test" );

		Assert.AreEqual( Justify.Center, result.Alignment );
		Assert.AreEqual( Color.Black, result.Color );
	}
}