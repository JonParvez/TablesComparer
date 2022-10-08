using System.Text;

namespace TablesComparer.Utility.Extensions
{
	public static class ConverterExtension
	{
		public static string ConvertAddedOrRemovedRecordsToStringValue(this IEnumerable<Dictionary<string, dynamic>> records, string primaryKey)
		{
			StringBuilder stringBuilder = new();
			foreach (var record in records)
			{
				int totalColumnCounter = 0;
				foreach (var key in record.Keys)
				{
					if (totalColumnCounter == 2)
					{
						break;
					}
					if (key == primaryKey)
					{
						stringBuilder.Append($"   *  {record[primaryKey]} ( ");
						continue;
					}
					stringBuilder.Append($"{record[key]} ");
					totalColumnCounter++;
				}
				stringBuilder.Append(") \n");
			}
			return stringBuilder.ToString();
		}

		public static string ConvertModifiedRecordsToStringValue(this IEnumerable<Dictionary<string, dynamic>> oldRecords, IEnumerable<Dictionary<string, dynamic>> newRecords, string primaryKey)
		{
			StringBuilder stringBuilder = new();
			foreach (var oldRecord in oldRecords)
			{
				var newRecord = newRecords.First(m => m[primaryKey] == oldRecord[primaryKey]);
				foreach (var item in oldRecord)
				{
					if (item.Key != primaryKey && oldRecord[item.Key] != newRecord[item.Key])
					{
						stringBuilder.Append($"   *  {oldRecord[primaryKey]} - {item.Key} has changed from '{oldRecord[item.Key]}' to '{newRecord[item.Key]}' \n");
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
