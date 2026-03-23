using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;

namespace DrMock.EfCore.Tests.BuilderTets
{
    public class WithRandomDataForTests
    {
        [Fact]
        public void GivenInterface_WithRandomDataFor_CreateDbSetWithSomeData()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Payroll>();

            var dbContext = mock.Object;

            // Act
            var payrolls = dbContext.Payrolls.ToList();

            // Assert
            payrolls.ShouldNotBeEmpty();
        }

        [Fact]
        public void GivenInterface_WithRandomDataFor_WithMinimumItems_CreateDbSetWithMinimumAmount()
        {
            // Arrange
            var minimumItems = RandomByteBetween(2, 5);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Payroll>(minimumItems);

            var dbContext = mock.Object;

            // Act
            var payrolls = dbContext.Payrolls.ToList();

            // Assert
            payrolls.Count.ShouldBe(minimumItems);
        }

        [Fact]
        public void GivenClass_WithRandomDataFor_CreateDbSetWithSomeData()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Payroll>();

            var dbContext = mock.Object;

            // Act
            var payrolls = dbContext.Payrolls.ToList();

            // Assert
            payrolls.ShouldNotBeEmpty();
        }

        [Fact]
        public void GivenClass_WithRandomDataFor_WithMinimumItems_CreateDbSetWithMinimumAmount()
        {
            // Arrange
            var minimumItems = RandomByteBetween(2, 5);

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Payroll>(minimumItems);

            var dbContext = mock.Object;

            // Act
            var payrolls = dbContext.Payrolls.ToList();

            // Assert
            payrolls.Count.ShouldBe(minimumItems);
        }
    }
}