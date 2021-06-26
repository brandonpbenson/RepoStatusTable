using System.Threading.Tasks;
using RepoStatusTable.Renderer;

namespace RepoStatusTable
{
	public interface IApplication
	{
		public Task RunAsync();
	}

	public class Application : IApplication
	{
		private readonly ITableRenderer _tableRenderer;

		public Application( ITableRenderer tableRenderer )
		{
			_tableRenderer = tableRenderer;
		}

		public async Task RunAsync()
		{
			await _tableRenderer.RenderAsync();
		}
	}
}