using DrMock.EfCore.Tests.Contexts;

namespace DrMock.EfCore.Tests.ThrowExceptionTests
{
    public class WithExceptionThrownOnSaveChangesTests
    {
        [Fact]
        public void GivenInterface_WhenSaveChanges_ShouldThrow()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .WithExceptionThrownOnSaveChanges<DataMisalignedException>();

            var sut = mock.Object;

            // Assert
            Should.Throw<DataMisalignedException>(() => sut.SaveChanges());
        }

        [Fact]
        public void GivenClass_WhenSaveChanges_ShouldThrow()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .WithExceptionThrownOnSaveChanges<DataMisalignedException>();

            var sut = mock.Object;

            // Assert
            Should.Throw<DataMisalignedException>(() => sut.SaveChanges());
        }
    }
}