using RepoStatusTable.Model;
using RepoStatusTable.View.SpectreConsoleTableView;

namespace RepoStatusTable.View;

public class SpectreConsoleTableViewLive : ITableViewStrategy
{
	private readonly ISpectreTableFactory _tableFactory;
	private readonly ITableModel _tableModel;

	public SpectreConsoleTableViewLive( ITableModel tableModel, ISpectreTableFactory tableFactory )
	{
		_tableModel = tableModel;
		_tableFactory = tableFactory;
	}

	public string RenderMethod => "Spectre Table Live";

	public async Task RenderAsync()
	{
		var table = _tableFactory.CreateFromOptions();

		await AnsiConsole.Live( table )
			.StartAsync( async ctx =>
				{
					RenderHeadings( table, ctx );
					await RenderRows( table, ctx );
				}
			);
	}

	private void RenderHeadings( Table table, LiveDisplayContext ctx )
	{
		foreach ( var heading in _tableModel.GetHeadings() )
		{
			table.AddColumn( heading );
			ctx.Refresh();
		}
	}

	private async Task RenderRows( Table table, LiveDisplayContext ctx )
	{
		await foreach ( var row in _tableModel.GetTableAsync() )
		{
			var tableRow = row.ToArray();
			table.AddRow( tableRow );
			ctx.Refresh();
		}
	}
}