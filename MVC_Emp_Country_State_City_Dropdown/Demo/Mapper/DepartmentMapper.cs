using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Mapper
{
    public class DepartmentMapper : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(e => e.DepId )
                .HasName("pk_DepId");

            builder.Property(e => e.DepId)
            .ValueGeneratedOnAdd()
            .HasColumnName("Dept Id")
            .HasColumnType("INT");


            builder.Property(e => e.DepName )
               .HasColumnName("Department Name")
               .HasColumnType("Nvarchar(100)")
               .IsRequired();
        }
    }
}
