using System.Threading.Tasks;

namespace RepoStatusTable.View
{
	public interface ITableView
	{
		public Task RenderAsync();
	}
}