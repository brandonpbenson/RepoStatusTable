using Spectre.Console;

namespace RepoStatusTable;

public static class ExceptionHandler
{
	public static void Handler( object sender, UnhandledExceptionEventArgs args )
	{
		if ( args.ExceptionObject is not Exception exception )
		{
			return;
		}

		RenderError( exception );

		Environment.Exit( 1 );
	}

	private static void RenderError( Exception ex )
	{
		const string headline = "[bold red]An error occured![/]";
		var type = $"[bold]Exception Type:[/] {ex.GetType()}";
		var message = $"[bold]Reason:[/] {ex.Message}";

		var text = new Markup( type + "\n" + message );
		var panel = new Panel( text ) { Header = new PanelHeader( headline ) };

		AnsiConsole.Render( panel );
	}
}