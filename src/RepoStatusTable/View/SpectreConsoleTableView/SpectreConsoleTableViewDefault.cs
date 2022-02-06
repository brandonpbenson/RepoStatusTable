using RepoStatusTable.Model;

namespace RepoStatusTable.View.SpectreConsoleTableView;

public class SpectreConsoleTableViewDefault : ITableViewStrategy
{
	private readonly ISpectreTableFactory _spectreTableFactory;
	private readonly ITableModel _tableModel;

	public SpectreConsoleTableViewDefault( ITableModel tableModel, ISpectreTableFactory spectreTableFactory )
	{
		_tableModel = tableModel;
		_spectreTableFactory = spectreTableFactory;
	}

	public string RenderMethod => "Spectre Table";

	public async Task RenderAsync()
	{
		var table = _spectreTableFactory.CreateFromOptions();

		RenderHeadings( table );
		await RenderRows( table );

		AnsiConsole.Render( table );
	}

	private void RenderHeadings( Table table )
	{
		foreach ( var heading in _tableModel.GetHeadings() ) table.AddColumn( heading );
	}

	private async Task RenderRows( Table table )
	{
		await foreach ( var row in _tableModel.GetTableAsync() )
		{
			var tableRow = row.ToArray();
			table.AddRow( tableRow );
		}
	}
}