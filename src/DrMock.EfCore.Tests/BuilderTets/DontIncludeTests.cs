using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;

namespace DrMock.EfCore.Tests.BuilderTets
{
    public class DontIncludeTests
    {
        [Fact]
        public void GivenInterface_DontIncludeWithPreviousInclude_CreateDbSetRemovingPreexistingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var personAge = RandomByteBetween(18, 65);

            var person = new Person()
            {
                Id = personId,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - personAge),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var mock = new MockDbContext<ITestDbContext>()
                .Include(person)
                .Exclude(person);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personResult = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldNotContain(x => x.Id == personId);
            personResult.ShouldBeNull();
        }

        [Fact] 
        public void GivenClass_DontIncludeWithPreviousInclude_CreateDbSetRemovingPreexistingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var personAge = RandomByteBetween(18, 65);

            var person = new Person()
            {
                Id = personId,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - personAge),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var mock = new MockDbContext<TestDbContext>()
                .Include(person)
                .Exclude(person);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personResult = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldNotContain(x => x.Id == personId);
            personResult.ShouldBeNull();
        }
    }
}
