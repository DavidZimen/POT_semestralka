using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Abstraction;

public abstract class BaseEntity<TKey>
{
    [Key]
    [Column(name: "id")]
    public TKey Id { get; }
    
    // Concurrency token for optimistic concurrency control
    [Column(name: "version")]
    [ConcurrencyCheck]
    public int Version { get; } 
}