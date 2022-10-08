using MatchTables.Repository;

namespace TablesComparer.Repository
{
	/// <summary>
	/// Data repository for specefic query generation and execution
	/// </summary>
	public class DataRepository : IDataRepository
	{
		private readonly IBaseRepository _baseRepository;
		public DataRepository(IBaseRepository baseRepository)
		{
			_baseRepository = baseRepository;
		}

		/// <summary>
		/// Get added records
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <param name="primaryKey">Primary Key</param>
		/// <returns>Return added records dictionary collection</returns>
		public async Task<IEnumerable<Dictionary<string, dynamic>>> GetAddedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			string commandText = $@"SELECT {sourceTable2}.* FROM {sourceTable2}
										LEFT JOIN {sourceTable1} 
										ON {sourceTable1}.{primaryKey} = {sourceTable2}.{primaryKey}
										where {sourceTable1}.{primaryKey} IS NULL";
			return await _baseRepository.GetRecordsAsync(commandText);
		}

		/// <summary>
		/// Get deleted records
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <param name="primaryKey">Primary Key</param>
		/// <returns>Return deleted records dictionary collection</returns>
		public async Task<IEnumerable<Dictionary<string, dynamic>>> GetRemovedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			string commandText = $@"SELECT {sourceTable1}.* FROM {sourceTable1}
									LEFT JOIN {sourceTable2} 
									ON {sourceTable1}.{primaryKey} = {sourceTable2}.{primaryKey}
									WHERE {sourceTable2}.{primaryKey} IS NULL";
			return await _baseRepository.GetRecordsAsync(commandText);
		}

		/// <summary>
		/// Get modified records
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <param name="primaryKey">Primary Key</param>
		/// <returns>Return modified records dictionary collection</returns>
		public async Task<IEnumerable<Dictionary<string, dynamic>>> GetModifiedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			string commandText = $@"(SELECT {sourceTable1}.* FROM {sourceTable1}
									INNER JOIN {sourceTable2} ON {sourceTable2}.{primaryKey} = {sourceTable1}.{primaryKey})
									EXCEPT
									(SELECT {sourceTable2}.* FROM {sourceTable1}
									INNER JOIN {sourceTable2}
									ON ({sourceTable1}.{primaryKey} = {sourceTable2}.{primaryKey}))";
			return await _baseRepository.GetRecordsAsync(commandText);
		}

		/// <summary>
		/// Get specific records of selected primary keys
		/// </summary>
		/// <param name="sourceTable">Source Table</param>
		/// <param name="primaryKeyName">Primary Key Name</param>
		/// <param name="primaryKeys">Primary Key Values</param>
		/// <returns>Return selected records</returns>
		public async Task<IEnumerable<Dictionary<string, dynamic>>> GetSpecificRecordsAsync(string sourceTable, string primaryKeyName, IEnumerable<string> primaryKeys)
		{
			string commandText = $@"SELECT {sourceTable}.* FROM {sourceTable}
									WHERE {sourceTable}.{primaryKeyName} IN ({string.Join(",", primaryKeys)})";
			return await _baseRepository.GetRecordsAsync(commandText);
		}

		/// <summary>
		/// Check identicality of two tables
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <returns>Return boolean value of identicality</returns>
		public async Task<bool> CheckIdenticalAsync(string sourceTable1, string sourceTable2)
		{
			string commandText = $@"SELECT 
										CASE 
											WHEN EXISTS 
											(
												SELECT name, system_type_id, user_type_id,max_length, precision,scale, is_nullable, is_identity
												FROM sys.columns WHERE object_id = OBJECT_ID('{sourceTable1}')
												EXCEPT
												SELECT name, system_type_id, user_type_id,max_length, precision,scale, is_nullable, is_identity
												FROM sys.columns WHERE object_id = OBJECT_ID('{sourceTable2}')
											)
											THEN CAST(0 AS BIT)
											ELSE CAST(1 AS BIT) END";

			var isIdentical = await _baseRepository.GetScalarAsync<bool?>(commandText);
			return isIdentical is not null and true;
		}

		/// <summary>
		/// Check if table contains provided column
		/// </summary>
		/// <param name="tableName">Table Name</param>
		/// <param name="columnName">Column Name</param>
		/// <returns>Return boolean result</returns>
		public async Task<bool> HasColumnAsync(string tableName, string columnName)
		{
			string commandText = $@"SELECT CASE 
										WHEN EXISTS
										(
											SELECT Name FROM sys.columns
											WHERE Name = '{columnName}' AND Object_ID = Object_ID('{tableName}')
										)
										THEN CAST(1 AS BIT)
										ELSE CAST(0 AS BIT) END";
			var hasColumn = await _baseRepository.GetScalarAsync<bool?>(commandText);
			return hasColumn is not null and true;
		}
	}
}
