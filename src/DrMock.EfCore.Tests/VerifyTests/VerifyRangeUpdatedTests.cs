using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Moq;

namespace DrMock.EfCore.Tests.VerifyTests
{
    public class VerifyRangeUpdatedTests
    {
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
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

            Should.Throw<Exception>(() => mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverUpdated<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }
    }
}