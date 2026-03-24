using DrMock.EfCore.Tests.Contexts;

namespace DrMock.EfCore.Tests.ThrowExceptionTests
{
    public class WithExceptionThrownOnSaveChangesAsyncTests
    {
        [Fact]
        public void GivenInterface_WhenSaveChangesAsync_ShouldThrow()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithExceptionThrownOnSaveChangesAsync<DataMisalignedException>();

            var sut = mock.Object;

            // Assert
            Should.Throw<DataMisalignedException>(() => sut.SaveChangesAsync());
        }

        [Fact]
        public void GivenClass_WhenSaveChangesAsync_ShouldThrow()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithExceptionThrownOnSaveChangesAsync<DataMisalignedException>();

            var sut = mock.Object;

            // Assert
            Should.Throw<DataMisalignedException>(() => sut.SaveChangesAsync());
        }
    }
}