using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using Persistence.Entity;

namespace Persistence.Configurations;

public class HotelEntityConfiguration : IEntityTypeConfiguration<HotelEntity>
{
    public void Configure(EntityTypeBuilder<HotelEntity> builder)
    {
        builder.ToTable(TableNames.Hotel);

        builder.Property(h => h.Id)
            .HasColumnName("hotel_id");
    }
}