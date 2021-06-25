using System;

namespace RepoStatusTable
{
	public interface IApplication
	{
		public void Run();
	}

	public class Application : IApplication
	{
		public void Run()
		{
			throw new NotImplementedException();
		}
	}
}