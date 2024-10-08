using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entity;

[Table(name: "hotel")]
public class HotelEntity
{
    [Key]
    public int HotelId { get; set; }

    [Column(name: "name", TypeName = "varchar(255)")]
    [Required]
    public string Name { get; set; } = string.Empty;
}