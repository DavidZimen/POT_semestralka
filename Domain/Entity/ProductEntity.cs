using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class ProductEntity : AuditableEntity<Guid>
{

    [Column(name: "name", TypeName = "varchar(255)")]
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Column(name: "description", TypeName = "varchar(255)")]
    public string? Description { get; set; }
    
    [Column(name:"price", TypeName = "double precision")]
    [Required]
    public double Price { get; set; }
    
    // [ForeignKey(name: "UserId")]
    // [Column(name: "user_id", TypeName = "text")]
    // public string? UserId { get; set; }
    //
    // public UserEntity? User { get; set; }
}