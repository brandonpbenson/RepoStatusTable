using System.Threading.Tasks;
using RepoStatusTable.Model;
using Spectre.Console;

namespace RepoStatusTable.View.SpectreConsoleFigletHeadlineView
{
	public class SpectreConsoleFigletHeadlineView : IHeadlineViewStrategy
	{
		private readonly ISpectreFigletFactory _figletFactory;
		private readonly IHeadlineModel _headlineModel;

		public SpectreConsoleFigletHeadlineView( IHeadlineModel headlineModel, ISpectreFigletFactory figletFactory )
		{
			_headlineModel = headlineModel;
			_figletFactory = figletFactory;
		}

		public Task RenderAsync()
		{
			var figlet = _figletFactory.CreateFromOptions( _headlineModel.GetHeadline() );
			AnsiConsole.Render( figlet );
			return Task.CompletedTask;
		}
	}
}