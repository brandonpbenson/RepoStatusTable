using System.Threading;
using System.Threading.Tasks;
using Spectre.Console;

namespace RepoStatusTable.Renderer
{
	public class SpectreConsoleTableRenderer : ITableRenderer
	{
		public async Task RenderAsync()
		{
			var table = new Table().Centered();

			AnsiConsole.Live( table )
				.Start( ctx =>
				{
					table.AddColumn( "Foo" );
					ctx.Refresh();
					Thread.Sleep( 1000 );

					table.AddColumn( "Bar" );
					ctx.Refresh();
					Thread.Sleep( 1000 );
				} );
		}
	}
}