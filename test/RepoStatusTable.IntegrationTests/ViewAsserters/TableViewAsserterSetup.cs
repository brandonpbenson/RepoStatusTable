using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using RepoStatusTable.View;

namespace RepoStatusTable.IntegrationTests.ViewAsserters;

public class TableViewAsserterSetup
{
	private readonly TableViewAsserter _instance;

	public TableViewAsserterSetup( IServiceProvider provider )
	{
		_instance = provider.GetRequiredService<ITableViewStrategy>() as TableViewAsserter ??
		            throw new InvalidOperationException();
	}

	public TableViewAsserterSetup SetExpectedHeadings( IList<string> headline )
	{
		_instance.ExpectedHeadings = headline;
		return this;
	}

	public TableViewAsserterSetup SetOneExpectedCell( string content )
	{
		var table = new List<IList<string>>
		{
			new List<string>
			{
				content
			}
		};
		SetExpectedTableContent( table );
		return this;
	}

	public void SetExpectedTableContent( IList<IList<string>> content )
	{
		_instance.ExpectedTableContent = content;
	}
}