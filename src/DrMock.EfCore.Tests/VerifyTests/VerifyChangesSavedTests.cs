using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Moq;

namespace DrMock.EfCore.Tests.VerifyTests
{
    public class VerifyChangesSavedTests
    {
        [Fact]
        public void GivenInterface_WhenSaveChangesNoParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            // Act
            sut.SaveChanges();

            // Assert
            mock.VerifyChangesSaved();
            mock.VerifyChangesSavedOnce();

            Should.Throw<Exception>(mock.VerifyChangesNeverSaved);
        }

        [Fact]
        public void GivenInterface_WhenSaveChangesWithParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            // Act
            sut.SaveChanges(true);

            // Assert
            mock.VerifyChangesSaved();
            mock.VerifyChangesSavedOnce();

            Should.Throw<Exception>(mock.VerifyChangesNeverSaved);
        }

        [Fact]
        public void GivenInterface_WhenSaveChangesBothWays_ShouldVerifySaveCorrectlyButNotOnce()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            // Act
            sut.SaveChanges();
            sut.SaveChanges(true);

            // Assert
            mock.VerifyChangesSaved();

            Should.Throw<Exception>(mock.VerifyChangesSavedOnce);
        }

        [Fact]
        public void GivenInterface_WhenSaveChangesMultipleTimesNoParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.SaveChanges();
            }

            // Assert
            mock.VerifyChangesSaved();
            mock.VerifyChangesSaved(Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(mock.VerifyChangesSavedOnce);
        }

        [Fact]
        public void GivenInterface_WhenSaveChangesMultipleTimesWithParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.SaveChanges(false);
            }

            // Assert
            mock.VerifyChangesSaved();
            mock.VerifyChangesSaved(Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(mock.VerifyChangesSavedOnce);
        }
    }
}