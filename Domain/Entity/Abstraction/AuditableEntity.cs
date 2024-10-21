using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Abstraction;

public abstract class AuditableEntity : BaseEntity
{
    [Column(name: "created_at", TypeName = "timestamp")]
    [Required]
    public DateTime CreatedDate { get; set; }
    
    [Column(name: "modified_at", TypeName = "timestamp")]
    public DateTime? ModifiedDate { get; set; }
    
    [Column(name: "deleted_at", TypeName = "timestamp")]
    public DateTime? DeletedDate { get; set; }
}