using Microsoft.Extensions.DependencyInjection;
using TablesComparer.Service;
using TablesComparer.Utility;

Console.WriteLine("Hello, World!");

var serviceProvider = ServiceInstaller.ConfigureServices();

var comparerService = serviceProvider.GetRequiredService<IComparerService>();

var addedRecordsString = await comparerService.GetAddedRecordsAsStringAsync("Test", "Test2", "Id");
Console.WriteLine(addedRecordsString);

if (serviceProvider is IDisposable)
{
    ((IDisposable)serviceProvider).Dispose();
}

Console.ReadKey();
