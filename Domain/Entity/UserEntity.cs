using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class UserEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column(name: "id", TypeName = "varchar(36)")]
    public required string Id { get; set; }
    
    [Column(name: "enabled", TypeName = "boolean")]
    public bool Enabled { get; set; }
    
    public virtual List<RatingEntity> Ratings { get; set; } = [];
}