using Domain.Entity;
using Domain.Entity.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class ActorEntityConfiguration : IEntityTypeConfiguration<ActorEntity>
{
    private const string ActorId = "actor_id";
    private const string FilmId = "film_id";
    
    public void Configure(EntityTypeBuilder<ActorEntity> builder)
    {
        builder.ToTable(TableNames.Actor);

        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Person)
            .WithOne(e => e.Actor)
            .HasForeignKey<ActorEntity>(e => e.PersonId)
            .IsRequired();
        
        builder.HasMany(e => e.Films)
            .WithMany(e => e.Actors)
            .UsingEntity(
                "film_actor",
                e => e.HasOne(typeof(FilmEntity)).WithMany().HasForeignKey(FilmId).HasPrincipalKey(nameof(FilmEntity.Id)),
                e => e.HasOne(typeof(ActorEntity)).WithMany().HasForeignKey(ActorId).HasPrincipalKey(nameof(ActorEntity.Id)),
                j => j.HasKey(FilmId, ActorId)
                );
    }
}