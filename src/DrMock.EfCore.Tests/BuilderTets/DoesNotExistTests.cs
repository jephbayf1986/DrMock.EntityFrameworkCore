using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;

namespace DrMock.EfCore.Tests.BuilderTets
{
    public class DoesNotExistTests
    {
        [Fact]
        public void GivenInterface_OnInclude_CreateDbSetWithSomeData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);

            var person = new Person()
            {
                Id = personId,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
            };

            var mock = new MockDbContext<ITestDbContext>()
                .Include(person)
                .DoesNotExist<Person>(x => x.Id == personId);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldNotContain(x => x.Id == personId);
            personFound.ShouldBeNull();
        }

        [Fact]
        public void GivenClass_OnInclude_CreateDbSetWithSomeData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);

            var person = new Person()
            {
                Id = personId,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
            };

            var mock = new MockDbContext<TestDbContext>()
                .Include(person)
                .DoesNotExist<Person>(x => x.Id == personId);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldNotContain(x => x.Id == personId);
            personFound.ShouldBeNull();
        }
    }
}