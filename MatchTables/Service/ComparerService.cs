﻿using TablesComparer.Repository;
using TablesComparer.Utility;

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

			if (records == null || !records.Any())
				throw new Exception("No records were added");

			return records.ConvertAddedOrRemovedRecordsToStringValue(primaryKey);
		}

		public async Task<string> GetDeletedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			var records = await _repository.GetRemovedRecordsAsync(sourceTable1, sourceTable2, primaryKey);

			if (records == null || !records.Any())
				throw new Exception("No records were deleted");

			return records.ConvertAddedOrRemovedRecordsToStringValue(primaryKey);
		}

		public async Task<string> GetModifiedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			var oldRecordValues = await _repository.GetModifiedRecordsAsync(sourceTable1, sourceTable2, primaryKey);

			if (oldRecordValues == null || !oldRecordValues.Any())
				throw new Exception("No records were modified");

			var newRecordValues = await _repository.GetSpecificRecordsAsync(sourceTable2, primaryKey, oldRecordValues.Select(m => (string)m[primaryKey]));

			return oldRecordValues.ConvertModifiedRecordsToStringValue(newRecordValues, primaryKey);
		}
	}
}