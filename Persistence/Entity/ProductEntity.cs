using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entity;

[Table(name: "product")]
public class ProductEntity
{
    [Key]
    public int ProductId { get; set; }

    [Column(name: "name", TypeName = "varchar(255)")]
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Column(name: "description", TypeName = "varchar(255)")]
    public string? Description { get; set; }
    
    [Column(name:"price", TypeName = "double precision")]
    [Required]
    public double Price { get; set; }
}