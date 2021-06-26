using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RepoStatusTable.Options;
using Spectre.Console;

namespace RepoStatusTable
{
	public interface IApplication
	{
		public Task RunAsync();
	}

	public class Application : IApplication
	{
		private readonly RepoOptions _repoOptions;

		public Application( IOptions<RepoOptions> repoOptions )
		{
			_repoOptions = repoOptions.Value;
		}

		public async Task RunAsync()
		{
			await AnsiConsole.Status().StartAsync( "Thinking", async _ => { await Task.Delay( 1000 ); } );
			foreach ( var dir in _repoOptions.RepoDirs ) Console.WriteLine( dir );
			foreach ( var dir in _repoOptions.ReposRoot ) Console.WriteLine( dir );
		}
	}
}