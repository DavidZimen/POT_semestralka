using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Abstraction;

public abstract class BaseEntity
{
    [Key]
    [Column(name: "id")]
    public Guid Id { get; }
    
    // Concurrency token for optimistic concurrency control
    [Column(name: "version")]
    [ConcurrencyCheck]
    public int Version { get; } 
}