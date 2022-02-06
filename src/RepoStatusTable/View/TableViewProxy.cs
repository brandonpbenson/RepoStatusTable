using RepoStatusTable.Options;

namespace RepoStatusTable.View;

public class TableViewProxy : ITableView
{
	private readonly TableOptions _options;
	private readonly IEnumerable<ITableViewStrategy> _tableViewStrategies;

	public TableViewProxy( IOptions<TableOptions> options, IEnumerable<ITableViewStrategy> tableViewStrategies )
	{
		_tableViewStrategies = tableViewStrategies;
		_options = options.Value;
	}

	public Task RenderAsync()
	{
		return GetTableViewStrategy().RenderAsync();
	}

	private ITableView GetTableViewStrategy()
	{
		var strategy = _tableViewStrategies.FirstOrDefault( t => t.RenderMethod == _options.RenderMethod );

		if ( strategy is null )
		{
			throw new ArgumentException( $"Unknown render method: {_options.RenderMethod}" );
		}

		return strategy;
	}
}