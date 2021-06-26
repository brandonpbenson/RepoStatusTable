using System.Threading.Tasks;

namespace RepoStatusTable.Renderer
{
	public interface ITableRenderer
	{
		public Task RenderAsync();
	}
}