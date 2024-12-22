using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class DirectorEntityConfiguration : IEntityTypeConfiguration<DirectorEntity>
{
    public void Configure(EntityTypeBuilder<DirectorEntity> builder)
    {
        builder.ToTable(TableNames.Director);
        
        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.Episodes)
            .WithOne(e => e.Director)
            .HasForeignKey(e => e.DirectorId)
            .IsRequired();
        
        builder.HasMany(e => e.Films)
            .WithOne(e => e.Director)
            .HasForeignKey(e => e.DirectorId)
            .IsRequired();
        
        builder.HasOne(e => e.Person)
            .WithOne(e => e.Director)
            .HasForeignKey<DirectorEntity>(e => e.PersonId)
            .IsRequired();
    }
}