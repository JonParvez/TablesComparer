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
			serviceCollection.AddScoped<IComparerService, ComparerService>();
			serviceCollection.AddScoped<IDataRepository, DataRepository>();
			return serviceCollection.BuildServiceProvider();
		}
	}
}
