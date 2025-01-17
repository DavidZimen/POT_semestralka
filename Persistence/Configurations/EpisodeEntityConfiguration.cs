using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class EpisodeEntityConfiguration : IEntityTypeConfiguration<EpisodeEntity>
{
    public void Configure(EntityTypeBuilder<EpisodeEntity> builder)
    {
        builder.ToTable(TableNames.Episode);

        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Director)
            .WithMany(e => e.Episodes)
            .HasForeignKey(e => e.DirectorId)
            .IsRequired();

        builder.HasOne(e => e.Season)
            .WithMany(e => e.Episodes)
            .HasForeignKey(e => e.SeasonId)
            .IsRequired();
        
        builder.HasMany(e => e.Ratings)
            .WithOne(e => e.Episode)
            .HasForeignKey(e => e.EpisodeId)
            .IsRequired(false);
        
        builder.HasOne(e => e.Image)
            .WithOne(e => e.Episode)
            .HasForeignKey<EpisodeEntity>(e => e.ImageId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}