using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Moq;

namespace DrMock.EfCore.Tests.VerifyTests
{
    public class VerifyRangeAddedAsyncTests
    {
        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            await sut.AddRangeAsync(newPeople);

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                    && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                      && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            await sut.AddRangeAsync(newPeople);

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                    && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                      && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            await sut.People.AddRangeAsync(newPeople);

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                      && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            await sut.People.AddRangeAsync(newPeople);

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                      && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncDirect_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            await sut.AddRangeAsync(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                      && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncDirect_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            await sut.AddRangeAsync(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                      && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncOnDbSet_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            await sut.People.AddRangeAsync(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                      && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncOnDbSet_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            await sut.People.AddRangeAsync(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAddedAsync<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                      && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }
    
        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncMultipleTimesDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.AddRangeAsync(newPeople);
            }

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                   && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncMultipleTimesDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.AddRangeAsync(newPeople);
            }

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                   && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncMultipleTimesOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.People.AddRangeAsync(newPeople);
            }

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                   && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncMultipleTimesOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.People.AddRangeAsync(newPeople);
            }

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                   && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncMultipleTimesDirect_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.AddRangeAsync(newPerson1, newPerson2);
            }

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                   && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncMultipleTimesDirect_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.AddRangeAsync(newPerson1, newPerson2);
            }

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                   && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncMultipleTimesOnDbSet_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.People.AddRangeAsync(newPerson1, newPerson2);
            }

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                   && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncMultipleTimesOnDbSet_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                await sut.People.AddRangeAsync(newPerson1, newPerson2);
            }

            // Assert
            mock.VerifyRangeAddedAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeAddedOnceAsync<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                   && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }
    }
}