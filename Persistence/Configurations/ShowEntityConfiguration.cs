using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class ShowEntityConfiguration : IEntityTypeConfiguration<ShowEntity>
{
    private const string ShowId = "show_id";
    private const string GenreId = "genre_id";
    private const string ActorId = "actor_id";
    
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
        
        builder.HasMany(e => e.Characters)
            .WithOne(e => e.Show)
            .HasForeignKey(f => f.ShowId)
            .IsRequired(false);
        
        builder.HasOne(e => e.Image)
            .WithOne(e => e.Show)
            .HasForeignKey<ShowEntity>(e => e.ImageId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
        
        builder.HasMany(e => e.Genres)
            .WithMany(e => e.Shows)
            .UsingEntity(
                TableNames.ShowGenre,
                e => e.HasOne(typeof(ShowEntity)).WithMany().HasForeignKey(ShowId).HasPrincipalKey(nameof(ShowEntity.Id)),
                e => e.HasOne(typeof(GenreEntity)).WithMany().HasForeignKey(GenreId).HasPrincipalKey(nameof(GenreEntity.Id)),
                j => j.HasKey(GenreId, ShowId));
        
        builder.HasMany(e => e.Actors)
            .WithMany(e => e.Shows)
            .UsingEntity(
                TableNames.ShowActor,
                e => e.HasOne(typeof(ShowEntity)).WithMany().HasForeignKey(ShowId).HasPrincipalKey(nameof(ShowEntity.Id)),
                e => e.HasOne(typeof(ActorEntity)).WithMany().HasForeignKey(ActorId).HasPrincipalKey(nameof(ActorEntity.Id)),
                j => j.HasKey(ShowId, ActorId)
            );
    }
}