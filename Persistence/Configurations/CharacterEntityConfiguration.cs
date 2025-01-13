using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class CharacterEntityConfiguration : IEntityTypeConfiguration<CharacterEntity>
{
    public void Configure(EntityTypeBuilder<CharacterEntity> builder)
    {
        builder.ToTable(TableNames.Character, t =>
        {
            t.HasCheckConstraint(
                "CHK_Character_FilmOrShow",
                "(film_id IS NOT NULL AND show_id IS NULL) OR (film_id IS NULL AND show_id IS NOT NULL)");
        });
        
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Actor)
            .WithMany(e => e.Characters)
            .HasForeignKey(e => e.ActorId)
            .IsRequired();
        
        builder.HasOne(e => e.Film)
            .WithMany(e => e.Characters)
            .HasForeignKey(e => e.FilmId)
            .IsRequired(false);
        
        builder.HasOne(e => e.Show)
            .WithMany(e => e.Characters)
            .HasForeignKey(e => e.ShowId)
            .IsRequired(false);
    }
}