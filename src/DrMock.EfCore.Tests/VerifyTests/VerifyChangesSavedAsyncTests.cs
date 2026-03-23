using DrMock.EfCore.Tests.Contexts;
using Moq;

namespace DrMock.EfCore.Tests.VerifyTests
{
    public class VerifyChangesSavedAsyncTests
    {
        [Fact]
        public void GivenInterface_WhenSaveChangesAsyncNoParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>();

            var sut = mock.Object;

            // Act
            sut.SaveChangesAsync();

            // Assert
            mock.VerifyChangesSavedAsync();
            mock.VerifyChangesSavedOnceAsync();

            Should.Throw<Exception>(mock.VerifyChangesNeverSavedAsync);
        }

        [Fact]
        public void GivenInterface_WhenSaveChangesAsyncWithParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>();

            var sut = mock.Object;

            // Act
            sut.SaveChangesAsync(true);

            // Assert
            mock.VerifyChangesSavedAsync();
            mock.VerifyChangesSavedOnceAsync();

            Should.Throw<Exception>(mock.VerifyChangesNeverSavedAsync);
        }

        [Fact]
        public void GivenInterface_WhenSaveChangesAsyncBothWays_ShouldVerifySaveCorrectlyButNotOnce()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>();

            var sut = mock.Object;

            // Act
            sut.SaveChangesAsync();
            sut.SaveChangesAsync(true);

            // Assert
            mock.VerifyChangesSavedAsync();

            Should.Throw<Exception>(mock.VerifyChangesSavedOnceAsync);
        }

        [Fact]
        public void GivenInterface_WhenSaveChangesAsyncMultipleTimesNoParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>();

            var sut = mock.Object;

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.SaveChangesAsync();
            }

            // Assert
            mock.VerifyChangesSavedAsync();
            mock.VerifyChangesSavedAsync(Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(mock.VerifyChangesSavedOnceAsync);
        }

        [Fact]
        public void GivenInterface_WhenSaveChangesAsyncMultipleTimesWithParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>();

            var sut = mock.Object;

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.SaveChangesAsync(false);
            }

            // Assert
            mock.VerifyChangesSavedAsync();
            mock.VerifyChangesSavedAsync(Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(mock.VerifyChangesSavedOnceAsync);
        }

        [Fact]
        public void GivenClass_WhenSaveChangesAsyncNoParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>();

            var sut = mock.Object;

            // Act
            sut.SaveChangesAsync();

            // Assert
            mock.VerifyChangesSavedAsync();
            mock.VerifyChangesSavedOnceAsync();

            Should.Throw<Exception>(mock.VerifyChangesNeverSavedAsync);
        }

        [Fact]
        public void GivenClass_WhenSaveChangesAsyncWithParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>();

            var sut = mock.Object;

            // Act
            sut.SaveChangesAsync(true);

            // Assert
            mock.VerifyChangesSavedAsync();
            mock.VerifyChangesSavedOnceAsync();

            Should.Throw<Exception>(mock.VerifyChangesNeverSavedAsync);
        }

        [Fact]
        public void GivenClass_WhenSaveChangesAsyncBothWays_ShouldVerifySaveCorrectlyButNotOnce()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>();

            var sut = mock.Object;

            // Act
            sut.SaveChangesAsync();
            sut.SaveChangesAsync(true);

            // Assert
            mock.VerifyChangesSavedAsync();

            Should.Throw<Exception>(mock.VerifyChangesSavedOnceAsync);
        }

        [Fact]
        public void GivenClass_WhenSaveChangesAsyncMultipleTimesNoParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>();

            var sut = mock.Object;

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.SaveChangesAsync();
            }

            // Assert
            mock.VerifyChangesSavedAsync();
            mock.VerifyChangesSavedAsync(Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(mock.VerifyChangesSavedOnceAsync);
        }

        [Fact]
        public void GivenClass_WhenSaveChangesAsyncMultipleTimesWithParameters_ShouldVerifySaveCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>();

            var sut = mock.Object;

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.SaveChangesAsync(false);
            }

            // Assert
            mock.VerifyChangesSavedAsync();
            mock.VerifyChangesSavedAsync(Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(mock.VerifyChangesSavedOnceAsync);
        }
    }
}