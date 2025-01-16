using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class ImageEntity : BaseEntity<Guid>
{
    [Column(name: "name", TypeName = "varchar(255)")]
    [Required]
    public string Name { get; set; }
    
    [Column(name: "type", TypeName = "varchar(20)")]
    [Required]
    public string Type { get; set; }
    
    [Column(name: "data", TypeName = "bytea")]
    [Required]
    public byte[] data { get; set; }
    
    public virtual PersonEntity? Person { get; set; }
    
    public virtual FilmEntity? Film { get; set; }
    
    public virtual ShowEntity? Show { get; set; }
    
    public virtual EpisodeEntity? Episode { get; set; }
}