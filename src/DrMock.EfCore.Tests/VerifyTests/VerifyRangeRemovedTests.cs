using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;
using Moq;

namespace DrMock.EfCore.Tests.VerifyTests
{
    public class VerifyRangeRemovedTests
    {
        [Fact]
        public void GivenInterface_WhenRangeRemovedDirect_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.RemoveRange(newPeople);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedDirect_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.RemoveRange(newPeople);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public void GivenInterface_WhenRangeRemovedOnDbSet_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.People.RemoveRange(newPeople);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedOnDbSet_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            // Act
            sut.People.RemoveRange(newPeople);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public void GivenInterface_WhenRangeRemovedDirect_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.RemoveRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedDirect_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.RemoveRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public void GivenInterface_WhenRangeRemovedOnDbSet_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.People.RemoveRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedOnDbSet_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            // Act
            sut.People.RemoveRange(newPerson1, newPerson2);

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Once());

            mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                            && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2));

            Should.Throw<Exception>(() => mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                 && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));

            mock.VerifyRangeNeverRemoved<Person>(x => x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty)
                                                   && x.Any(p => p.FirstName == string.Empty && p.LastName == string.Empty));
        }

        [Fact]
        public void GivenInterface_WhenRangeRemovedMultipleTimesDirect_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.RemoveRange(newPeople);
            }

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                              && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedMultipleTimesDirect_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.RemoveRange(newPeople);
            }

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                              && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public void GivenInterface_WhenRangeRemovedMultipleTimesOnDbSet_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.People.RemoveRange(newPeople);
            }

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                              && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedMultipleTimesOnDbSet_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            List<Person> newPeople = [newPerson1, newPerson2];

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.People.RemoveRange(newPeople);
            }

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                              && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public void GivenInterface_WhenRangeRemovedMultipleTimesDirect_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.RemoveRange(newPerson1, newPerson2);
            }

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                              && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedMultipleTimesDirect_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.RemoveRange(newPerson1, newPerson2);
            }

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                              && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public void GivenInterface_WhenRangeRemovedMultipleTimesOnDbSet_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.People.RemoveRange(newPerson1, newPerson2);
            }

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                              && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }

        [Fact]
        public void GivenClass_WhenRangeRemovedMultipleTimesOnDbSet_WithParams_ShouldVerifyRemovedCorrectly()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                                .UseEntity<Person>();

            var sut = mock.Object;

            var firstName1 = RandomFirstName();
            var lastName1 = RandomLastName();
            var firstName2 = RandomFirstName();
            var lastName2 = RandomLastName();

            Person newPerson1 = new Person() { FirstName = firstName1, LastName = lastName1 };
            Person newPerson2 = new Person() { FirstName = firstName2, LastName = lastName2 };

            var numberOfEvents = RandomByteBetween(2, 5);

            // Act
            for (int i = 0; i < numberOfEvents; i++)
            {
                sut.People.RemoveRange(newPerson1, newPerson2);
            }

            // Assert
            mock.VerifyRangeRemoved<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                              && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2), Times.Exactly(numberOfEvents));

            Should.Throw<Exception>(() => mock.VerifyRangeRemovedOnce<Person>(x => x.Any(p => p.FirstName == firstName1 && p.LastName == lastName1)
                                                                                && x.Any(p => p.FirstName == firstName2 && p.LastName == lastName2)));
        }
    }
}