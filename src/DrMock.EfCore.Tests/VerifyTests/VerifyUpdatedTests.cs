using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Moq;

namespace DrMock.EfCore.Tests.VerifyTests
{
    public class VerifyUpdatedTests
    {
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

            Should.Throw<Exception>(() => mock.VerifyNeverUpdated<Person>(x => x.FirstName == firstName
                                                                            && x.LastName == lastName));

            mock.VerifyNeverUpdated<Person>(x => x.FirstName == string.Empty
                                              && x.LastName == string.Empty);
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

            Should.Throw<Exception>(() => mock.VerifyNeverUpdated<Person>(x => x.FirstName == firstName
                                                                            && x.LastName == lastName));

            mock.VerifyNeverUpdated<Person>(x => x.FirstName == string.Empty
                                              && x.LastName == string.Empty);
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

            Should.Throw<Exception>(() => mock.VerifyNeverUpdated<Person>(x => x.FirstName == firstName
                                                                            && x.LastName == lastName));

            mock.VerifyNeverUpdated<Person>(x => x.FirstName == string.Empty
                                              && x.LastName == string.Empty);
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

            Should.Throw<Exception>(() => mock.VerifyNeverUpdated<Person>(x => x.FirstName == firstName
                                                                            && x.LastName == lastName));

            mock.VerifyNeverUpdated<Person>(x => x.FirstName == string.Empty
                                              && x.LastName == string.Empty);
        }
    }
}