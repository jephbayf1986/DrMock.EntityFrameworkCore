using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrMock.EfCore.Tests.BuilderTets
{
    public class ExistsTests
    {
        [Fact]
        public void GivenInterface_WithSimpleExists_CreateDbSetWithPassingData() 
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
        }
    }
}