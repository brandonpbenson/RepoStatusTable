using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RepoStatusTable
{
	internal static class Program
	{
		private static async Task Main()
		{
			SetUpExceptionHandling();
			await StartApp();
		}

		private static async Task StartApp()
		{
			var serviceProvider = Bindings.CreateBindings();
			var app = serviceProvider.GetRequiredService<IApplication>();
			await app.RunAsync();
		}

		private static void SetUpExceptionHandling()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.UnhandledException += ExceptionHandler.Handler;
		}
	}
}