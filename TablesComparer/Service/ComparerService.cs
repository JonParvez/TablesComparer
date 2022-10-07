using System.Text;
using TablesComparer.Repository;

namespace TablesComparer.Service
{
	public class ComparerService : IComparerService
	{
		private readonly IRepository _repository;
		public ComparerService(IRepository repository)
		{
			_repository = repository;
		}

		public async Task<string> GetAddedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			var records = await _repository.GetAddedRecordsAsync(sourceTable1, sourceTable2, primaryKey);
			StringBuilder stringBuilder = new();
			foreach (var record in records)
			{
				//stringBuilder.Append($"   •  {record[primaryKey]} ({record.ElementAt(1)} {record.ElementAt(1)}) \n");
				stringBuilder.Append($"   *  {record[primaryKey]} ({record.ElementAt(1).Value}) \n");
			}
			return stringBuilder.ToString();
		}
	}
}
