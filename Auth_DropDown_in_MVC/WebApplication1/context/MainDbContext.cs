using WebApplication1.Mapper;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class MainDbContext : IdentityDbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> option) : base(option)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<State> Statements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeeMapper());
            base.OnModelCreating(builder);

           builder.ApplyConfiguration(new DepartmentMapper());
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new StateMapper());
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CityMapper());
            base.OnModelCreating(builder);
        }

    }
}
