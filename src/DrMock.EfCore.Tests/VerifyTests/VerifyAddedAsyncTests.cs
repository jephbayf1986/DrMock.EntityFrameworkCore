using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Moq;

namespace DrMock.EfCore.Tests.VerifyTests
{
    public class VerifyAddedAsyncTests
    {
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

            Should.Throw<Exception>(() => mock.VerifyNeverAddedAsync<Person>(x => x.FirstName == firstName
                                                                          && x.LastName == lastName));

            mock.VerifyNeverAddedAsync<Person>(x => x.FirstName == string.Empty
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

            Should.Throw<Exception>(() => mock.VerifyNeverAddedAsync<Person>(x => x.FirstName == firstName
                                                                          && x.LastName == lastName));

            mock.VerifyNeverAddedAsync<Person>(x => x.FirstName == string.Empty
                                            && x.LastName == string.Empty);
        }

        [Fact]
        public async Task GivenInterface_WhenAddedAsyncOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            await sut.People.AddAsync(newPerson);

            // Assert
            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyAddedOnceAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            Should.Throw<Exception>(() => mock.VerifyNeverAddedAsync<Person>(x => x.FirstName == firstName
                                                                               && x.LastName == lastName));

            mock.VerifyNeverAddedAsync<Person>(x => x.FirstName == string.Empty
                                                 && x.LastName == string.Empty);
        }

        [Fact]
        public async Task GivenClass_WhenAddedAsyncOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            await sut.People.AddAsync(newPerson);

            // Assert
            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyAddedOnceAsync<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            Should.Throw<Exception>(() => mock.VerifyNeverAddedAsync<Person>(x => x.FirstName == firstName
                                                                               && x.LastName == lastName));

            mock.VerifyNeverAddedAsync<Person>(x => x.FirstName == string.Empty
                                                 && x.LastName == string.Empty);
        }

        [Fact]
        public async Task GivenInterface_WhenAddedAsyncMultipleTimesDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.AddAsync(newPerson);
            }

            // Assert
            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                            && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyAddedOnceAsync<Person>(x => x.FirstName == firstName
                                                                              && x.LastName == lastName));
        }

        [Fact]
        public async Task GivenClass_WhenAddedAsyncMultipleTimesDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.AddAsync(newPerson);
            }

            // Assert
            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                            && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyAddedOnceAsync<Person>(x => x.FirstName == firstName
                                                                              && x.LastName == lastName));
        }

        [Fact]
        public async Task GivenInterface_WhenAddedAsyncMultipleTimesOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.People.AddAsync(newPerson);
            }

            // Assert
            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                            && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyAddedOnceAsync<Person>(x => x.FirstName == firstName
                                                                              && x.LastName == lastName));
        }

        [Fact]
        public async Task GivenClass_WhenAddedAsyncMultipleTimesOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.People.AddAsync(newPerson);
            }

            // Assert
            mock.VerifyAddedAsync<Person>(x => x.FirstName == firstName
                                            && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyAddedOnceAsync<Person>(x => x.FirstName == firstName
                                                                              && x.LastName == lastName));
        }
    }
}