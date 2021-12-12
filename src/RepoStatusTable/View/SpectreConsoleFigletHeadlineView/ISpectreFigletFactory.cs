using Spectre.Console;

namespace RepoStatusTable.View.SpectreConsoleFigletHeadlineView
{

	public interface ISpectreFigletFactory
	{
		FigletText CreateFromOptions( string text );
	}
}