using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Moq;

namespace DrMock.EfCore.Tests.VerifyTests
{
    public class VerifyRangeAddedTests
    {
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                            && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                            && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                            && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                            && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                            && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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
            sut.People.AddRange(newPeople);

            // Assert
            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeAddedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                               && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverAdded<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                            && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }
    }
}