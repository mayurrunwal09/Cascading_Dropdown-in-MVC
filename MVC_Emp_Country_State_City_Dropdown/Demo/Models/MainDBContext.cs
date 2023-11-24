using Demo.Mapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models
{
    public class MainDBContext : IdentityDbContext
    {
        public MainDBContext(DbContextOptions options): base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> Statements { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeeMapper());
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new DepartmentMapper());
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CountryMapper());
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new StateMapper());
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CityMapper());
            base.OnModelCreating(builder);
        }
    }
}
