using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using RepoStatusTable.Facade;

namespace RepoStatusTable.CellProviders
{
	public class GitStatusProvider : ICellProvider
	{
		private readonly IGitFacade _gitFacade;

		public string Heading => "Status";

		public GitStatusProvider( [NotNull] IGitFacade gitFacade )
		{
			_gitFacade = gitFacade;
		}

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