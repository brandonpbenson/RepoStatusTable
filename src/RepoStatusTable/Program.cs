using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RepoStatusTable
{
	internal static class Program
	{
		private static async Task Main()
		{
			var serviceProvider = Bindings.CreateBindings();
			var app = serviceProvider.GetRequiredService<IApplication>();
			await app.RunAsync();
		}
	}
}