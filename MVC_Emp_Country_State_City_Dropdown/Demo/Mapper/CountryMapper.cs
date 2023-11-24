using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Mapper
{
    public class CountryMapper : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(e => e.CountryId)
            .HasName("pk_CountryId");

            builder.Property(e => e.CountryId)
               .ValueGeneratedOnAdd()
               .HasColumnName("Country Id")
               .HasColumnType("INT");

            builder.Property(e => e.CountryName)
             .HasColumnName("Country Name")
             .HasColumnType("Nvarchar(100)")
             .IsRequired();
        }
    }
}
