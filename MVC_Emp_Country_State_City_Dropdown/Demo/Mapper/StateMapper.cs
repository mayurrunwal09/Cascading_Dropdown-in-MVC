using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Mapper
{
    public class StateMapper : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(e => e.StateId)
             .HasName("pk_State_Id");

            builder.Property(e => e.StateId)
              .ValueGeneratedOnAdd()
              .HasColumnName("State Id")
              .HasColumnType("INT");


            builder.Property(e => e.StateName)
              .HasColumnName("State Name")
              .HasColumnType("Nvarchar(100)")
              .IsRequired();

            builder.HasOne<Country>(c => c.Country)
                .WithMany(d => d.states)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
