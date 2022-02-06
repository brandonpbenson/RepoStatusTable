using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using RepoStatusTable.Model;
using RepoStatusTable.View;

namespace RepoStatusTable.IntegrationTests.ViewAsserters;

public class TableViewAsserter : ITableViewStrategy
{
	private readonly ITableModel _tableModel;

	public TableViewAsserter( ITableModel tableModel )
	{
		_tableModel = tableModel;
	}

	public IList<string>? ExpectedHeadings { get; set; }

	public IList<IList<string>>? ExpectedTableContent { get; set; }

	public string RenderMethod => "TableAsserter";

	public async Task RenderAsync()
	{
		var headings = _tableModel.GetHeadings().ToList();
		var enumTable = await _tableModel.GetTableAsync().ToListAsync();
		var table = enumTable.Select( r => r.ToList() );

		AssertHeadings( headings );
		AssertTable( table );
	}

	private void AssertHeadings( List<string> headings )
	{
		if ( ExpectedHeadings is null )
		{
			return;
		}

		Assert.AreEqual( ExpectedHeadings, headings );
	}

	private void AssertTable( IEnumerable tasks )
	{
		if ( ExpectedTableContent is null )
		{
			return;
		}

		Assert.AreEqual( ExpectedTableContent, tasks );
	}
}