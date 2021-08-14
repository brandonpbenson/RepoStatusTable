namespace RepoStatusTable.View
{
	public interface ITableViewStrategy : ITableView
	{
		public string RenderMethod { get; }
	}
}