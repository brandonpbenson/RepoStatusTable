using System.Threading.Tasks;
using NUnit.Framework;
using RepoStatusTable.Model;
using RepoStatusTable.View;

namespace RepoStatusTable.IntegrationTests.ViewAsserters;

public class HeadlineViewAsserter : IHeadlineViewStrategy
{
	private readonly IHeadlineModel _headlineModel;

	public HeadlineViewAsserter( IHeadlineModel headlineModel )
	{
		_headlineModel = headlineModel;
	}

	public bool CallExpected { get; set; } = true;

	public string? ExpectedHeadline { get; set; }

	public Task RenderAsync()
	{
		AssertCall();
		AssertExpectedHeadline();
		return Task.CompletedTask;
	}

	private void AssertCall()
	{
		if ( CallExpected == false )
		{
			Assert.Fail();
		}
	}

	private void AssertExpectedHeadline()
	{
		Assert.AreEqual( ExpectedHeadline, _headlineModel.GetHeadline() );
	}
}