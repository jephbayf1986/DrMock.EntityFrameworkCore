using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;

namespace DrMock.EfCore.Tests.AddCallbackTests
{
    public class WithActionOnAddAsyncTests
    {
        [Fact]
        public async Task GivenInterface_WhenAddedAsyncDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var expectedDepartmentId = RandomIntBetween(1000, 9999);

            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Department>()
                                .WithActionOnAddAsync<Department>(x => x.Id = expectedDepartmentId);

            var sut = mock.Object;

            var departmentName = LoremIpsumText(10);

            Department newDepartment = new Department() { Name = departmentName };

            // Act
            await sut.AddAsync(newDepartment);

            // Assert
            newDepartment.Id.ShouldBe(expectedDepartmentId);
        }

        [Fact]
        public async Task GivenInterface_WhenAddedAsyncOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var expectedDepartmentId = RandomIntBetween(1000, 9999);

            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Department>()
                                .WithActionOnAddAsync<Department>(x => x.Id = expectedDepartmentId);

            var sut = mock.Object;

            var departmentName = LoremIpsumText(10);

            Department newDepartment = new Department() { Name = departmentName };

            // Act
            await sut.Departments.AddAsync(newDepartment);

            // Assert
            newDepartment.Id.ShouldBe(expectedDepartmentId);
        }

        [Fact]
        public async Task GivenClass_WhenAddedAsyncDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var expectedDepartmentId = RandomIntBetween(1000, 9999);

            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Department>()
                                .WithActionOnAddAsync<Department>(x => x.Id = expectedDepartmentId);

            var sut = mock.Object;

            var departmentName = LoremIpsumText(10);

            Department newDepartment = new Department() { Name = departmentName };

            // Act
            await sut.AddAsync(newDepartment);

            // Assert
            newDepartment.Id.ShouldBe(expectedDepartmentId);
        }

        [Fact]
        public async Task GivenClass_WhenAddedAsyncOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var expectedDepartmentId = RandomIntBetween(1000, 9999);

            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Department>()
                                .WithActionOnAddAsync<Department>(x => x.Id = expectedDepartmentId);

            var sut = mock.Object;

            var departmentName = LoremIpsumText(10);

            Department newDepartment = new Department() { Name = departmentName };

            // Act
            await sut.Departments.AddAsync(newDepartment);

            // Assert
            newDepartment.Id.ShouldBe(expectedDepartmentId);
        }
    }
}