using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TablesComparer.Repository
{
	public abstract class BaseRepository
	{
		private readonly IConfiguration _configuration;

		protected BaseRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		protected IDbConnection CreateConnection()
		{
			return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
		}

		protected async Task<IEnumerable<Dictionary<string, dynamic>>> GetRecordsAsync(string query)
		{
			List<Dictionary<string, dynamic>> records;
			SqlConnection? connection = null;
			try
			{
				using (connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
				{
					connection.Open();
					using var sqlCommand = new SqlCommand(query, connection);
					using var dataReader = await sqlCommand.ExecuteReaderAsync();
					records = new();
					int index = 0;
					while (dataReader.Read())
					{
						records.Add(new Dictionary<string, dynamic>()
						{
							{ dataReader.GetName(index), dataReader.GetValue(index) }
						});
						index++;
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
