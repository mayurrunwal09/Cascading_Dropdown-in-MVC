using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Mapper
{
    public class EmployeeMapper : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmpId)
                 .HasName("pk_EmpId");

            builder.Property(e => e.EmpId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Employee Id")
                .HasColumnType("INT");

            builder.Property(e => e.EmpName)
                .HasColumnName("Employee Name")
                .HasColumnType("Nvarchar(100)")
                .IsRequired();

            builder.Property(e=>e.Mobno)
                .HasColumnName("Mobile number")
                .HasColumnType("Nvarchar(50)")
                .IsRequired();

            builder.Property(e => e.Gender)
               .HasColumnName("Gender")
               .HasColumnType("Nvarchar(50)")
               .IsRequired();


            builder.Property(e => e.Salary )
               .HasColumnName("Salary")
               .HasColumnType("Nvarchar(50)")
               .IsRequired();

            builder.HasOne<Department>(d => d.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(d => d.DepId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Country >(d => d.Country)
                .WithMany(d => d.employees)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<State >(d => d.State )
                .WithMany(d => d.Employees)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder .HasOne<City>(c=>c.City)
                .WithMany(d=>d.Employees)
                .HasForeignKey(d=>d.CityId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
