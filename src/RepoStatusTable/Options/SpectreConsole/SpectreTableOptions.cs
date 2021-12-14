using RepoStatusTable.Utilities;

namespace RepoStatusTable.Options.SpectreConsole;

[SuppressMessage( "ReSharper", "UnusedAutoPropertyAccessor.Global" )]
[SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" )]
public class SpectreTableOptions
{
	public Justify Alignment { get; set; } = Justify.Center;

	public string Border { get; set; } = "Ascii";

	public bool Expand { get; set; } = false;

	public SpectreColorOptions Color { get; set; } = new();
}

public static class SpectreTableExtensions
{
	public static TableBorder GetTableBorder( this SpectreTableOptions options )
	{
		return ReflectionUtility.GetValueOfProperty<TableBorder, TableBorder>( options.Border, null );
	}
}