using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

/// <summary>
/// Base entity for each entity to extend from.
/// Contains base attributes for id and auditing dates.
/// </summary>
public abstract class BaseEntity
{
    [Key]
    [Column(name: "id")]
    public Guid Id { get; set; }
    
    // Concurrency token for optimistic concurrency control
    [Column(name: "version")]
    [ConcurrencyCheck]
    [Timestamp]
    public byte[] Version { get; set; } 
    
    [Column(name: "created_at", TypeName = "timestamp")]
    [Required]
    public DateTime CreatedDate { get; set; }
    
    [Column(name: "modified_at", TypeName = "timestamp")]
    public DateTime? ModifiedDate { get; set; }
    
    [Column(name: "deleted_at", TypeName = "timestamp")]
    public DateTime? DeletionDate { get; set; }
}