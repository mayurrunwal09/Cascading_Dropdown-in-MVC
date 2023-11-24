using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Mapper
{
    public class StateMapper : IEntityTypeConfiguration<State>
    {

        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(e => e.StateId)
             .HasName("pk_StateId");

            builder.Property(e => e.StateId)
               .ValueGeneratedOnAdd()
               .HasColumnName("State Id")
               .HasColumnType("INT");

            builder.Property(e => e.StateName)
             .HasColumnName("State Name")
             .HasColumnType("Nvarchar(100)")
             .IsRequired();


        }
        
        }
}
