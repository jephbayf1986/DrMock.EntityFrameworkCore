# DrMock.EfCore
### *A Superset of Moq which allow quick, easy and clear testing with Entity Framework Core!*

The package is designed to quickly write readable methods to test your CRUD logic with test data without having to do integration tests or manually hard code test data for every test. It contains a fluent API to allow you to quickly randomise data but keep it relevant to the test you're writing - for example easily write data where something exists or doesn't.

## A Brief History
It's always been a frustration of mine that we learn TDD with a 'Plus 2 values' or similar piece of logic. In reality we rarely get logic that we can Unit Test in such an isolated way, and often work on CRUD apps that have logic around which database entity you create or not. These become very difficult to test and my first attempt at solving this problem was to write out reams and reams of hardcoded test data that all the tests shared. Inevitably this was difficult to manage and it was difficult to reuse the same data for multiple tests, not to mention this breaks the rules of isolation in Unit Tests.

So I've been working on this pattern and honing it for a few years now, and finally got to the point where I have it in a reusable library, save having to recreate all the different classes! Making it generic was quite a challenge, and Entity Framework has a lot of inconsistencies so i've tried to make the API consistent regardless of this. I hope you find this library as useful as I will!

## How to Use
You can install **DrMock.EFCore** from Nuget Package Manager or Nuget CLI:

```nuget install DrMock.EFCore```

Or you can use the dotnet CLI:

```dotnet add package DrMock.EFCore```

I have also created a seperate package called **DrMock.EFCore.Base** which contains the key interface you need to inherit from in your injected DatabaseContext. The reason for this being seperated is so you can install only that on your production code, and it's much more lightweight as a result. So I suggest install that practice on your code, and install **DrMock.EFCore** in your Test project.

## Getting Started

Make sure to inherit from the interface ```IDbContext``` on your DbContext. You shouldn't need to generate any further code in order to implement this, it has all the key EF Core methods:

```cs
public class ContosoDbContext : DbContext, IDbContext
{
    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<Department> Departments { get; set; }
}
```
Or for Inverting the dependency:
```cs
public interface IContosoDbContext : IDbContext
{
    public DbSet<Person> People { get; set; }

    public DbSet<Payroll> Payrolls { get; set; }

    public DbSet<Department> Departments { get; set; }
}
```

**That's It, you're ready to write some tests!**

## Creating Test Data

To Mock your Interface, create an instance as follows:

```cs
var mockContext = new MockDbContext<IContosoDbContext>();
```

You can then chain a few different calls to ensure data is present:

```cs
var mockContext = new MockDbContext<IContosoDbContext>()
        .UseEntity<Department>()
        .WithRandomDataFor<Person>()
        .Include<Department>(hrDept, itDept);
```
You can also ensure all Entities are accessable with the following static method:
```cs
// Options is not required
var options = new MockDbContextOptions
    {
        MinItemsInDbSet = 20 // Means 20 items will be auto created for each entity
    };

var mockContext = MockDbContext<IContosoDbContext>.UseAllEntities(options);
```

A full breakdown of the Building Data Methods is as follows:

|Method Name|Example Code|Definition|
|-----------|------------|----------|
|UseEntity|```.UseEntity<Person>()```|Ensures the DbSet can then be called within scope without causing null reference exceptions|
|WithRandomDataFor|```.WithRandomDataFor<Person>()```|As with ```UseEntity``` plus it generates random data to be queryable within a unit test scope of work|
|Include|```.Include(person1, person2)```|Allows you to manually input the queryable data|
|Exlude|```.Exlude(person1, person2)```|As with ```WithRandomDataFor``` but allows you to ensure unwanted values get removed|
|Exists|```.Exists<Person>(x => x.Id == 24 && x.fullName == "Joe Bloggs")```|As with ```Include``` but allows you to just declare the properties of interest|
|DoesNotExist|```.DoesNotExist<Person>(x => x.Id == invalidId)```|As with ```Exlude``` but allows you to just declare the properties of interest|

## Verifying Method Calls

Once you have declared the data your Unit Tests require, you can verify CRUD calls have been made...

There is a number of ready made Verify calls for each of the CRUD operation, such as:

```cs
// In your application code:
// Direct on the DbContext:
await _db.AddAync(newPerson);
// Or on the DbSet:
await _db.People.AddAsync(newPerson);

// In your Unit Test:
await mockDb.VerifyAddedAsync<Person>(x => x.FirstName == newPerson.FirstName && x.LastName == newPerson.LastName);

```
Note that there are 2 ways to call each CRUD method - directly on the Context, or via a DbSet. The Verification will handle either of these, but using both may cause issues.

## Handling Exceptions

This library will allow you to write and test code for preventing conflicts such as duplicates. However no amount of server side code will remove the possibility of database conflicts. These usually occur in ```SaveChanges``` or ```SaveChangesAsync``` hence the following methods are provided for build time to allow handling these:

```cs
// For SaveChanges:
var mock = new MockDbContext<TestDbContext>()
    .UseEntity<Person>()
    .WithExceptionThrownOnSaveChanges<DataMisalignedException>();

// For SaveChangesAsync:
var mock = new MockDbContext<TestDbContext>()
    .UseEntity<Person>()
    .WithExceptionThrownOnSaveChangesAsync<ApplicationException>();
```

## New Id Creation Callbacks

When creating entities with `Add` and `AddAsync` Entity Framework handily updates the class with the newly created key. You may then want to use this, possibly return it in the output. Hence the following methods to allow you to create Callbacks to mimic this:

```cs
// For Add:
var mock = new MockDbContext<ITestDbContext>()
                    .UseEntity<Department>()
                    .WithActionOnAdd<Department>(x => x.Id = expectedDepartmentId);
// For AddAsync:
var mock = new MockDbContext<ITestDbContext>()
                    .UseEntity<Department>()
                    .WithActionOnAddAsync<Department>(x => x.Id = expectedDepartmentId);
```

## License, Copyright etc
DrMock.EFCore is created by Jeph Bayfield and is licensed under the MIT license.