namespace TablesComparer.Service
{
	public interface IComparerService
	{
		Task<string> GetAddedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey);
	}
}
