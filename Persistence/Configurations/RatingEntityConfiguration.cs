using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class RatingEntityConfiguration : IEntityTypeConfiguration<RatingEntity>
{
    public void Configure(EntityTypeBuilder<RatingEntity> builder)
    {
        builder.ToTable(TableNames.Rating, t =>
        {
            t.HasCheckConstraint("CHK_Rating_value", "\"value\" >= 1 AND \"value\" <= 10");
            t.HasCheckConstraint("CHK_One_Type_Not_Null", "\"film_id\" IS NOT NULL OR \"show_id\" IS NOT NULL OR \"episode_id\" IS NOT NULL");
        });
        
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.User)
            .WithMany(e => e.Ratings)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        
        builder.HasOne(e => e.Film)
            .WithMany(e => e.Ratings)
            .HasForeignKey(e => e.FilmId)
            .IsRequired(false);
        
        builder.HasOne(e => e.Show)
            .WithMany(e => e.Ratings)
            .HasForeignKey(e => e.ShowId)
            .IsRequired(false);
        
        builder.HasOne(e => e.Episode)
            .WithMany(e => e.Ratings)
            .HasForeignKey(e => e.EpisodeId)
            .IsRequired(false);
    }
}