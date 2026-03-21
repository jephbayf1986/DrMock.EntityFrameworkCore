using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Moq;

namespace DrMock.EfCore.Tests.VerifyTests
{
    public class VerifyAddedTests
    {
        [Fact]
        public void GivenInterface_WhenAddedDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.Add(newPerson);

            // Assert
            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyAddedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            Should.Throw<Exception>(() => mock.VerifyNeverAdded<Person>(x => x.FirstName == firstName
                                                                          && x.LastName == lastName));

            mock.VerifyNeverAdded<Person>(x => x.FirstName == string.Empty
                                            && x.LastName == string.Empty);
        }

        [Fact]
        public void GivenClass_WhenAddedDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.Add(newPerson);

            // Assert
            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyAddedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            Should.Throw<Exception>(() => mock.VerifyNeverAdded<Person>(x => x.FirstName == firstName
                                                                          && x.LastName == lastName));

            mock.VerifyNeverAdded<Person>(x => x.FirstName == string.Empty
                                            && x.LastName == string.Empty);
        }

        [Fact]
        public async Task GivenInterface_WhenAddedAsyncDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            await sut.AddAsync(newPerson);

            // Assert
            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyAddedOnceAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            Should.Throw<Exception>(() => mock.VerifyNeverAdded<Person>(x => x.FirstName == firstName
                                                                          && x.LastName == lastName));

            mock.VerifyNeverAdded<Person>(x => x.FirstName == string.Empty
                                            && x.LastName == string.Empty);
        }

        [Fact]
        public async Task GivenClass_WhenAddedAsyncDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            await sut.AddAsync(newPerson);

            // Assert
            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyAddedOnceAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            Should.Throw<Exception>(() => mock.VerifyNeverAdded<Person>(x => x.FirstName == firstName
                                                                          && x.LastName == lastName));

            mock.VerifyNeverAdded<Person>(x => x.FirstName == string.Empty
                                            && x.LastName == string.Empty);
        }

        [Fact]
        public void GivenInterface_WhenAddedOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.People.Add(newPerson);

            // Assert
            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyAddedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            Should.Throw<Exception>(() => mock.VerifyNeverAdded<Person>(x => x.FirstName == firstName
                                                                          && x.LastName == lastName));

            mock.VerifyNeverAdded<Person>(x => x.FirstName == string.Empty
                                            && x.LastName == string.Empty);
        }

        [Fact]
        public void GivenClass_WhenAddedOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.People.Add(newPerson);

            // Assert
            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyAddedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            Should.Throw<Exception>(() => mock.VerifyNeverAdded<Person>(x => x.FirstName == firstName
                                                                          && x.LastName == lastName));

            mock.VerifyNeverAdded<Person>(x => x.FirstName == string.Empty
                                            && x.LastName == string.Empty);
        }
    }
}
