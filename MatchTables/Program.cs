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

	#region Input 

	ProcessInput(args);

	#endregion

	#region Input and Table Schema Validation

	await comparerService.ValidateInputsAsync(sourceTable1, sourceTable2, primaryKey);

	#endregion

	#region Operational Functionalities

	Console.WriteLine("Added Records:");
	Console.WriteLine(await comparerService.GetAddedRecordsAsStringAsync(sourceTable1, sourceTable2, primaryKey));

	Console.WriteLine("Removed Records:");
	Console.WriteLine(await comparerService.GetDeletedRecordsAsStringAsync(sourceTable1, sourceTable2, primaryKey));

	Console.WriteLine("Changes:");
	Console.WriteLine(await comparerService.GetModifiedRecordsAsStringAsync(sourceTable1, sourceTable2, primaryKey));

	#endregion

	#region Service Disposal
	if (serviceProvider is IDisposable disposable)
	{
		disposable.Dispose();
	}
	#endregion
}
catch (Exception ex)
{
	Console.ForegroundColor = ConsoleColor.Red;
	Console.WriteLine(ex.Message + "\nTry Again ... ");
	Console.ForegroundColor = ConsoleColor.White;
}
Console.WriteLine("\nInsert any key to close the application ...");
Console.ReadKey();

#region Private Methods

void ProcessInput(string[] args)
{
	//If parameters are passed by arguments
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
	//If parameters are inputted by console reader
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
