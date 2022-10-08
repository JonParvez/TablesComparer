using TablesComparer.Repository;
using TablesComparer.Utility.Extensions;

namespace TablesComparer.Service
{
	/// <summary>
	/// Table matcher service
	/// </summary>
	public class ComparerService : IComparerService
	{
		private readonly IDataRepository _repository;
		public ComparerService(IDataRepository repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// Get added records as string formatted
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <param name="primaryKey">Primary Key</param>
		/// <returns>Return string formatted added records result</returns>
		public async Task<string> GetAddedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			var records = await _repository.GetAddedRecordsAsync(sourceTable1, sourceTable2, primaryKey);

			if (records == null || !records.Any())
				return "No records were added";

			return records.ConvertAddedOrRemovedRecordsToStringValue(primaryKey);
		}

		/// <summary>
		/// Get deleted records as string formatted
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <param name="primaryKey">Primary Key</param>
		/// <returns>Return string formatted deleted records result</returns>
		public async Task<string> GetDeletedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			var records = await _repository.GetRemovedRecordsAsync(sourceTable1, sourceTable2, primaryKey);

			if (records == null || !records.Any())
				return "No records were deleted";

			return records.ConvertAddedOrRemovedRecordsToStringValue(primaryKey);
		}

		/// <summary>
		/// Get modified records as string formatted
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <param name="primaryKey">Primary Key</param>
		/// <returns>Return string formatted modified records result</returns>
		public async Task<string> GetModifiedRecordsAsStringAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			var oldRecordValues = await _repository.GetModifiedRecordsAsync(sourceTable1, sourceTable2, primaryKey);

			if (oldRecordValues == null || !oldRecordValues.Any())
				return "No records were modified";

			var newRecordValues = await _repository.GetSpecificRecordsAsync(sourceTable2, primaryKey, oldRecordValues.Select(m => (string)m[primaryKey]));

			return oldRecordValues.ConvertModifiedRecordsToStringValue(newRecordValues, primaryKey);
		}

		/// <summary>
		/// Validate Inputs 
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <param name="primaryKey">Primary Key</param>
		/// <returns></returns>
		public async Task ValidateInputsAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			ValidateInputValuesAsync(sourceTable1, sourceTable2, primaryKey);
			await ValidateIdenticalAsync(sourceTable1, sourceTable2);
			await ValidateTablePrimaryKeyAsync(sourceTable1, sourceTable2, primaryKey);
		}

		/// <summary>
		/// Validate input parameter values 
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <param name="primaryKey">Primary Key</param>
		/// <exception cref="ArgumentException">Argument Exception</exception>
		private static void ValidateInputValuesAsync(string sourceTable1, string sourceTable2, string primaryKey)
		{
			if(string.IsNullOrWhiteSpace(sourceTable1))
				throw new ArgumentException("SourceTable1 is required!");
			if(string.IsNullOrWhiteSpace(sourceTable2))
				throw new ArgumentException("SourceTable2 is required!");
			if(string.IsNullOrWhiteSpace(primaryKey))
				throw new ArgumentException("PrimaryKey is required!");
		}

		/// <summary>
		/// Validate two tables identicality
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <returns></returns>
		/// <exception cref="InvalidDataException">Invalid Data Exception</exception>
		private async Task ValidateIdenticalAsync(string sourceTable1, string sourceTable2)
		{
			if (!await _repository.CheckIdenticalAsync(sourceTable1, sourceTable2))
			{
				throw new InvalidDataException("Source tables are not identical!");
			}
		}

		/// <summary>
		/// Validate two tables' primary key
		/// </summary>
		/// <param name="sourceTable1">Source Table1</param>
		/// <param name="sourceTable2">Source Table2</param>
		/// <param name="primaryKey">Primary Key</param>
		/// <returns></returns>
		/// <exception cref="InvalidDataException">Invalid Data Exception</exception>
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
