using DrMock.EfCore.Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace DrMock.EfCore.Tests.Contexts
{
    public class TestDbContext : DbContext, ITestDbContext
    {
        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Payroll> Payrolls { get; set; }

        public virtual DbSet<Department> Departments { get; set; }
    }
}