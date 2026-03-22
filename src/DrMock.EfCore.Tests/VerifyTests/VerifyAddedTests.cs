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

        [Fact]
        public void GivenInterface_WhenAddedMultipleTimesDirect_ShouldVerifyAddedCorrectly()
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
                sut.Add(newPerson);
            }

            // Assert
            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyAddedOnce<Person>(x => x.FirstName == firstName
                                                                         && x.LastName == lastName));
        }

        [Fact]
        public void GivenClass_WhenAddedMultipleTimesDirect_ShouldVerifyAddedCorrectly()
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
                sut.Add(newPerson);
            }

            // Assert
            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyAddedOnce<Person>(x => x.FirstName == firstName
                                                                         && x.LastName == lastName));
        }

        [Fact]
        public void GivenInterface_WhenAddedMultipleTimesOnDbSet_ShouldVerifyAddedCorrectly()
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
                sut.Add(newPerson);
            }

            // Assert
            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyAddedOnce<Person>(x => x.FirstName == firstName
                                                                         && x.LastName == lastName));
        }

        [Fact]
        public void GivenClass_WhenAddedMultipleTimesOnDbSet_ShouldVerifyAddedCorrectly()
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
                sut.Add(newPerson);
            }

            // Assert
            mock.VerifyAdded<Person>(x => x.FirstName == firstName
                                       && x.LastName == lastName, Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyAddedOnce<Person>(x => x.FirstName == firstName
                                                                         && x.LastName == lastName));
        }
    }
}