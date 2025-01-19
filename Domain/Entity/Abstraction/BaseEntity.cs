using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Abstraction;

public abstract class BaseEntity<TKey>
{
    [Key]
    [Column(name: "id", TypeName = "uuid")]
    public TKey? Id { get; set; }
    
    // Concurrency token for optimistic concurrency control
    [Column(name: "version")]
    [ConcurrencyCheck]
    public int Version { get; } = 0;
}