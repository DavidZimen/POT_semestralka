using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class EpisodeEntityConfiguration : IEntityTypeConfiguration<EpisodeEntity>
{
    private const string ActorId = "actor_id";
    private const string EpisodeId = "episode_id";
    
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
        
        builder.HasMany(e => e.Actors)
            .WithMany(e => e.Episodes)
            .UsingEntity(
                "episode_actor",
                e => e.HasOne(typeof(EpisodeEntity)).WithMany().HasForeignKey(EpisodeId).HasPrincipalKey(nameof(EpisodeEntity.Id)),
                e => e.HasOne(typeof(ActorEntity)).WithMany().HasForeignKey(ActorId).HasPrincipalKey(nameof(ActorEntity.Id)),
                j => j.HasKey(EpisodeId, ActorId)
            );
    }
}