namespace RepoStatusTable.Model;

public interface ITableModel
{
	/// <summary>
	///     Column headings of each cell provider
	/// </summary>
	/// <returns>
	///     A list of all column headings
	/// </returns>
	IEnumerable<string> GetHeadings();

	/// <summary>
	///     Table structure containing all rows of the table.
	///     Each row represents a VCS repository and contains multiple cells.
	///     Each cell contains content from a branch provider.
	/// </summary>
	/// <returns>
	///     Table as a list of rows each containing cells
	/// </returns>
	IAsyncEnumerable<IAsyncEnumerable<string>> GetTableAsync();
}