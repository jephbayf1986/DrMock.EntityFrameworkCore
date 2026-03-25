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
        public void GivenInterface_WithExistsWithNull_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.Level == null);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.Level.ShouldBeNull();
        }

        [Fact]
        public void GivenInterface_WithExistsWithGuidGenerated_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.UniqueIdentifier == Guid.NewGuid() && x.FirstName == Guid.NewGuid().ToString());

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.UniqueIdentifier.ShouldNotBeNull();
            personFound.FirstName.ShouldNotBeEmpty();
        }

        [Fact]
        public void GivenInterface_WithExistsWithEmptyList_CreateDbSetWithPassingData()
        {
            // Arrange
            var departmentId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Department>()
                .Exists<Department>(x => x.Id == departmentId && x.Employees == new List<Person>());
            
            var dbContext = mock.Object;

            // Act
            var departments = dbContext.Departments.ToList();
            var departmentFound = dbContext.Departments.FirstOrDefault(x => x.Id == departmentId);

            // Assert
            departments.ShouldContain(x => x.Id == departmentId);
            departmentFound.ShouldNotBeNull();
        }

        [Fact]
        public void GivenInterface_WithExistsWithPopulatedList_CreateDbSetWithPassingData()
        {
            // Arrange
            var departmentId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);
            var person = new Person { Id = RandomIntBetween(10, 99) };

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Department>()
                .Exists<Department>(x => x.Id == departmentId && x.Employees == new List<Person>() { person });

            var dbContext = mock.Object;

            // Act
            var departments = dbContext.Departments.ToList();
            var departmentFound = dbContext.Departments.FirstOrDefault(x => x.Id == departmentId);

            // Assert
            departments.ShouldContain(x => x.Id == departmentId);
            departmentFound.ShouldNotBeNull();
            departmentFound.Employees.ShouldNotBeEmpty();
        }

        [Fact]
        public void GivenInterface_WithExistsWithWithCalulation_CreateDbSetWithPassingData()
        {
            // Arrange
            var departmentId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);
            var person = new Person { Id = RandomIntBetween(10, 99) };

            var mock = new MockDbContext<ITestDbContext>()
                .WithRandomDataFor<Department>()
                .Exists<Department>(x => x.Id == departmentId + 1);

            var dbContext = mock.Object;

            // Act
            var departments = dbContext.Departments.ToList();
            var departmentFound = dbContext.Departments.FirstOrDefault(x => x.Id == departmentId + 1);

            // Assert
            departmentFound.ShouldNotBeNull();
        }

        [Fact]
        public void GivenInterface_WithExistsWithWithMethodCall_CreateDbSetWithPassingData()
        {
            // Arrange
            var mock = new MockDbContext<ITestDbContext>()
                .Exists<Department>(x => x.Id == RandomIntBetween(500, 550));

            var dbContext = mock.Object;

            // Act
            var departmentFound = dbContext.Departments.FirstOrDefault();

            // Assert
            departmentFound.ShouldNotBeNull();
            departmentFound.Id.ShouldBeInRange(500, 550);
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

        [Fact]
        public void GivenClass_WithExistsWithNull_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.Level == null);

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.Level.ShouldBeNull();
        }

        [Fact]
        public void GivenClass_WithExistsWithGuidGenerated_CreateDbSetWithPassingData()
        {
            // Arrange
            var personId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Person>()
                .Exists<Person>(x => x.Id == personId && x.UniqueIdentifier == Guid.NewGuid() && x.FirstName == Guid.NewGuid().ToString());

            var dbContext = mock.Object;

            // Act
            var people = dbContext.People.ToList();
            var personFound = dbContext.People.FirstOrDefault(x => x.Id == personId);

            // Assert
            people.ShouldContain(x => x.Id == personId);
            personFound.ShouldNotBeNull();
            personFound.UniqueIdentifier.ShouldNotBeNull();
            personFound.FirstName.ShouldNotBeEmpty();
        }

        [Fact]
        public void GivenClass_WithExistsWithEmptyList_CreateDbSetWithPassingData()
        {
            // Arrange
            var departmentId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Department>()
                .Exists<Department>(x => x.Id == departmentId && x.Employees == new List<Person>());

            var dbContext = mock.Object;

            // Act
            var departments = dbContext.Departments.ToList();
            var departmentFound = dbContext.Departments.FirstOrDefault(x => x.Id == departmentId);

            // Assert
            departments.ShouldContain(x => x.Id == departmentId);
            departmentFound.ShouldNotBeNull();
        }

        [Fact]
        public void GivenClass_WithExistsWithPopulatedList_CreateDbSetWithPassingData()
        {
            // Arrange
            var departmentId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);
            var person = new Person { Id = RandomIntBetween(10, 99) };

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Department>()
                .Exists<Department>(x => x.Id == departmentId && x.Employees == new List<Person>() { person });

            var dbContext = mock.Object;

            // Act
            var departments = dbContext.Departments.ToList();
            var departmentFound = dbContext.Departments.FirstOrDefault(x => x.Id == departmentId);

            // Assert
            departments.ShouldContain(x => x.Id == departmentId);
            departmentFound.ShouldNotBeNull();
            departmentFound.Employees.ShouldNotBeEmpty();
        }

        [Fact]
        public void GivenClass_WithExistsWithWithCalulation_CreateDbSetWithPassingData()
        {
            // Arrange
            var departmentId = RandomIntBetween(100, 999);
            var payrollDepartmentId = RandomIntBetween(1000, 2000);
            var person = new Person { Id = RandomIntBetween(10, 99) };

            var mock = new MockDbContext<TestDbContext>()
                .WithRandomDataFor<Department>()
                .Exists<Department>(x => x.Id == departmentId + 1);

            var dbContext = mock.Object;

            // Act
            var departments = dbContext.Departments.ToList();
            var departmentFound = dbContext.Departments.FirstOrDefault(x => x.Id == departmentId + 1);

            // Assert
            departmentFound.ShouldNotBeNull();
        }

        [Fact]
        public void GivenClass_WithExistsWithWithMethodCall_CreateDbSetWithPassingData()
        {
            // Arrange
            var mock = new MockDbContext<TestDbContext>()
                .Exists<Department>(x => x.Id == RandomIntBetween(500, 550));

            var dbContext = mock.Object;

            // Act
            var departmentFound = dbContext.Departments.FirstOrDefault();

            // Assert
            departmentFound.ShouldNotBeNull();
            departmentFound.Id.ShouldBeInRange(500, 550);
        }
    }
}