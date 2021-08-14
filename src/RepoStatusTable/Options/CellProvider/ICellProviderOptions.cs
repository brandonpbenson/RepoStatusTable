using System.ComponentModel.DataAnnotations;

namespace RepoStatusTable.Options.CellProvider
{
	public interface ICellProviderOptions
	{
		/// <summary>
		///     Specifies whether the information provided by the cell provider should be shown in the table output
		/// </summary>
		/// <value>True to enable, false to disable</value>
		[Required]
		public bool Enable { get; }
	}
}