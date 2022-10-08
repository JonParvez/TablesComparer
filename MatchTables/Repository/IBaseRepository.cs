namespace MatchTables.Repository
{
	public interface IBaseRepository
	{
		Task<IEnumerable<Dictionary<string, dynamic>>> GetRecordsAsync(string commandText);
		Task<T?> GetScalarAsync<T>(string commandText);
	}
}
