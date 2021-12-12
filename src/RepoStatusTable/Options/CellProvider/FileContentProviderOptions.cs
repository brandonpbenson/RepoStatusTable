using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RepoStatusTable.Options.CellProvider;

[SuppressMessage( "ReSharper", "UnusedAutoPropertyAccessor.Global" )]
[SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" )]
public class FileContentProviderOptions : ICellProviderOptions
{
	/// <summary>
	///     Relative path to a file contained in each repo
	/// </summary>
	/// <remarks>
	///     The content of the referenced file will be printed in the output model
	/// </remarks>
	/// <example>
	///     If you have a file containing the project version in each repo, you can output it in the table.
	/// </example>
	[Required]
	public string? Path { get; set; }

	/// <summary>
	///     Specifies whether missing and inaccessible files should be ignored.
	///     If set to false, inaccessible file paths will lead to an exception.
	/// </summary>
	public bool IgnoreMissingFiles { get; set; } = true;

	/// <summary>
	///     Number of lines that should be read from the <see cref="Path" /> and added to the output model
	/// </summary>
	public int NumberOfLines { get; set; } = 1;

	/// <inheritdoc />
	public bool Enable { get; set; } = true;

	/// <inheritdoc />
	public int? Position { get; set; }

	/// <inheritdoc />
	public string Heading { get; set; } = "File Content";
}