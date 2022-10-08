namespace TablesComparer.Repository
{
	public interface IDataRepository
	{
		Task<IEnumerable<Dictionary<string, dynamic>>> GetAddedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey);
		Task<IEnumerable<Dictionary<string, dynamic>>> GetRemovedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey);
		Task<IEnumerable<Dictionary<string, dynamic>>> GetModifiedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey);
		Task<IEnumerable<Dictionary<string, dynamic>>> GetSpecificRecordsAsync(string sourceTable, string primaryKeyName, IEnumerable<string> primaryKeys);
		Task<bool> CheckIdenticalAsync(string sourceTable1, string sourceTable2);
		Task<bool> HasColumnAsync(string tableName, string columnName);
		Task<bool> TableExistsAsync(string tableName);
	}
}
