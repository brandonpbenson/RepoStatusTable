namespace RepoStatusTable.Options.SpectreConsole;

public class SpectreFigletOptions
{
	public Justify Alignment { get; set; } = Justify.Center;
	public SpectreColorOptions Color { get; set; } = new();
}