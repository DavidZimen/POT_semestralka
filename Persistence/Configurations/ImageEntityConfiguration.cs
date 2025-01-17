using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class ImageEntityConfiguration : IEntityTypeConfiguration<ImageEntity>
{
    public void Configure(EntityTypeBuilder<ImageEntity> builder)
    {
        builder.ToTable(TableNames.Image);
        
        builder.HasKey(e => e.Id);
        
        builder.HasOne(e => e.Person)
            .WithOne(e => e.Image)
            .HasForeignKey<PersonEntity>(e => e.ImageId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
        
        builder.HasOne(e => e.Episode)
            .WithOne(e => e.Image)
            .HasForeignKey<EpisodeEntity>(e => e.ImageId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
        
        builder.HasOne(e => e.Film)
            .WithOne(e => e.Image)
            .HasForeignKey<FilmEntity>(e => e.ImageId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
        
        builder.HasOne(e => e.Show)
            .WithOne(e => e.Image)
            .HasForeignKey<ShowEntity>(e => e.ImageId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}