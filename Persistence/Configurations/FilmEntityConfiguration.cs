using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class FilmEntityConfiguration : IEntityTypeConfiguration<FilmEntity>
{
    private const string ActorId = "actor_id";
    private const string FilmId = "film_id";
    
    public void Configure(EntityTypeBuilder<FilmEntity> builder)
    {
        builder.ToTable(TableNames.Film);

        builder.HasKey(e => e.Id);
        
        builder.HasMany(e => e.Actors)
            .WithMany(e => e.Films)
            .UsingEntity(
                "film_actor",
                e => e.HasOne(typeof(FilmEntity)).WithMany().HasForeignKey(FilmId).HasPrincipalKey(nameof(FilmEntity.Id)),
                e => e.HasOne(typeof(ActorEntity)).WithMany().HasForeignKey(ActorId).HasPrincipalKey(nameof(ActorEntity.Id)),
                j => j.HasKey(FilmId, ActorId)
                
                );
    }
}