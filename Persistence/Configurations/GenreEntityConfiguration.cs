using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence.Constants;

namespace Persistence.Configurations;

public class GenreEntityConfiguration : IEntityTypeConfiguration<GenreEntity>
{
    private const string ShowId = "show_id";
    private const string FilmId = "film_id";
    private const string GenreId = "genre_id";
    
    public void Configure(EntityTypeBuilder<GenreEntity> builder)
    {
        builder.ToTable(TableNames.Genre);
        
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Name)
            .IsUnique()
            .HasDatabaseName("UQ_Genre_Name");
        
        builder.HasMany(e => e.Films)
            .WithMany(e => e.Genres)
            .UsingEntity(
                TableNames.FilmGenre,
                e => e.HasOne(typeof(FilmEntity)).WithMany().HasForeignKey(FilmId).HasPrincipalKey(nameof(FilmEntity.Id)),
                e => e.HasOne(typeof(GenreEntity)).WithMany().HasForeignKey(GenreId).HasPrincipalKey(nameof(GenreEntity.Id)),
                j => j.HasKey(GenreId, FilmId));
        
        builder.HasMany(e => e.Shows)
            .WithMany(e => e.Genres)
            .UsingEntity(
                TableNames.ShowGenre,
                e => e.HasOne(typeof(ShowEntity)).WithMany().HasForeignKey(ShowId).HasPrincipalKey(nameof(ShowEntity.Id)),
                e => e.HasOne(typeof(GenreEntity)).WithMany().HasForeignKey(GenreId).HasPrincipalKey(nameof(GenreEntity.Id)),
                j => j.HasKey(GenreId, ShowId));
    }
}