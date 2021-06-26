using System;
using Microsoft.Extensions.Options;
using RepoStatusTable.Options;

namespace RepoStatusTable
{
	public interface IApplication
	{
		public void Run();
	}

	public class Application : IApplication
	{
		private readonly RepoOptions _repoOptions;

		public Application( IOptions<RepoOptions> repoOptions )
		{
			_repoOptions = repoOptions.Value;
		}

		public void Run()
		{
			foreach ( var dir in _repoOptions.RepoDirs ) Console.WriteLine( dir );

			foreach ( var dir in _repoOptions.ReposRoot ) Console.WriteLine( dir );
		}
	}
}