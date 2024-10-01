using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POT_semestralka_backend.Models;

[Table(name: "hotel")]
public class Hotel
{
    [Key]
    public int HotelId { get; set; }

    [Column(name: "name", TypeName = "varchar(255)")]
    [Required]
    public string Name { get; set; } = string.Empty;
}