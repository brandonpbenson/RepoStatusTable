using Microsoft.Extensions.DependencyInjection;

namespace RepoStatusTable
{
	internal class Program
	{
		private static void Main()
		{
			var serviceProvider = Bindings.CreateBindings();
			var app = serviceProvider.GetRequiredService<IApplication>();
			app.Run();
		}
	}
}