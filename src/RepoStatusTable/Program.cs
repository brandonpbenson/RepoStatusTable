using RepoStatusTable.DependencyInjection;

namespace RepoStatusTable;

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
		var currentDomain = AppDomain.CurrentDomain;
		currentDomain.UnhandledException += ExceptionHandler.Handler;
	}
}