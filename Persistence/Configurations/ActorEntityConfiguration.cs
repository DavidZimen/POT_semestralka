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
    }
}