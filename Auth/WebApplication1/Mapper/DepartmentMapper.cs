using WebApplication1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Mapper
{
    public class DepartmentMapper : IEntityTypeConfiguration<Department>
    {

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(e => e.Dept_Id)
               .HasName("pk_Dep_Id");

            builder.Property(e => e.Dept_Id)
              .ValueGeneratedOnAdd()
              .HasColumnName("Dept Id")
              .HasColumnType("INT");


            builder.Property(e => e.Department_Name)
               .HasColumnName("Department Name")
               .HasColumnType("Nvarchar(100)")
               .IsRequired();
        }

    }
}
