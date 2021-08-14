using System.Linq;
using System.Threading.Tasks;
using RepoStatusTable.Model;
using Spectre.Console;

namespace RepoStatusTable.View
{
	public class SpectreConsoleTableViewLive : ITableViewStrategy
	{
		private readonly ITableModel _tableModel;

		public SpectreConsoleTableViewLive( ITableModel tableModel )
		{
			_tableModel = tableModel;
		}

		public string RenderMethod => "Spectre Table Live";

		public async Task RenderAsync()
		{
			var table = new Table().Centered();

			await AnsiConsole.Live( table )
				.StartAsync( async ctx =>
					{
						RenderHeadings( table, ctx );
						await RenderRows( table, ctx );
					}
				);
		}

		private void RenderHeadings( Table table, LiveDisplayContext ctx )
		{
			foreach ( var heading in _tableModel.GetHeadings() )
			{
				table.AddColumn( heading );
				ctx.Refresh();
			}
		}

		private async Task RenderRows( Table table, LiveDisplayContext ctx )
		{
			await foreach ( var row in _tableModel.GetTableAsync() )
			{
				var tableRow = await row.ToArrayAsync();
				table.AddRow( tableRow );
				ctx.Refresh();
			}
		}
	}
}