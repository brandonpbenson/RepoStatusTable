using Microsoft.Extensions.DependencyInjection;

namespace RepoStatusTable
{
	public static class Bindings
	{
		public static ServiceProvider CreateBindings()
		{
			return new ServiceProviderBuilder()
				.ConfigureServices()
				.Build();
		}
	}


	public class ServiceProviderBuilder
	{
		private readonly ServiceCollection _collection = new();

		public ServiceProvider Build()
		{
			return _collection.BuildServiceProvider();
		}

		public ServiceProviderBuilder ConfigureServices()
		{
			_collection.AddSingleton<IApplication, Application>();
			return this;
		}
	}
}