using DrMock.EfCore.Options;
using DrMock.EfCore.Tests.Contexts;

namespace DrMock.EfCore.Tests.BuilderTets
{
    public class UseAllEntitiesTests
    {
        [Fact]
        public void GivenInterface_WhenUseAllEntities_CreateEachDbSetWithNoData()
        {
            // Arrange
            var mock = MockDbContext<ITestDbContext>.UseAllEntities();
            
            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var payrolls = dbContext.Payrolls.ToList();
            var deparments = dbContext.Departments.ToList();

            // Assert
            people.ShouldBeEmpty();
            payrolls.ShouldBeEmpty();
            deparments.ShouldBeEmpty();
        }

        [Fact]
        public void GivenInterface_WhenUseAllEntitiesWithMinimum_CreateEachDbSetWithMinimumData()
        {
            // Arrange
            var numberToCreate = RandomByteBetween(2, 5);

            var options = new MockDbContextOptions
            {
                MinItemsInDbSet = numberToCreate
            };

            var mock = MockDbContext<ITestDbContext>.UseAllEntities(options);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var payrolls = dbContext.Payrolls.ToList();
            var deparments = dbContext.Departments.ToList();

            // Assert
            people.Count.ShouldBeGreaterThanOrEqualTo(numberToCreate);
            payrolls.Count.ShouldBeGreaterThanOrEqualTo(numberToCreate);
            deparments.Count.ShouldBeGreaterThanOrEqualTo(numberToCreate);
        }

        [Fact]
        public void GivenClass_WhenUseAllEntities_CreateEachDbSetWithNoData()
        {
            // Arrange
            var mock = MockDbContext<TestDbContext>.UseAllEntities();

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var payrolls = dbContext.Payrolls.ToList();
            var deparments = dbContext.Departments.ToList();

            // Assert
            people.ShouldBeEmpty();
            payrolls.ShouldBeEmpty();
            deparments.ShouldBeEmpty();
        }

        [Fact]
        public void GivenClass_WhenUseAllEntitiesWithMinimum_CreateEachDbSetWithMinimumData()
        {
            // Arrange
            var numberToCreate = RandomByteBetween(2, 5);

            var options = new MockDbContextOptions
            {
                MinItemsInDbSet = numberToCreate
            };

            var mock = MockDbContext<TestDbContext>.UseAllEntities(options);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var payrolls = dbContext.Payrolls.ToList();
            var deparments = dbContext.Departments.ToList();

            // Assert
            people.Count.ShouldBeGreaterThanOrEqualTo(numberToCreate);
            payrolls.Count.ShouldBeGreaterThanOrEqualTo(numberToCreate);
            deparments.Count.ShouldBeGreaterThanOrEqualTo(numberToCreate);
        }
    }
}