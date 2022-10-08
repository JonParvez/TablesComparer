using MatchTables.Repository;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using TablesComparer.Repository;
using Xunit;

namespace MatchTables_Test
{
	public class DataRepositoryTest : MockData
	{
		[Fact]
		public async Task GetAddedRecordsAsync_Should_Return_Empty()
		{
			//Arrange
			var mockBaseRepository = Substitute.For<IBaseRepository>();
			mockBaseRepository.GetRecordsAsync("dummyQuery").Returns(new List<Dictionary<string, dynamic>>());
			var mockDataRepository = new DataRepository(mockBaseRepository);

			//Act
			var records = await mockDataRepository.GetAddedRecordsAsync("SourceTable1", "SourceTable2", "socialsecuritynumber");

			//Assert
			Assert.Empty(records);
		}

		[Fact]
		public async Task GetRemovedRecordsAsync_Should_Return_Empty()
		{
			//Arrange
			var mockBaseRepository = Substitute.For<IBaseRepository>();
			mockBaseRepository.GetRecordsAsync("dummyQuery").Returns(new List<Dictionary<string, dynamic>>());
			var mockDataRepository = new DataRepository(mockBaseRepository);

			//Act
			var records = await mockDataRepository.GetRemovedRecordsAsync("SourceTable1", "SourceTable2", "socialsecuritynumber");

			//Assert
			Assert.Empty(records);
		}

		[Fact]
		public async Task GetModifiedRecordsAsync_Should_Return_Empty()
		{
			//Arrange
			var mockBaseRepository = Substitute.For<IBaseRepository>();
			mockBaseRepository.GetRecordsAsync("dummyQuery").Returns(new List<Dictionary<string, dynamic>>());
			var mockDataRepository = new DataRepository(mockBaseRepository);

			//Act
			var records = await mockDataRepository.GetModifiedRecordsAsync("SourceTable1", "SourceTable2", "socialsecuritynumber");

			//Assert
			Assert.Empty(records);
		}

		[Fact]
		public async Task CheckIdenticalAsync_Should_Return_False()
		{
			//Arrange
			var mockBaseRepository = Substitute.For<IBaseRepository>();
			mockBaseRepository.GetScalarAsync<bool>("dummyQuery").Returns(false);
			var mockDataRepository = new DataRepository(mockBaseRepository);

			//Act
			var result = await mockDataRepository.CheckIdenticalAsync("SourceTable1", "SourceTable2");

			//Assert
			Assert.False(result);
		}

		[Fact]
		public async Task HasColumnAsync_Should_Return_False()
		{
			//Arrange
			var mockBaseRepository = Substitute.For<IBaseRepository>();
			mockBaseRepository.GetScalarAsync<bool>("dummyQuery").Returns(true);
			var mockDataRepository = new DataRepository(mockBaseRepository);

			//Act
			var result = await mockDataRepository.HasColumnAsync("SourceTable1", "SourceTable2");

			//Assert
			Assert.False(result);
		}
	}
}