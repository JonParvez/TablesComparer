using System.Collections.Generic;

namespace MatchTables_Test
{
	public class MockData
	{
		public static string sourceTable1Name = "SourceTable1";
		public static string sourceTable2Name = "SourceTable2";
		public static string primaryKey = "socialsecuritynumber";
		public static List<Dictionary<string, dynamic>> mockSourceRecords =
			new()
			{
				new Dictionary<string, dynamic>
				{
					{"socialsecuritynumber", "01010101"},
					{"Firstname" , "Kari"},
					{"Lastname" , "Nordmann"},
					{"Department" , "Sales"}
				}
			};
		public static List<Dictionary<string, dynamic>> mockTargetRecords =
			new()
			{
				new Dictionary<string, dynamic>
				{
					{"socialsecuritynumber", "01010101"},
					{"Firstname" , "Kari"},
					{"Lastname" , "Nordmann"},
					{"Department" , "Marketing"}
				}
			};
	}
}
