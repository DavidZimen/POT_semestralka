using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class ShowEntityConfiguration : IEntityTypeConfiguration<ShowEntity>
{
    public void Configure(EntityTypeBuilder<ShowEntity> builder)
    {
        builder.ToTable(TableNames.Show);
        
        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.Seasons)
            .WithOne(e => e.Show)
            .HasForeignKey(e => e.ShowId)
            .IsRequired();
        
        builder.HasMany(e => e.Ratings)
            .WithOne(e => e.Show)
            .HasForeignKey(e => e.ShowId)
            .IsRequired(false);
    }
}