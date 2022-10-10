namespace TablesComparer.Repository
{
	/// <summary>
	/// Data repository for specefic query generation and execution
	/// </summary>
	public class DataRepository : SqlDataProvider, IDataRepository
	{
		/// <summary>
		/// Get added records
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <param name="primaryKey">Primary Key</param>
		/// <returns>Return added records dictionary collection</returns>
		public async Task<IEnumerable<Dictionary<string, dynamic>>> GetAddedRecordsAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			string commandText = $@"SELECT [{sourceTable2}].* FROM [{sourceTable2}]
										LEFT JOIN [{sourceTable1}] 
										ON [{sourceTable1}].[{primaryKey}] = [{sourceTable2}].[{primaryKey}]
										where [{sourceTable1}].[{primaryKey}] IS NULL";
			return await GetRecordsAsync(commandText);
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
			string commandText = $@"SELECT [{sourceTable1}].* FROM [{sourceTable1}]
									LEFT JOIN [{sourceTable2}] 
									ON [{sourceTable1}].[{primaryKey}] = [{sourceTable2}].[{primaryKey}]
									WHERE [{sourceTable2}].[{primaryKey}] IS NULL";
			return await GetRecordsAsync(commandText);
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
			string commandText = $@"(SELECT [{sourceTable1}].* FROM [{sourceTable1}]
									INNER JOIN [{sourceTable2}] ON [{sourceTable2}].[{primaryKey}] = [{sourceTable1}].[{primaryKey}])
									EXCEPT
									(SELECT [{sourceTable2}].* FROM [{sourceTable1}]
									INNER JOIN [{sourceTable2}]
									ON ([{sourceTable1}].[{primaryKey}] = [{sourceTable2}].[{primaryKey}]))";
			return await GetRecordsAsync(commandText);
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
			string commandText = $@"SELECT [{sourceTable}].* FROM [{sourceTable}]
									WHERE [{sourceTable}].[{primaryKeyName}] IN ({string.Join(",", primaryKeys)})";
			return await GetRecordsAsync(commandText);
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

			var isIdentical = await GetScalarAsync<bool?>(commandText);
			return isIdentical is not null and true;
		}

		/// <summary>
		/// Check if table contains provided column
		/// </summary>
		/// <param name="tableName">Table Name</param>
		/// <param name="columnName">Column Name</param>
		/// <returns>Return boolean result</returns>
		public async Task<bool> CheckPrimaryKeyAsync(string tableName, string primaryKey)
		{
			string commandText = $@"SELECT CASE 
										WHEN EXISTS
										(
											SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS T  
											JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE C  
											ON C.CONSTRAINT_NAME = T.CONSTRAINT_NAME  
											WHERE C.TABLE_NAME='{tableName}' COLLATE SQL_Latin1_General_CP1_CS_AS 
											AND T.CONSTRAINT_TYPE='PRIMARY KEY' 
											AND C.COLUMN_NAME = '{primaryKey}' COLLATE SQL_Latin1_General_CP1_CS_AS
										)
										THEN CAST(1 AS BIT)
										ELSE CAST(0 AS BIT) END";
			var hasPrimaryKey = await GetScalarAsync<bool?>(commandText);
			return hasPrimaryKey is not null and true;
		}

		/// <summary>
		/// Check table exists or not
		/// </summary>
		/// <param name="tableName">Table name</param>
		/// <returns>Return boolean result</returns>
		public async Task<bool> TableExistsAsync(string tableName)
		{
			string commandText = $@"SELECT CASE 
										WHEN EXISTS
										(
											SELECT * FROM INFORMATION_SCHEMA.TABLES 
											WHERE TABLE_NAME = N'{tableName}' COLLATE SQL_Latin1_General_CP1_CS_AS
										)
										THEN CAST(1 AS BIT)
										ELSE CAST(0 AS BIT) END";
			var exists = await GetScalarAsync<bool?>(commandText);
			return exists is not null and true;
		}
	}
}
