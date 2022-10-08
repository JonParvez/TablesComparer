using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TablesComparer.Repository
{
	public abstract class BaseRepository
	{
		private readonly IConfiguration _configuration;

		protected BaseRepository()
		{
			var builder = new ConfigurationBuilder()
						.SetBasePath(Directory.GetCurrentDirectory())
						.AddJsonFile("appsettings.json");

			_configuration = builder.Build();
		}

		protected async Task<IEnumerable<Dictionary<string, dynamic>>> GetRecordsAsync(string query)
		{
			List<Dictionary<string, dynamic>> records = new();
			SqlConnection? connection = null;
			try
			{
				using (connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
				{
					connection.Open();
					using var sqlCommand = new SqlCommand(query, connection);
					using var dataReader = await sqlCommand.ExecuteReaderAsync();
					while (await dataReader.ReadAsync())
					{
						var record = new Dictionary<string, dynamic>();
						for (int index = 0; index < dataReader.FieldCount; index++)
						{
							record.Add(dataReader.GetName(index), dataReader.GetValue(index));
						}
						records.Add(record);
					}
					connection.Close();
					return records;
				}
			}
			catch (Exception)
			{
				if (connection != null && connection.State == ConnectionState.Open)
				{
					connection.Close();
				}
				throw;
			}
		}
	}
}
