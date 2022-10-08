using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TablesComparer.Repository;
using TablesComparer.Service;
using Xunit;

namespace MatchTables_Test
{
	public class ComparerServiceTest : MockData
	{
		[Fact]
		public async Task GetAddedRecordsAsStringAsync_Should_Return_Records_String()
		{
			//Arrange
			var mockRepository = Substitute.For<IDataRepository>();
			mockRepository.GetAddedRecordsAsync(sourceTable1Name, sourceTable2Name, primaryKey).Returns(MockSourceRecords);
			var comparerService = new ComparerService(mockRepository);

			//Act
			var result = await comparerService.GetAddedRecordsAsStringAsync(sourceTable1Name, sourceTable2Name, primaryKey);

			//Assert
			Assert.IsType<string>(result);
			Assert.NotEmpty(result);
		}

		[Fact]
		public async Task GetAddedRecordsAsStringAsync_Should_Return_No_Record_Added()
		{
			//Arrange
			var mockRepository = Substitute.For<IDataRepository>();
			mockRepository.GetAddedRecordsAsync(sourceTable1Name, sourceTable2Name, primaryKey).Returns(new List<Dictionary<string, dynamic>>());
			var comparerService = new ComparerService(mockRepository);

			//Act
			var result = await comparerService.GetAddedRecordsAsStringAsync(sourceTable1Name, sourceTable2Name, primaryKey);

			//Assert
			Assert.IsType<string>(result);
			Assert.Equal("No records were added", result);
		}

		[Fact]
		public async Task GetDeletedRecordsAsStringAsync_Should_Return_Records_String()
		{
			//Arrange
			var mockRepository = Substitute.For<IDataRepository>();
			mockRepository.GetRemovedRecordsAsync(sourceTable1Name, sourceTable2Name, primaryKey).Returns(MockSourceRecords);
			var comparerService = new ComparerService(mockRepository);

			//Act
			var result = await comparerService.GetDeletedRecordsAsStringAsync(sourceTable1Name, sourceTable2Name, primaryKey);

			//Assert
			Assert.IsType<string>(result);
			Assert.NotEmpty(result);
		}

		[Fact]
		public async Task GetDeletedRecordsAsStringAsync_Should_Return_No_Record_Deleted()
		{
			//Arrange
			var mockRepository = Substitute.For<IDataRepository>();
			mockRepository.GetRemovedRecordsAsync(sourceTable1Name, sourceTable2Name, primaryKey).Returns(new List<Dictionary<string,dynamic>>());
			var comparerService = new ComparerService(mockRepository);

			//Act
			var result = await comparerService.GetDeletedRecordsAsStringAsync(sourceTable1Name, sourceTable2Name, primaryKey);

			//Assert
			Assert.IsType<string>(result);
			Assert.Equal("No records were deleted", result);
		}

		[Fact]
		public async Task GetModifiedRecordsAsStringAsync_Should_Return_No_Record_Modified()
		{
			//Arrange
			var mockRepository = Substitute.For<IDataRepository>();
			mockRepository.GetModifiedRecordsAsync(sourceTable1Name, sourceTable2Name, primaryKey).Returns(new List<Dictionary<string, dynamic>>());
			mockRepository.GetSpecificRecordsAsync(sourceTable2Name, primaryKey, new List<string>()).Returns(MockTargetRecords);
			var comparerService = new ComparerService(mockRepository);

			//Act
			var result = await comparerService.GetModifiedRecordsAsStringAsync(sourceTable1Name, sourceTable2Name, primaryKey);

			//Assert
			Assert.Equal("No records were modified", result);
		}

		[Fact]
		public async Task ValidateInputsAsync_Should_Throw_Exception_SourceTable_Required()
		{
			//Arrange
			var mockRepository = Substitute.For<IDataRepository>();
			var comparerService = new ComparerService(mockRepository);

			//Act
			async Task act() => await comparerService.ValidateInputsAsync(string.Empty, sourceTable2Name, primaryKey);

			//Assert
			ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(act);
			Assert.Equal("SourceTable1 is required!", exception.Message);
		}

		[Fact]
		public async Task ValidateInputsAsync_Should_Throw_Exception_PrimaryKey_Required()
		{
			//Arrange
			var mockRepository = Substitute.For<IDataRepository>();
			var comparerService = new ComparerService(mockRepository);

			//Act
			async Task act() => await comparerService.ValidateInputsAsync(sourceTable1Name, sourceTable2Name, string.Empty);

			//Assert
			ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(act);
			Assert.Equal("PrimaryKey is required!", exception.Message);
		}

		[Fact]
		public async Task ValidateInputsAsync_Should_Throw_Exception_PrimaryKey_Missing()
		{
			//Arrange
			var mockRepository = Substitute.For<IDataRepository>();
			mockRepository.CheckIdenticalAsync(sourceTable1Name, sourceTable2Name).Returns(true);
			mockRepository.HasColumnAsync(sourceTable1Name, primaryKey).Returns(false);
			var comparerService = new ComparerService(mockRepository);

			//Act
			async Task act() => await comparerService.ValidateInputsAsync(sourceTable1Name, sourceTable2Name, primaryKey);

			//Assert
			var exception = await Assert.ThrowsAsync<InvalidDataException>(act);
			Assert.Equal("Primary key is missing in SourceTable1!", exception.Message);
		}

		[Fact]
		public async Task ValidateInputsAsync_Should_Throw_Exception_SourceTables_Not_Identical()
		{
			//Arrange
			var mockRepository = Substitute.For<IDataRepository>();
			mockRepository.CheckIdenticalAsync(sourceTable1Name, sourceTable2Name).Returns(false);
			var comparerService = new ComparerService(mockRepository);

			//Act
			async Task act() => await comparerService.ValidateInputsAsync(sourceTable1Name, sourceTable2Name, primaryKey);

			//Assert
			var exception = await Assert.ThrowsAsync<InvalidDataException>(act);
			Assert.Equal("Source tables are not identical!", exception.Message);
		}
	}
}
