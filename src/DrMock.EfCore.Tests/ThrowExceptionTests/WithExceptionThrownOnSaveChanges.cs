using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrMock.EfCore.Tests.ThrowExceptionTests
{
    public class WithExceptionThrownOnSaveChanges
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