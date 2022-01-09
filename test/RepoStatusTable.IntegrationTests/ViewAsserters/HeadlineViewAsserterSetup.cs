using System;
using Microsoft.Extensions.DependencyInjection;
using RepoStatusTable.View;

namespace RepoStatusTable.IntegrationTests.ViewAsserters;

public class HeadlineViewAsserterSetup
{
	private readonly HeadlineViewAsserter _instance;

	public HeadlineViewAsserterSetup( IServiceProvider provider )
	{
		_instance = provider.GetRequiredService<IHeadlineViewStrategy>() as HeadlineViewAsserter ??
		            throw new InvalidOperationException();
	}

	public void SetCallExpectationFalse()
	{
		_instance.CallExpected = false;
	}

	public void SetExpectedHeadline( string headline )
	{
		_instance.ExpectedHeadline = headline;
	}
}