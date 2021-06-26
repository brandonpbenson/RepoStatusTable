using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RepoStatusTable.Options;
using RepoStatusTable.Renderer;

namespace RepoStatusTable
{
	public interface IApplication
	{
		public Task RunAsync();
	}

	public class Application : IApplication
	{
		private readonly IOptions<RepoOptions> _repoOptions;
		private readonly ITableRenderer _tableRenderer;

		public Application( ITableRenderer tableRenderer, IOptions<RepoOptions> repoOptions )
		{
			_tableRenderer = tableRenderer;
			_repoOptions = repoOptions;
		}

		public async Task RunAsync()
		{
			Console.WriteLine( _repoOptions.Value.RepoDirs );
			await _tableRenderer.RenderAsync();
		}
	}
}