using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class ActorEntityConfiguration : IEntityTypeConfiguration<ActorEntity>
{
    private const string ActorId = "actor_id";
    private const string FilmId = "film_id";
    private const string ShowId = "show_id";
    
    public void Configure(EntityTypeBuilder<ActorEntity> builder)
    {
        builder.ToTable(TableNames.Actor);

        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Person)
            .WithOne(e => e.Actor)
            .HasForeignKey<ActorEntity>(e => e.PersonId)
            .IsRequired();

        builder.HasMany(e => e.Characters)
            .WithOne(e => e.Actor)
            .HasForeignKey(e => e.ActorId)
            .IsRequired();
        
        builder.HasMany(e => e.Films)
            .WithMany(e => e.Actors)
            .UsingEntity(
                TableNames.FilmActor,
                e => e.HasOne(typeof(FilmEntity)).WithMany().HasForeignKey(FilmId).HasPrincipalKey(nameof(FilmEntity.Id)),
                e => e.HasOne(typeof(ActorEntity)).WithMany().HasForeignKey(ActorId).HasPrincipalKey(nameof(ActorEntity.Id)),
                j => j.HasKey(FilmId, ActorId)
                );

        builder.HasMany(e => e.Shows)
            .WithMany(e => e.Actors)
            .UsingEntity(
                TableNames.ShowActor,
                e => e.HasOne(typeof(ShowEntity)).WithMany().HasForeignKey(ShowId).HasPrincipalKey(nameof(ShowEntity.Id)),
                e => e.HasOne(typeof(ActorEntity)).WithMany().HasForeignKey(ActorId).HasPrincipalKey(nameof(ActorEntity.Id)),
                j => j.HasKey(ShowId, ActorId)
            );
    }
}