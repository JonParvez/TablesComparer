using System.Collections.Generic;

namespace MatchTables_Test
{
	public class MockData
	{
		public const string sourceTable1Name = "SourceTable1";
		public const string sourceTable2Name = "SourceTable2";
		public const string primaryKey = "SocialSecurityNumber";
		public List<Dictionary<string, dynamic>> MockSourceRecords { get; } =
			new()
			{
				new Dictionary<string, dynamic>
				{
					{"SocialSecurityNumber", "01010101"},
					{"Firstname" , "Kari"},
					{"Lastname" , "Nordmann"},
					{"Department" , "Sales"}
				}
			};
		public List<Dictionary<string, dynamic>> MockTargetRecords { get; } =
			new()
			{
				new Dictionary<string, dynamic>
				{
					{"SocialSecurityNumber", "01010101"},
					{"Firstname" , "Kari"},
					{"Lastname" , "Nordmann"},
					{"Department" , "Marketing"}
				}
			};

	}
}
