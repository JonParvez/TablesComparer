namespace TablesComparer.Repository
{
	public interface IRepository
	{
		Task<IEnumerable<Dictionary<string, dynamic>>> GetAddedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey);
		Task<IEnumerable<Dictionary<string, dynamic>>> GetRemovedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey);
		Task<IEnumerable<Dictionary<string, dynamic>>> GetModifiedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey);
		Task<IEnumerable<Dictionary<string, dynamic>>> GetSpecificRecordsAsync(string sourceTable, string primaryKeyName, List<string> primaryKeys);
	}
}
