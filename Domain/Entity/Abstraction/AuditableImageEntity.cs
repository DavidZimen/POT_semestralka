using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Abstraction;

public class AuditableImageEntity<TKey> : AuditableEntity<TKey>, IImageEntity
{
    [Column(name: "image_id", TypeName = "uuid")]
    public Guid? ImageId { get; set; }
    
    public virtual ImageEntity? Image { get; set; }
}