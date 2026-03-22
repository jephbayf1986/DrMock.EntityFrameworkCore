using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Moq;

namespace DrMock.EfCore.Tests.VerifyTests
{
    public class VerifyRemovedTests
    {
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

            Should.Throw<Exception>(() => mock.VerifyNeverRemoved<Person>(x => x.FirstName == firstName
                                                                            && x.LastName == lastName));

            mock.VerifyNeverRemoved<Person>(x => x.FirstName == string.Empty
                                              && x.LastName == string.Empty);
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

            Should.Throw<Exception>(() => mock.VerifyNeverRemoved<Person>(x => x.FirstName == firstName
                                                                            && x.LastName == lastName));

            mock.VerifyNeverRemoved<Person>(x => x.FirstName == string.Empty
                                              && x.LastName == string.Empty);
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

            Should.Throw<Exception>(() => mock.VerifyNeverRemoved<Person>(x => x.FirstName == firstName
                                                                            && x.LastName == lastName));

            mock.VerifyNeverRemoved<Person>(x => x.FirstName == string.Empty
                                              && x.LastName == string.Empty);
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

            Should.Throw<Exception>(() => mock.VerifyNeverRemoved<Person>(x => x.FirstName == firstName
                                                                            && x.LastName == lastName));

            mock.VerifyNeverRemoved<Person>(x => x.FirstName == string.Empty
                                              && x.LastName == string.Empty);
        }

        [Fact]
        public void GivenInterface_WhenRemovedMultipleTimesDirect_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person personToDelete = new Person() { FirstName = firstName, LastName = lastName };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.Remove(personToDelete);
            }

            // Assert
            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                         && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRemovedOnce<Person>(x => x.FirstName == firstName
                                                                           && x.LastName == lastName));
        }

        [Fact]
        public void GivenClass_WhenRemovedMultipleTimesDirect_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person personToDelete = new Person() { FirstName = firstName, LastName = lastName };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.Remove(personToDelete);
            }

            // Assert
            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                         && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRemovedOnce<Person>(x => x.FirstName == firstName
                                                                           && x.LastName == lastName));
        }

        [Fact]
        public void GivenInterface_WhenRemovedMultipleTimesOnDbSet_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person personToDelete = new Person() { FirstName = firstName, LastName = lastName };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.People.Remove(personToDelete);
            }

            // Assert
            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                         && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRemovedOnce<Person>(x => x.FirstName == firstName
                                                                           && x.LastName == lastName));
        }

        [Fact]
        public void GivenClass_WhenRemovedMultipleTimesOnDbSet_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithEntity<Person>();

            var sut = mock.Object;

            var firstName = RandomFirstName();
            var lastName = RandomLastName();

            Person personToDelete = new Person() { FirstName = firstName, LastName = lastName };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.People.Remove(personToDelete);
            }

            // Assert
            mock.VerifyRemoved<Person>(x => x.FirstName == firstName
                                         && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRemovedOnce<Person>(x => x.FirstName == firstName
                                                                           && x.LastName == lastName));
        }
    }
}