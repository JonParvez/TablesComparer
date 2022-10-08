using TablesComparer.Repository;
using TablesComparer.Utility.Extensions;

namespace TablesComparer.Service
{
	public class ComparerService : IComparerService
	{
		private readonly IDataRepository _repository;
		public ComparerService(IDataRepository repository)
		{
			_repository = repository;
		}

		public async Task<string> GetAddedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			var records = await _repository.GetAddedRecordsAsync(sourceTable1, sourceTable2, primaryKey);

			if (records == null || !records.Any())
				return "No records were added";

			return records.ConvertAddedOrRemovedRecordsToStringValue(primaryKey);
		}

		public async Task<string> GetDeletedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			var records = await _repository.GetRemovedRecordsAsync(sourceTable1, sourceTable2, primaryKey);

			if (records == null || !records.Any())
				return "No records were deleted";

			return records.ConvertAddedOrRemovedRecordsToStringValue(primaryKey);
		}

		public async Task<string> GetModifiedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			var oldRecordValues = await _repository.GetModifiedRecordsAsync(sourceTable1, sourceTable2, primaryKey);

			if (oldRecordValues == null || !oldRecordValues.Any())
				return "No records were modified";

			var newRecordValues = await _repository.GetSpecificRecordsAsync(sourceTable2, primaryKey, oldRecordValues.Select(m => (string)m[primaryKey]));

			return oldRecordValues.ConvertModifiedRecordsToStringValue(newRecordValues, primaryKey);
		}

		public async Task ValidateInputsAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			ValidateInputValuesAsync(sourceTable1, sourceTable2, primaryKey);
			await ValidateIdenticalAsync(sourceTable1, sourceTable2);
			await ValidateTablePrimaryKeyAsync(sourceTable1, sourceTable2, primaryKey);
		}

		private static void ValidateInputValuesAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			if(string.IsNullOrWhiteSpace(sourceTable1))
				throw new ArgumentNullException("SourceTable1 is required!");
			if(string.IsNullOrWhiteSpace(sourceTable2))
				throw new ArgumentNullException("SourceTable2 is required!");
			if(string.IsNullOrWhiteSpace(primaryKey))
				throw new ArgumentNullException("PrimaryKey is required!");
		}

		private async Task ValidateIdenticalAsync(string sourceTable1, string sourceTable2)
		{
			if (!await _repository.CheckIdenticalAsync(sourceTable1, sourceTable2))
			{
				throw new InvalidDataException("Source tables are not identical!");
			}
		}

		private async Task ValidateTablePrimaryKeyAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			if (!await _repository.HasColumnAsync(sourceTable1, primaryKey))
			{
				throw new InvalidDataException("Primary key is missing in SourceTable1!");
			}
			if (!await _repository.HasColumnAsync(sourceTable2, primaryKey))
			{
				throw new InvalidDataException("Primary key is missing in SourceTable2!");
			}
		}
	}
}
