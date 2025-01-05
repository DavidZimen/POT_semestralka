using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class ShowEntityConfiguration : IEntityTypeConfiguration<ShowEntity>
{
    private const string ShowId = "show_id";
    private const string GenreId = "genre_id";
    
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
        
        builder.HasMany(e => e.Genres)
            .WithMany(e => e.Shows)
            .UsingEntity(
                "show_genre",
                e => e.HasOne(typeof(ShowEntity)).WithMany().HasForeignKey(ShowId).HasPrincipalKey(nameof(ShowEntity.Id)),
                e => e.HasOne(typeof(GenreEntity)).WithMany().HasForeignKey(GenreId).HasPrincipalKey(nameof(GenreEntity.Id)),
                j => j.HasKey(GenreId, ShowId));
    }
}