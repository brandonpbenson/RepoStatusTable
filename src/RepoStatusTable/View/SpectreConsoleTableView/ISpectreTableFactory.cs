using Spectre.Console;

namespace RepoStatusTable.View.SpectreConsoleTableView;

public interface ISpectreTableFactory
{
	public Table CreateFromOptions();
}