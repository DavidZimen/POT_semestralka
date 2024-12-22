using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class SeasonEntityConfiguration : IEntityTypeConfiguration<SeasonEntity>
{
    public void Configure(EntityTypeBuilder<SeasonEntity> builder)
    {
        builder.ToTable(TableNames.Season);
        
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Show)
            .WithMany(e => e.Seasons)
            .HasForeignKey(e => e.ShowId)
            .IsRequired();

        builder.HasMany(e => e.Episodes)
            .WithOne(e => e.Season)
            .HasForeignKey(e => e.SeasonId)
            .IsRequired();
    }
}