using System.ComponentModel.DataAnnotations;

namespace RepoStatusTable.Options.CellProvider
{
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
		public string Path { get; set; }

		public bool IgnoreMissingFiles { get; set; } = true;

		public int NumberOfLines { get; set; } = 1;

		/// <inheritdoc />
		public bool Enable { get; set; } = true;

		/// <inheritdoc />
		public int? Position { get; set; }

		/// <inheritdoc />
		public string? Heading { get; set; }
	}
}