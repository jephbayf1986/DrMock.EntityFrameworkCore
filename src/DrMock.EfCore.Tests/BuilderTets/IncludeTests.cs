using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;

namespace DrMock.EfCore.Tests.BuilderTets
{
    public class IncludeTests
    {
        [Fact]
        public void GivenInterface_OnInclude_CreateDbSetWithSomeData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var personAge = RandomByteBetween(18, 65);

            var person = new Person()
            {
                Id = personId,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - personAge),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var mock = new MockDbContext<ITestDbContext>()
                .Include(person);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            people.Count(x => x.Id == personId).ShouldBe(1);
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.FirstName.ShouldBe(person.FirstName),
                    x => x.LastName.ShouldBe(person.LastName),
                    x => x.DateOfBirth.ShouldBe(person.DateOfBirth),
                    x => x.Height.ShouldBe(person.Height),
                    x => x.PayrollNumber.ShouldBe(person.PayrollNumber)
                );
        }

        [Fact]
        public void GivenInterface_IncludeMultiple_CreateDbSetWithSomeData()
        {
            // Arrange
            var person1Id = RandomIntBetween(100, 999);
            var person2Id = RandomIntBetween(100, 999);
            var person1Age = RandomByteBetween(18, 65);
            var person2Age = RandomByteBetween(18, 65);

            var person1 = new Person()
            {
                Id = person1Id,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - person1Age),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var person2 = new Person()
            {
                Id = person2Id,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - person2Age),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var mock = new MockDbContext<ITestDbContext>()
                .Include(person1, person2);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var person1Found = dbContext.People.FirstOrDefault(x => x.Id == person1Id);
            var person2Found = dbContext.People.FirstOrDefault(x => x.Id == person2Id);

            // Assert
            people.ShouldContain(x => x.Id == person1Id);
            people.Count(x => x.Id == person1Id).ShouldBe(1);
            person1Found.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(person1Id),
                    x => x.FirstName.ShouldBe(person1.FirstName),
                    x => x.LastName.ShouldBe(person1.LastName),
                    x => x.DateOfBirth.ShouldBe(person1.DateOfBirth),
                    x => x.Height.ShouldBe(person1.Height),
                    x => x.PayrollNumber.ShouldBe(person1.PayrollNumber)
                );
            person2Found.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(person2Id),
                    x => x.FirstName.ShouldBe(person2.FirstName),
                    x => x.LastName.ShouldBe(person2.LastName),
                    x => x.DateOfBirth.ShouldBe(person2.DateOfBirth),
                    x => x.Height.ShouldBe(person2.Height),
                    x => x.PayrollNumber.ShouldBe(person2.PayrollNumber)
                );
        }

        [Fact]
        public void GivenInterface_IncludeChainedDifferentTypes_CreateDbSetWithSomeData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var departmentId = RandomIntBetween(100, 999);
            var person1Age = RandomByteBetween(18, 65);
            var person2Age = RandomByteBetween(18, 65);

            var person1 = new Person()
            {
                Id = personId,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - person1Age),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var department = new Department()
            {
                Id = departmentId,
                Name = LoremIpsumText(20),
                DateOpen = RandomDateInPast()
            };

            var mock = new MockDbContext<ITestDbContext>()
                .Include(person1)
                .Include(department);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);
            var departmentFound = dbContext.Departments.FirstOrDefault(x => x.Id == departmentId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            people.Count(x => x.Id == personId).ShouldBe(1);
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.FirstName.ShouldBe(person1.FirstName),
                    x => x.LastName.ShouldBe(person1.LastName),
                    x => x.DateOfBirth.ShouldBe(person1.DateOfBirth),
                    x => x.Height.ShouldBe(person1.Height),
                    x => x.PayrollNumber.ShouldBe(person1.PayrollNumber)
                );
            departmentFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(departmentId),
                    x => x.Name.ShouldBe(department.Name),
                    x => x.DateOpen.ShouldBe(department.DateOpen)
                );
        }

        [Fact]
        public void GivenInterface_IncludeAfterUseEntity_CreateDbSetWithSomeData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var personAge = RandomByteBetween(18, 65);

            var person = new Person()
            {
                Id = personId,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - personAge),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var mock = new MockDbContext<ITestDbContext>()
                .UseEntity<Person>()
                .Include(person);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            people.Count(x => x.Id == personId).ShouldBe(1);
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.FirstName.ShouldBe(person.FirstName),
                    x => x.LastName.ShouldBe(person.LastName),
                    x => x.DateOfBirth.ShouldBe(person.DateOfBirth),
                    x => x.Height.ShouldBe(person.Height),
                    x => x.PayrollNumber.ShouldBe(person.PayrollNumber)
                );
        }

        [Fact]
        public void GivenClass_OnInclude_CreateDbSetWithSomeData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var personAge = RandomByteBetween(18, 65);

            var person = new Person()
            {
                Id = personId,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - personAge),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var mock = new MockDbContext<TestDbContext>()
                .Include(person);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            people.Count(x => x.Id == personId).ShouldBe(1);
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.FirstName.ShouldBe(person.FirstName),
                    x => x.LastName.ShouldBe(person.LastName),
                    x => x.DateOfBirth.ShouldBe(person.DateOfBirth),
                    x => x.Height.ShouldBe(person.Height),
                    x => x.PayrollNumber.ShouldBe(person.PayrollNumber)
                );
        }

        [Fact]
        public void GivenClass_IncludeMultiple_CreateDbSetWithSomeData()
        {
            // Arrange
            var person1Id = RandomIntBetween(100, 999);
            var person2Id = RandomIntBetween(100, 999);
            var person1Age = RandomByteBetween(18, 65);
            var person2Age = RandomByteBetween(18, 65);

            var person1 = new Person()
            {
                Id = person1Id,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - person1Age),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var person2 = new Person()
            {
                Id = person2Id,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - person2Age),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var mock = new MockDbContext<TestDbContext>()
                .Include(person1, person2);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var person1Found = dbContext.People.FirstOrDefault(x => x.Id == person1Id);
            var person2Found = dbContext.People.FirstOrDefault(x => x.Id == person2Id);

            // Assert
            people.ShouldContain(x => x.Id == person1Id);
            people.Count(x => x.Id == person1Id).ShouldBe(1);
            person1Found.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(person1Id),
                    x => x.FirstName.ShouldBe(person1.FirstName),
                    x => x.LastName.ShouldBe(person1.LastName),
                    x => x.DateOfBirth.ShouldBe(person1.DateOfBirth),
                    x => x.Height.ShouldBe(person1.Height),
                    x => x.PayrollNumber.ShouldBe(person1.PayrollNumber)
                );
            person2Found.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(person2Id),
                    x => x.FirstName.ShouldBe(person2.FirstName),
                    x => x.LastName.ShouldBe(person2.LastName),
                    x => x.DateOfBirth.ShouldBe(person2.DateOfBirth),
                    x => x.Height.ShouldBe(person2.Height),
                    x => x.PayrollNumber.ShouldBe(person2.PayrollNumber)
                );
        }

        [Fact]
        public void GivenClass_IncludeChainedDifferentTypes_CreateDbSetWithSomeData() 
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var departmentId = RandomIntBetween(100, 999);
            var person1Age = RandomByteBetween(18, 65);
            var person2Age = RandomByteBetween(18, 65);

            var person1 = new Person()
            {
                Id = personId,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - person1Age),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var department = new Department()
            {
                Id = departmentId,
                Name = LoremIpsumText(20),
                DateOpen = RandomDateInPast()
            };

            var mock = new MockDbContext<TestDbContext>()
                .Include(person1)
                .Include(department);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);
            var departmentFound = dbContext.Departments.FirstOrDefault(x => x.Id == departmentId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            people.Count(x => x.Id == personId).ShouldBe(1);
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.FirstName.ShouldBe(person1.FirstName),
                    x => x.LastName.ShouldBe(person1.LastName),
                    x => x.DateOfBirth.ShouldBe(person1.DateOfBirth),
                    x => x.Height.ShouldBe(person1.Height),
                    x => x.PayrollNumber.ShouldBe(person1.PayrollNumber)
                );
            departmentFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(departmentId),
                    x => x.Name.ShouldBe(department.Name),
                    x => x.DateOpen.ShouldBe(department.DateOpen)
                );
        }

        [Fact]
        public void GivenClass_IncludeAfterUseEntity_CreateDbSetWithSomeData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var personAge = RandomByteBetween(18, 65);

            var person = new Person()
            {
                Id = personId,
                FirstName = RandomFirstName(),
                LastName = RandomLastName(),
                DateOfBirth = RandomDateInYear(DateTime.Now.Year - personAge),
                Height = ((decimal)RandomIntBetween(150, 200)) / 100,
                PayrollNumber = RandomIntBetween(10000, 99999)
            };

            var mock = new MockDbContext<TestDbContext>()
                .UseEntity<Person>()
                .Include(person);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            people.Count(x => x.Id == personId).ShouldBe(1);
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.FirstName.ShouldBe(person.FirstName),
                    x => x.LastName.ShouldBe(person.LastName),
                    x => x.DateOfBirth.ShouldBe(person.DateOfBirth),
                    x => x.Height.ShouldBe(person.Height),
                    x => x.PayrollNumber.ShouldBe(person.PayrollNumber)
                );
        }
    }
}