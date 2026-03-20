using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DrMock.EfCore.Tests
{
    public class VerifyTests
    {
        #region Add

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
        }

        #endregion

        #region AddRange

        [Fact]
        public void GivenInterface_WhenRangeAddedDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.AddRange(newPeople);

            // Assert
            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeAddedDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.AddRange(newPeople);

            // Assert
            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

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
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

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
        }

        [Fact]
        public void GivenInterface_WhenRangeAddedOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.People.AddRange(newPeople);

            // Assert
            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeAddedOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.AddRange(newPeople);
            sut.AddRange(newPeople);
            sut.AddRange(newPerson1, newPerson2);
            sut.AddRange(newPerson1, newPerson2);

            sut.People.AddRange(newPeople);
            sut.People.AddRange(newPeople);
            sut.People.AddRange(newPerson1, newPerson2);
            sut.People.AddRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

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
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

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
        }

        [Fact]
        public void GivenInterface_WhenRangeAddedDirect_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.AddRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeAddedDirect_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.AddRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncDirect_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

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
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncDirect_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

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
        }

        [Fact]
        public void GivenInterface_WhenRangeAddedOnDbSet_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.People.AddRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeAddedOnDbSet_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.People.AddRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public async Task GivenInterface_WhenRangeAddedAsyncOnDbSet_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

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
        }

        [Fact]
        public async Task GivenClass_WhenRangeAddedAsyncOnDbSet_WithParams_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

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
        }

        #endregion

        #region Update

        [Fact]
        public void GivenInterface_WhenUpdatedDirect_ShouldVerifyUpdatedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.Update(newPerson);

            // Assert
            mock.VerifyUpdated<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyUpdated<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyUpdatedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);
        }

        [Fact]
        public void GivenClass_WhenUpdatedDirect_ShouldVerifyUpdatedCCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.Update(newPerson);

            // Assert
            mock.VerifyUpdated<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyUpdated<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyUpdatedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);
        }

        [Fact]
        public void GivenInterface_WhenUpdatedOnDbSet_ShouldVerifyUpdatedCCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.People.Update(newPerson);

            // Assert
            mock.VerifyUpdated<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyUpdated<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyUpdatedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);
        }

        [Fact]
        public void GivenClass_WhenUpdatedOnDbSet_ShouldVerifyUpdatedCCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person newPerson = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.People.Update(newPerson);

            // Assert
            mock.VerifyUpdated<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyUpdated<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyUpdatedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);
        }

        #endregion

        #region Update Range

        [Fact]
        public void GivenInterface_WhenRangeUpdatedDirect_ShouldVerifyUpdatedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.UpdateRange(newPeople);

            // Assert
            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeUpdatedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeUpdatedDirect_ShouldVerifyUpdatedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.UpdateRange(newPeople);

            // Assert
            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeUpdatedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenInterface_WhenRangeUpdatedOnDbSet_ShouldVerifyUpdatedCCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.People.UpdateRange(newPeople);

            // Assert
            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeUpdatedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeUpdatedOnDbSet_ShouldVerifyUpdatedCCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.People.UpdateRange(newPeople);

            // Assert
            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeUpdatedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenInterface_WhenRangeUpdatedDirect_WithParams_ShouldVerifyUpdatedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.UpdateRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeUpdatedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeUpdatedDirect_WithParams_ShouldVerifyUpdatedCCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.UpdateRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeUpdatedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenInterface_WhenRangeUpdatedOnDbSet_WithParams_ShouldVerifyUpdatedCCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.People.UpdateRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeUpdatedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeUpdatedOnDbSet_WithParams_ShouldVerifyUpdatedCCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.People.UpdateRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeUpdatedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        #endregion

        #region Remove

        [Fact]
        public void GivenInterface_WhenRemovedDirect_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person personToDelete = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.Remove(personToDelete);

            // Assert
            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyRemovedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);
        }

        [Fact]
        public void GivenClass_WhenRemovedDirect_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person personToDelete = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.Remove(personToDelete);

            // Assert
            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyRemovedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);
        }

        [Fact]
        public void GivenInterface_WhenRemovedOnDbSet_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person personToDelete = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.People.Remove(personToDelete);

            // Assert
            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyRemovedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);
        }

        [Fact]
        public void GivenClass_WhenRemovedOnDbSet_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person personToDelete = new Person() { FirstName = firstName, LastName = lastName };

            // Act
            sut.People.Remove(personToDelete);

            // Assert
            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);

            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Once());

            mock.VerifyRemovedOnce<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName);
        }

        #endregion

        #region RemoveRange

        [Fact]
        public void GivenInterface_WhenRangeRemovedDirect_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.RemoveRange(newPeople);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedDirect_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.RemoveRange(newPeople);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenInterface_WhenRangeRemovedOnDbSet_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.People.RemoveRange(newPeople);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedOnDbSet_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.People.RemoveRange(newPeople);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenInterface_WhenRangeRemovedDirect_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.RemoveRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedDirect_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.RemoveRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenInterface_WhenRangeRemovedOnDbSet_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.People.RemoveRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedOnDbSet_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.People.RemoveRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));
        }

        #endregion
    }
}