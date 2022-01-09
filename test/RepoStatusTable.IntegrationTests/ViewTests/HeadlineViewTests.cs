using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RepoStatusTable.IntegrationTests.DependencyInjection;
using RepoStatusTable.IntegrationTests.ViewAsserters;
using RepoStatusTable.View;

namespace RepoStatusTable.IntegrationTests.ViewTests;

public class HeadlineTests
{
	private const string HeadlineText = "Test Headline 123";

	[Test]
	public void Headline_WithHeadlineEnabled_HeadlineViewShouldGetHeadlineText()
	{
		var serviceProvider = new TestServiceProviderBuilder()
			.AddOrReplaceConfigKeyValuePair( "Headline:Enable", "true" )
			.AddOrReplaceConfigKeyValuePair( "Headline:Text", HeadlineText )
			.Build();

		new HeadlineViewAsserterSetup( serviceProvider )
			.SetExpectedHeadline( HeadlineText );

		var headlineViewProxy = serviceProvider.GetRequiredService<IHeadlineView>();
		headlineViewProxy.RenderAsync();
	}

	[Test]
	public void Headline_WithHeadlineDisabled_HeadlineViewShouldNotGetCalled()
	{
		var serviceProvider = new TestServiceProviderBuilder()
			.AddOrReplaceConfigKeyValuePair( "Headline:Enable", "false" )
			.AddOrReplaceConfigKeyValuePair( "Headline:Text", HeadlineText )
			.Build();

		new HeadlineViewAsserterSetup( serviceProvider ).SetCallExpectationFalse();

		var headlineViewProxy = serviceProvider.GetRequiredService<IHeadlineView>();
		headlineViewProxy.RenderAsync();
	}
}