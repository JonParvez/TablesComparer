using MatchTables.Repository;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TablesComparer.Repository
{
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

		public async Task<IEnumerable<Dictionary<string, dynamic>>> GetRecordsAsync(string commandText)
		{
			List<Dictionary<string, dynamic>> records = new();
			SqlCommand? command = null;
			try
			{
				command = CreateDbCommand();
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

		public async Task<T?> GetScalarAsync<T>(string commandText)
		{
			SqlCommand? command = null;
			try
			{
				command = CreateDbCommand();
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

		private SqlCommand CreateDbCommand()
		{
			using var sqlCommand = new SqlCommand();
			sqlCommand.Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
			sqlCommand.Connection.Open();
			return sqlCommand;
		}

		private static void CloseConnectionAsync(SqlCommand? command)
		{
			if (command?.Connection != null && command.Connection.State == ConnectionState.Open)
			{
				command.Connection.Close();
			}
		}
	}
}
