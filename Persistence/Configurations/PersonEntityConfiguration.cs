using Domain.Entity;
using Domain.Entity.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

public class PersonEntityConfiguration : IEntityTypeConfiguration<PersonEntity>
{
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
        builder.ToTable(TableNames.Person);

        builder.HasKey(e => e.Id);
        
        builder.HasOne(e => e.Actor)
            .WithOne(e => e.Person)
            .HasForeignKey<ActorEntity>(e => e.PersonId)
            .IsRequired();
        
        builder.HasOne(e => e.Director)
            .WithOne(e => e.Person)
            .HasForeignKey<DirectorEntity>(e => e.PersonId)
            .IsRequired();
    }
}