using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class FilmEntityConfiguration : IEntityTypeConfiguration<FilmEntity>
{
    private const string ActorId = "actor_id";
    private const string FilmId = "film_id";
    private const string GenreId = "genre_id";
    
    public void Configure(EntityTypeBuilder<FilmEntity> builder)
    {
        builder.ToTable(TableNames.Film);

        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.Ratings)
            .WithOne(e => e.Film)
            .HasForeignKey(e => e.FilmId)
            .IsRequired(false);
        
        builder.HasMany(e => e.Actors)
            .WithMany(e => e.Films)
            .UsingEntity(
                "film_actor",
                e => e.HasOne(typeof(FilmEntity)).WithMany().HasForeignKey(FilmId).HasPrincipalKey(nameof(FilmEntity.Id)),
                e => e.HasOne(typeof(ActorEntity)).WithMany().HasForeignKey(ActorId).HasPrincipalKey(nameof(ActorEntity.Id)),
                j => j.HasKey(FilmId, ActorId)
                
                );

        builder.HasMany(e => e.Genres)
            .WithMany(e => e.Films)
            .UsingEntity(
                "film_genre",
                e => e.HasOne(typeof(FilmEntity)).WithMany().HasForeignKey(FilmId)
                    .HasPrincipalKey(nameof(FilmEntity.Id)),
                e => e.HasOne(typeof(GenreEntity)).WithMany().HasForeignKey(GenreId)
                    .HasPrincipalKey(nameof(GenreEntity.Id)),
                j => j.HasKey(GenreId, FilmId));
    }
}