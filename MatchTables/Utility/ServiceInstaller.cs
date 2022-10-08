using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TablesComparer.Repository;
using TablesComparer.Service;

namespace TablesComparer.Utility
{
	public class ServiceInstaller
	{
		public static ServiceProvider ConfigureServices()
		{
			var serviceCollection = new ServiceCollection();
			//serviceCollection.AddSingleton<IConfiguration>();
			serviceCollection.AddScoped<IComparerService, ComparerService>();
			serviceCollection.AddScoped<IRepository, DataRepository>();
			return serviceCollection.BuildServiceProvider();
		}
	}
}
