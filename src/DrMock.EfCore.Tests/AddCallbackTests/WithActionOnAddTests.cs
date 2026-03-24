using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;

namespace DrMock.EfCore.Tests.AddCallbackTests
{
    public class WithActionOnAddTests
    {
        [Fact]
        public void GivenInterface_WhenAddedDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var expectedDepartmentId = RandomIntBetween(1000, 9999);

            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Department>()
                                .WithActionOnAdd<Department>(x => x.Id = expectedDepartmentId);

            var sut = mock.Object;

            var departmentName = LoremIpsumText(10);

            Department newDepartment = new Department() { Name = departmentName };

            // Act
            sut.Add(newDepartment);

            // Assert
            newDepartment.Id.ShouldBe(expectedDepartmentId);
        }

        [Fact]
        public void GivenInterface_WhenAddedOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var expectedDepartmentId = RandomIntBetween(1000, 9999);

            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Department>()
                                .WithActionOnAdd<Department>(x => x.Id = expectedDepartmentId);

            var sut = mock.Object;

            var departmentName = LoremIpsumText(10);

            Department newDepartment = new Department() { Name = departmentName };

            // Act
            sut.Departments.Add(newDepartment);

            // Assert
            newDepartment.Id.ShouldBe(expectedDepartmentId);
        }

        [Fact]
        public void GivenClass_WhenAddedDirect_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var expectedDepartmentId = RandomIntBetween(1000, 9999);

            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Department>()
                                .WithActionOnAdd<Department>(x => x.Id = expectedDepartmentId);

            var sut = mock.Object;

            var departmentName = LoremIpsumText(10);

            Department newDepartment = new Department() { Name = departmentName };

            // Act
            sut.Add(newDepartment);

            // Assert
            newDepartment.Id.ShouldBe(expectedDepartmentId);
        }

        [Fact]
        public void GivenClass_WhenAddedOnDbSet_ShouldVerifyAddedCorrectly()
        {
            // Arrange
            var expectedDepartmentId = RandomIntBetween(1000, 9999);

            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Department>()
                                .WithActionOnAdd<Department>(x => x.Id = expectedDepartmentId);

            var sut = mock.Object;

            var departmentName = LoremIpsumText(10);

            Department newDepartment = new Department() { Name = departmentName };

            // Act
            sut.Departments.Add(newDepartment);

            // Assert
            newDepartment.Id.ShouldBe(expectedDepartmentId);
        }
    }
}