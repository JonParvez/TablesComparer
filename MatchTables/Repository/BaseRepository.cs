using MatchTables.Repository;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TablesComparer.Repository
{
	/// <summary>
	/// Base Repository For Basic SQL Execution
	/// </summary>
	public class BaseRepository : IBaseRepository
	{
		private readonly IConfiguration _configuration;

		public BaseRepository()
		{
			var builder = new ConfigurationBuilder()
						.SetBasePath(Directory.GetCurrentDirectory())
						.AddJsonFile("appsettings.json");

			_configuration = builder.Build();
		}

		/// <summary>
		/// Get records with provided query
		/// </summary>
		/// <param name="commandText">Query</param>
		/// <returns>Return records dictionary collection as key value pair</returns>
		public async Task<IEnumerable<Dictionary<string, dynamic>>> GetRecordsAsync(string commandText)
		{
			List<Dictionary<string, dynamic>> records = new();
			SqlCommand? command = null;
			try
			{
				command = await CreateDbCommand();
				command.CommandText = commandText;
				using var dataReader = await command.ExecuteReaderAsync();
				while (await dataReader.ReadAsync())
				{
					var record = new Dictionary<string, dynamic>();
					for (int index = 0; index < dataReader.FieldCount; index++)
					{
						record.Add(dataReader.GetName(index), dataReader.GetValue(index));
					}
					records.Add(record);
				}
				return records;
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				CloseConnectionAsync(command);
			}
		}

		/// <summary>
		/// Generic scalar query executor
		/// </summary>
		/// <typeparam name="T">Generic Output Type</typeparam>
		/// <param name="commandText">Query</param>
		/// <returns>Return scalar value of output type</returns>
		public async Task<T?> GetScalarAsync<T>(string commandText)
		{
			SqlCommand? command = null;
			try
			{
				command = await CreateDbCommand();
				command.CommandText = commandText;
				var result = (T?)await command.ExecuteScalarAsync();
				return result;
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				CloseConnectionAsync(command);
			}
		}

		/// <summary>
		/// Create sql command with sql connection
		/// </summary>
		/// <returns>Return created sql command</returns>
		private async Task<SqlCommand> CreateDbCommand()
		{
			using var sqlCommand = new SqlCommand();
			sqlCommand.Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
			await sqlCommand.Connection.OpenAsync();
			return sqlCommand;
		}

		/// <summary>
		/// Close sql connection
		/// </summary>
		/// <param name="command">SQL command</param>
		private static void CloseConnectionAsync(SqlCommand? command)
		{
			if (command?.Connection != null && command.Connection.State == ConnectionState.Open)
			{
				command.Connection.Close();
			}
		}
	}
}
