using System.Threading.Tasks;
using RepoStatusTable.View;

namespace RepoStatusTable
{
	public interface IApplication
	{
		public Task RunAsync();
	}

	public class Application : IApplication
	{
		private readonly ITableView _tableView;

		public Application( ITableView tableView )
		{
			_tableView = tableView;
		}

		public async Task RunAsync()
		{
			await _tableView.RenderAsync();
		}
	}
}