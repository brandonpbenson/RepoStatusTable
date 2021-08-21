using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.CellProviders
{
	public class GitStatusProvider : ICellProvider
	{
		private readonly IGitFacade _gitFacade;
		private readonly GitStatusProviderOptions _options;

		public GitStatusProvider( IOptions<GitStatusProviderOptions> options, IGitFacade gitFacade )
		{
			_options = options.Value;
			_gitFacade = gitFacade;
		}

		public string Heading => _options.Heading ?? "Status";

		public bool IsEnabled => _options.Enable;

		public int? Position => _options.Position;

		public Task<Cell> GetCell( string directory )
		{
			var status = _gitFacade.GetStatus( directory );

			return Task.FromResult(
				new Cell( GetStatusDescription( status ) ) );
		}

		private static bool IsStatusChanged( IDictionary<string, int> status ) =>
			!( status is null || status.All( v => v.Value == 0 ) );

		private static string GetStatusDescription( IDictionary<string, int> status ) =>
			$"+{status["Added"]} " +
			$"!{status["Modified"]} " +
			$"-{status["Missing"]} " +
			$"?{status["Untracked"]}";
	}
}