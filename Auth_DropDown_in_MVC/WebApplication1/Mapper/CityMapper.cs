using WebApplication1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Mapper
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
                .WithMany(e => e.Cities)
                .HasForeignKey(f => f.StateId)
                .OnDelete(DeleteBehavior.Restrict);


        }

    }
}
