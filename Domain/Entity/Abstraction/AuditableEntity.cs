using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Abstraction;

public abstract class AuditableEntity<TKey> : BaseEntity<TKey>, IAuditableEntity
{
    [Column(name: "created_at", TypeName = "timestamp with time zone")]
    [Required]
    public DateTime CreatedDate { get; set; }
    
    [Column(name: "modified_at", TypeName = "timestamp with time zone")]
    public DateTime? ModifiedDate { get; set; }
    
    [Column(name: "deleted_at", TypeName = "timestamp with time zone")]
    public DateTime? DeletedDate { get; set; }

    [Column(name: "modified_by", TypeName = "varchar(255)")]
    [Required]
    public string LastModifiedBy { get; set; } = string.Empty;
}