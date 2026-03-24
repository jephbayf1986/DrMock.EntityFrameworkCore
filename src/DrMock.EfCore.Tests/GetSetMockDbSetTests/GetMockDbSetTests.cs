using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Moq;

namespace DrMock.EfCore.Tests.GetSetMockDbSetTests
{
    public class GetMockDbSetTests
    {
        [Fact]
        public void GivenInterface_WhenGetMockDbSet_BeforeObject_DontThrow()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>();

            var dbSet = mock.GetMockDbSet<Person>();

            var sut = dbSet.Object;

            // Act
            var people = sut.ToList();

            // Assert
            people.ShouldNotBeEmpty();
        }

        [Fact]
        public void GivenInterface_WhenGetMockDbSet_AfterObject_DontThrow()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>();

            var mockInstance = mock.Object;

            var dbSet = mock.GetMockDbSet<Person>();

            var sut = dbSet.Object;

            // Act
            var people = sut.ToList();

            // Assert
            people.ShouldNotBeEmpty();
        }

        [Fact]
        public void GivenInterface_WhenGetMockDbSet_BeforeObject_WithSetup_DontThrow()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var person = new Person();

            var mock = new MockDbContext<ITestDbContext>()
                .UseEntity<Person>();

            var dbSet = mock.GetMockDbSet<Person>();

            dbSet.Setup(x => x.Add(It.IsAny<Person>())).Callback<Person>(x => x.Id = personId);

            var sut = dbSet.Object;

            // Act
            sut.Add(person);

            // Assert
            person.Id.ShouldBe(personId);
        }

        [Fact]
        public void GivenInterface_WhenGetMockDbSet_AfterObject_WithSetup_DontThrow()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var person = new Person();

            var mock = new MockDbContext<ITestDbContext>()
                .UseEntity<Person>();

            var mockInstance = mock.Object;

            var dbSet = mock.GetMockDbSet<Person>();

            dbSet.Setup(x => x.Add(It.IsAny<Person>())).Callback<Person>(x => x.Id = personId);

            var sut = dbSet.Object;

            // Act
            sut.Add(person);

            // Assert
            person.Id.ShouldBe(personId);
        }

        [Fact]
        public void GivenClass_WhenGetMockDbSet_BeforeObject_DontThrow()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Person>();

            var dbSet = mock.GetMockDbSet<Person>();

            var sut = dbSet.Object;

            // Act
            var people = sut.ToList();

            // Assert
            people.ShouldNotBeEmpty();
        }

        [Fact]
        public void GivenClass_WhenGetMockDbSet_AfterObject_DontThrow()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Person>();

            var mockInstance = mock.Object;

            var dbSet = mock.GetMockDbSet<Person>();

            var sut = dbSet.Object;

            // Act
            var people = sut.ToList();

            // Assert
            people.ShouldNotBeEmpty();
        }

        [Fact]
        public void GivenClass_WhenGetMockDbSet_BeforeObject_WithSetup_DontThrow()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var person = new Person();

            var mock = new MockDbContext<TestDbContext>()
                .UseEntity<Person>();

            var dbSet = mock.GetMockDbSet<Person>();

            dbSet.Setup(x => x.Add(It.IsAny<Person>())).Callback<Person>(x => x.Id = personId);

            var sut = dbSet.Object;

            // Act
            sut.Add(person);

            // Assert
            person.Id.ShouldBe(personId);
        }
         
        [Fact]
        public void GivenClass_WhenGetMockDbSet_AfterObject_WithSetup_DontThrow()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var person = new Person();

            var mock = new MockDbContext<TestDbContext>()
                .UseEntity<Person>();

            var mockInstance = mock.Object;

            var dbSet = mock.GetMockDbSet<Person>();

            dbSet.Setup(x => x.Add(It.IsAny<Person>())).Callback<Person>(x => x.Id = personId);

            var sut = dbSet.Object;

            // Act
            sut.Add(person);

            // Assert
            person.Id.ShouldBe(personId);
        }
    }
}