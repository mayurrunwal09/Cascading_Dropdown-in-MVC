using WebApplication1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Mapper
{
    public class EmployeeMapper : IEntityTypeConfiguration<Employee>
    {

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Emp_Id)
                .HasName("pk_Employee_Id");

            builder.Property(e => e.Emp_Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Employee Id")
                .HasColumnType("INT");

            builder.Property(e => e.Emp_Name)
                .HasColumnName("Employee Name")
                .HasColumnType("Nvarchar(100)")
                .IsRequired();

            builder.Property(e => e.Emp_Adress)
                .HasColumnName("Employee Address")
                .HasColumnType("NVarchar(500)")
                .IsRequired();

            builder.Property(e => e.mobno)
                .HasColumnName("Employee PhNo")
                .HasColumnType("NVarchar(500)")
                .IsRequired();

            builder.Property(e => e.Sallary)
                .HasColumnName("Employee Sallary")
                .HasColumnType("NVarchar(500)")
                .IsRequired();

            builder.Property(e => e.age)
               .HasColumnName("Employee Age")
               .HasColumnType("NVarchar(500)")
               .IsRequired();


            builder.HasOne<Department>(d => d.department)
               .WithMany(e => e.Employees)
               .HasForeignKey(f => f.Dept_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<State>(d => d.State)
                .WithMany(e => e.Employees)
                .HasForeignKey(f => f.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<City>(d => d.City)
                 .WithMany(e => e.Employees)
                 .HasForeignKey(f => f.CityId)
                 .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
