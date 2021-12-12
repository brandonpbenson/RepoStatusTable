using Microsoft.Extensions.DependencyInjection;

namespace RepoStatusTable.DependencyInjection;

public static class Bindings
{
	public static ServiceProvider CreateBindings()
	{
		return new ServiceProviderBuilder()
			.ConfigureOptions()
			.ConfigureServices()
			.Build();
	}
}