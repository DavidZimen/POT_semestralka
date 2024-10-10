using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entity;

[Table(name: "hotel")]
public class HotelEntity : BaseEntity
{
    [Column(name: "name", TypeName = "varchar(255)")]
    [Required]
    public string Name { get; set; } = string.Empty;
}