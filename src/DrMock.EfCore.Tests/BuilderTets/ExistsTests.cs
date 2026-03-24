using DrMock.EfCore.Tests.Contexts;
using DrMock.EfCore.Tests.Models;

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

        [Fact]
        public void GivenInterface_WithExistsOtherWayAround_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => personId == x.Id);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
        }

        [Fact]
        public void GivenInterface_WithExistsTwoPropertiesAndConstant_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.FirstName == "Fred");

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.FirstName.ShouldBe("Fred")
                );
        }

        [Fact]
        public void GivenInterface_WithExistsOr_CreateDbSetOneSide()
        {
            // Arrange
            var personId1 = RandomIntBetween(900, 999);
            var personId2 = RandomIntBetween(1000, 1099);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId1 || x.Id == personId2);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var person1Found = dbContext.People.FirstOrDefault(x => x.Id == personId1);
            var person2Found = dbContext.People.FirstOrDefault(x => x.Id == personId2);

            List<Person> peopleFound = [person1Found, person2Found];

            // Assert
            people.ShouldContain(x => x.Id == personId1 || x.Id == personId2);
            peopleFound.ShouldContain(x => x != null);
        }

        [Fact]
        public void GivenInterface_WithExistsBoolean_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && !x.IsManager);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.IsManager.ShouldBeFalse()
                );
        }

        [Fact]
        public void GivenInterface_WithExistsNegative_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var positivePayrollNumber = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.PayrollNumber == -positivePayrollNumber);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => (x.PayrollNumber + positivePayrollNumber).ShouldBe(0)
                );
        }

        [Fact]
        public void GivenInterface_WithExistsSingleNestedAssignment_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var payrollId = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.Payroll.Id == payrollId && x.PayrollNumber == payrollId);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.Payroll.ShouldNotBeNull(),
                    x => x.Payroll.Id.ShouldBe(payrollId),
                    x => x.PayrollNumber.ShouldBe(payrollId)
                );
        }

        [Fact]
        public void GivenInterface_WithExistsMultiNestedAssignment_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.Payroll.PayrollDepartment.Id == payrollDepartmentId && x.Payroll.PayrollDepartment.Name == "Test");

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.Payroll.ShouldNotBeNull(),
                    x => x.Payroll.PayrollDepartment.ShouldNotBeNull(),
                    x => x.Payroll.PayrollDepartment.Id.ShouldBe(payrollDepartmentId),
                    x => x.Payroll.PayrollDepartment.Name.ShouldNotBeEmpty()
                );
        }

        [Fact]
        public void GivenClass_WithSimpleExists_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);

            var mock = new MockDbContext<TestDbContext>()
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

        [Fact]
        public void GivenClass_WithExistsOtherWayAround_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => personId == x.Id);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
        }

        [Fact]
        public void GivenClass_WithExistsTwoPropertiesAndConstant_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.FirstName == "Fred");

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.FirstName.ShouldBe("Fred")
                );
        }

        [Fact]
        public void GivenClass_WithExistsOr_CreateDbSetOneSide()
        {
            // Arrange
            var personId1 = RandomIntBetween(900, 999);
            var personId2 = RandomIntBetween(1000, 1099);

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId1 || x.Id == personId2);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var person1Found = dbContext.People.FirstOrDefault(x => x.Id == personId1);
            var person2Found = dbContext.People.FirstOrDefault(x => x.Id == personId2);

            List<Person> peopleFound = [person1Found, person2Found];

            // Assert
            people.ShouldContain(x => x.Id == personId1 || x.Id == personId2);
            peopleFound.ShouldContain(x => x != null);
        }

        [Fact]
        public void GivenClass_WithExistsBoolean_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && !x.IsManager);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.IsManager.ShouldBeFalse()
                );
        }

        [Fact]
        public void GivenClass_WithExistsNegative_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var positivePayrollNumber = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.PayrollNumber == -positivePayrollNumber);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => (x.PayrollNumber + positivePayrollNumber).ShouldBe(0)
                );
        }

        [Fact]
        public void GivenClass_WithExistsSingleNestedAssignment_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var payrollId = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.Payroll.Id == payrollId && x.PayrollNumber == payrollId);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.Payroll.ShouldNotBeNull(),
                    x => x.Payroll.Id.ShouldBe(payrollId),
                    x => x.PayrollNumber.ShouldBe(payrollId)
                );
        }

        [Fact]
        public void GivenClass_WithExistsMultiNestedAssignment_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.Payroll.PayrollDepartment.Id == payrollDepartmentId && x.Payroll.PayrollDepartment.Name == "Test");

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.ShouldSatisfyAllConditions(
                    x => x.Id.ShouldBe(personId),
                    x => x.Payroll.ShouldNotBeNull(),
                    x => x.Payroll.PayrollDepartment.ShouldNotBeNull(),
                    x => x.Payroll.PayrollDepartment.Id.ShouldBe(payrollDepartmentId),
                    x => x.Payroll.PayrollDepartment.Name.ShouldNotBeEmpty()
                );
        }
    }
}