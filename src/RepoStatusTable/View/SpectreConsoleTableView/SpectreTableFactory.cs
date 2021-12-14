using RepoStatusTable.Options.SpectreConsole;

namespace RepoStatusTable.View.SpectreConsoleTableView;

public class SpectreTableFactory : ISpectreTableFactory
{
	private readonly SpectreTableOptions _options;

	public SpectreTableFactory( IOptions<SpectreTableOptions> options )
	{
		_options = options.Value;
	}

	public Table CreateFromOptions()
	{
		var borderColor = new Style( _options.Color.GetSpectreConsoleColor() );

		var table = new Table()
			.Alignment( _options.Alignment )
			.Border( _options.GetTableBorder() )
			.BorderStyle( borderColor );

		if ( _options.Expand )
		{
			table.Expand();
		}

		return table;
	}
}