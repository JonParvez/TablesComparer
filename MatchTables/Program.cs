using MatchTables.Constants;
using Microsoft.Extensions.DependencyInjection;
using TablesComparer.Service;
using TablesComparer.Utility;

string? sourceTable1 = string.Empty;
string? sourceTable2 = string.Empty;
string? primaryKey = string.Empty;
try
{
	#region Service Configuration for Dependency Injection

	var serviceProvider = ServiceInstaller.ConfigureServices();
	var comparerService = serviceProvider.GetRequiredService<IComparerService>();

	#endregion

	#region Console Input 
	ProcessInput(args);
	#endregion

	#region Validation
	if (string.IsNullOrWhiteSpace(sourceTable1) || string.IsNullOrWhiteSpace(sourceTable2) || string.IsNullOrWhiteSpace(primaryKey))
	{
		throw new Exception("Input arguments are required!");
	}
	#endregion

	#region Operational Functionalities

	Console.WriteLine("Added Records:");
	Console.WriteLine(await comparerService.GetAddedRecordsAsStringAsync(sourceTable1, sourceTable2, primaryKey));

	Console.WriteLine("Deleted Records:");
	Console.WriteLine(await comparerService.GetDeletedRecordsAsStringAsync(sourceTable1, sourceTable2, primaryKey));

	Console.WriteLine("Modified Records:");
	Console.WriteLine(await comparerService.GetModifiedRecordsAsStringAsync(sourceTable1, sourceTable2, primaryKey));

	#endregion

	#region Disposal
	if (serviceProvider is IDisposable disposable)
	{
		disposable.Dispose();
	}
	#endregion
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message + "\n\nTry Again ... ");
}
Console.ReadLine();

#region Private Methods

void ProcessInput(string[] args)
{
	if (args != null && args.Length > 5)
	{
		Console.WriteLine("Reading from console arguments ... ");
		if (args[0] == InputConstants.TABLE_NAME_1)
			sourceTable1 = args[1];
		if (args[2] == InputConstants.TABLE_NAME_2)
			sourceTable2 = args[3];
		if (args[4] == InputConstants.PRIMARY_KEY)
			primaryKey = args[5];
	}
	else
	{
		Console.WriteLine("Reading from console reader ... ");
		Console.Write($"{InputConstants.TABLE_NAME_1} : ");
		sourceTable1 = Console.ReadLine();
		Console.Write($"{InputConstants.TABLE_NAME_2} : ");
		sourceTable2 = Console.ReadLine();
		Console.Write($"{InputConstants.PRIMARY_KEY} : ");
		primaryKey = Console.ReadLine();

		Console.WriteLine("\n");
	}
}

#endregion
