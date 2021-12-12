using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RepoStatusTable.Options.CellProvider;

[SuppressMessage( "ReSharper", "UnusedMemberInSuper.Global",
	Justification = "Used as template and for documentation" )]
public interface ICellProviderOptions
{
	/// <summary>
	///     Specifies whether the information provided by the cell provider should be shown in the table output
	/// </summary>
	/// <value>True to enable, false to disable</value>
	[Required]
	public bool Enable { get; }

	/// <summary>
	///     Absolute position of the column in the table
	/// </summary>
	/// <remarks>
	///     Specifies the order of the columns.
	///     Columns with an explicit position will appear first.
	///     If two columns want the same position, the order is unspecified.
	/// </remarks>
	public int? Position { get; }

	/// <summary>
	///     Optional alternative column heading that appears in the output model
	/// </summary>
	public string Heading { get; }
}