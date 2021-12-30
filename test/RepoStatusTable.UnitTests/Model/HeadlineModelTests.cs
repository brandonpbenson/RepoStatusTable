using Microsoft.Extensions.Options;
using RepoStatusTable.Model;
using RepoStatusTable.Options;

namespace RepoStatusTable.UnitTests.Model;

public class HeadlineModelTests
{
	[Test]
	public void GetHeadline_WithOption_ShouldReturnHeadline()
	{
		const string headline = "test";
		var options = new HeadlineOptions
		{
			Text = headline
		};

		var uut = new HeadlineModel( new OptionsWrapper<HeadlineOptions>( options ) );

		var result = uut.GetHeadline();

		Assert.AreEqual( headline, result );
	}
}