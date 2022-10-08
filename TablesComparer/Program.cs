using Microsoft.Extensions.DependencyInjection;
using TablesComparer.Service;
using TablesComparer.Utility;

#region Service Configuration for Dependency Injection

var serviceProvider = ServiceInstaller.ConfigureServices();
var comparerService = serviceProvider.GetRequiredService<IComparerService>();

#endregion

#region Operational Functionalities

Console.WriteLine("Added Records:");
Console.WriteLine(await comparerService.GetAddedRecordsAsStringAsync("Test", "Test2", "Id"));

Console.WriteLine("Deleted Records:");
Console.WriteLine(await comparerService.GetDeletedRecordsAsStringAsync("Test", "Test2", "Id"));

Console.WriteLine("Modified Records:");
Console.WriteLine(await comparerService.GetModifiedRecordsAsStringAsync("Test", "Test2", "Id"));

#endregion

#region Disposal
if (serviceProvider is IDisposable)
{
	((IDisposable)serviceProvider).Dispose();
} 
#endregion
