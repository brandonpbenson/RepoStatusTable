using System.Threading.Tasks;
using RepoStatusTable.View;

namespace RepoStatusTable;

public interface IApplication
{
	public Task RunAsync();
}

public class Application : IApplication
{
	private readonly IHeadlineView _headlineView;
	private readonly ITableView _tableView;

	public Application( IHeadlineView headlineView, ITableView tableView )
	{
		_headlineView = headlineView;
		_tableView = tableView;
	}

	public async Task RunAsync()
	{
		await _headlineView.RenderAsync();
		await _tableView.RenderAsync();
	}
}