using DrMock.EfCore.Options;
using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;

namespace DrMock.EfCore.Tests.BuilderTets
{
    public class UseEntityTests
    {
        [Fact]
        public void GivenInterface_WhenUseEntity_CreateDbSetWithNoData()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                .UseEntity<Department>();

            var dbContext = mock.Object;

            // Act
            var deparments = dbContext.Departments.ToList();

            // Assert
            deparments.ShouldBeEmpty();
        }

        [Fact]
        public void GivenInterface_WhenUseEntityWithMinimum_CreateDbSetWithMinimumData()
        {
            // Arrange
            var numberToCreate = RandomByteBetween(2, 5);

            var options = new MockDbContextOptions
            {
                MinItemsInDbSet = numberToCreate
            };

            var mock = new MockDbContext<ITestDbContext>(options)
                .UseEntity<Department>();

            var dbContext = mock.Object;

            // Act
            var deparments = dbContext.Departments.ToList();

            // Assert
            deparments.Count.ShouldBeGreaterThanOrEqualTo(numberToCreate);
        }

        [Fact]
        public void GivenClass_WhenUseEntity_CreateDbSetWithNoData()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                .UseEntity<Department>();

            var dbContext = mock.Object;

            // Act
            var deparments = dbContext.Departments.ToList();

            // Assert
            deparments.ShouldBeEmpty();
        }

        [Fact]
        public void GivenClass_WhenUseEntityWithMinimum_CreateDbSetWithMinimumData()
        {
            // Arrange
            var numberToCreate = RandomByteBetween(2, 5);

            var options = new MockDbContextOptions
            {
                MinItemsInDbSet = numberToCreate
            };

            var mock = new MockDbContext<TestDbContext>(options)
                .UseEntity<Department>();

            var dbContext = mock.Object;

            // Act
            var deparments = dbContext.Departments.ToList();

            // Assert
            deparments.Count.ShouldBeGreaterThanOrEqualTo(numberToCreate);
        }
    }
}
