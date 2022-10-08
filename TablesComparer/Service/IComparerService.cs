namespace TablesComparer.Service
{
	public interface IComparerService
	{
		Task<string> GetAddedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey);
		Task<string> GetDeletedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey);
		Task<string> GetModifiedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey);
	}
}
