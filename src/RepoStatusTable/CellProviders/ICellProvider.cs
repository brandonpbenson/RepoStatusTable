using System.Threading.Tasks;

namespace RepoStatusTable.CellProviders;

public interface ICellProvider
{
	/// <summary>
	///     Column headline to be shown in the table output
	/// </summary>
	public string Heading { get; }


	/// <summary>
	///     Determine whether the output of the cell provider should be shown in the output model
	/// </summary>
	/// <returns>True if enabled, false otherwise</returns>
	public bool IsEnabled { get; }

	/// <summary>
	///     The configured position of the column in the output table model
	/// </summary>
	public int? Position { get; }

	/// <summary>
	///     Takes a directory path and returns the corresponding cell for the table
	/// </summary>
	/// <param name="path">Path to a directory which contains a VCS repo</param>
	/// <returns><see cref="Cell" /> with the content belonging to the given <paramref name="path" /></returns>
	public Task<Cell> GetCell( string path );
}