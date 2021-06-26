using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepoStatusTable.Facade;
using RepoStatusTable.Options;
using RepoStatusTable.Renderer;

namespace RepoStatusTable
{
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

			// Output Rendering
			_collection.AddSingleton<ITableRenderer, SpectreConsoleTableRenderer>();

			// Facades
			_collection.AddSingleton<IVscFacade, GitFacade>();
			_collection.AddSingleton<IGitFacade, GitFacade>();
			_collection.AddSingleton<IFileSystemFacade, FileSystemFacade>();

			return this;
		}

		private static IConfigurationRoot ConfigureConfiguration()
		{
			var configurationBuilder = new ConfigurationBuilder();

			configurationBuilder.Sources.Clear();
			configurationBuilder.AddJsonFile( "config.dev.json" );

			return configurationBuilder.Build();
		}

		public ServiceProviderBuilder ConfigureOptions()
		{
			var configurationRoot = ConfigureConfiguration();
			_collection.Configure<RepoOptions>( configurationRoot.GetSection( "Repos" ) );
			return this;
		}
	}
}