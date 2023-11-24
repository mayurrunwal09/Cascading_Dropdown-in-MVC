using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Mapper
{
    public class CityMapper : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(e => e.CityId)
             .HasName("pk_City_Id");

            builder.Property(e => e.CityId)
              .ValueGeneratedOnAdd()
              .HasColumnName("City Id")
              .HasColumnType("INT");


            builder.Property(e => e.CityName)
              .HasColumnName("City Name")
              .HasColumnType("Nvarchar(100)")
              .IsRequired();

            builder.HasOne<State>(d => d.State)
                .WithMany(d => d.City)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.Restrict);

           
        }
    }
}
