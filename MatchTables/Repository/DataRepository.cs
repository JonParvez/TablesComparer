using Microsoft.Extensions.Configuration;

namespace TablesComparer.Repository
{
	public class DataRepository : BaseRepository, IRepository
	{
		public async Task<IEnumerable<Dictionary<string, dynamic>>> GetAddedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			try
			{
				string commandText = $@"SELECT {sourceTable2}.* FROM {sourceTable2}
										LEFT JOIN {sourceTable1} 
										ON {sourceTable1}.{primaryKey} = {sourceTable2}.{primaryKey}
										where {sourceTable1}.{primaryKey} IS NULL";
				return await GetRecordsAsync(commandText);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<IEnumerable<Dictionary<string, dynamic>>> GetRemovedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			string commandText = $@"SELECT {sourceTable1}.* FROM {sourceTable1}
									LEFT JOIN {sourceTable2} 
									ON {sourceTable1}.{primaryKey} = {sourceTable2}.{primaryKey}
									WHERE {sourceTable2}.{primaryKey} IS NULL";
			return await GetRecordsAsync(commandText);
		}

		public async Task<IEnumerable<Dictionary<string, dynamic>>> GetModifiedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			string commandText = $@"(SELECT {sourceTable1}.* FROM {sourceTable1}
									INNER JOIN {sourceTable2} ON {sourceTable2}.{primaryKey} = {sourceTable1}.{primaryKey})
									EXCEPT
									(SELECT {sourceTable2}.* FROM {sourceTable1}
									INNER JOIN {sourceTable2}
									ON ({sourceTable1}.{primaryKey} = {sourceTable2}.{primaryKey}))";
			return await GetRecordsAsync(commandText);
		}

		public async Task<IEnumerable<Dictionary<string, dynamic>>> GetSpecificRecordsAsync(string sourceTable, string primaryKeyName, IEnumerable<string> primaryKeys)
		{
			string commandText = $@"SELECT {sourceTable}.* FROM {sourceTable}
									WHERE {sourceTable}.{primaryKeyName} IN ({string.Join(",", primaryKeys)})";
			return await GetRecordsAsync(commandText);
		}

		
	}
}
