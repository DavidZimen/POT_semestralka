using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class SeasonEntity : AuditableEntity<Guid>
{
    [Column(name: "title", TypeName = "varchar(100)")]
    [Required]
    public required string Title { get; set; }
    
    [Column(name: "description", TypeName = "text")]
    [Required]
    public required string Description { get; set; }
    
    [Column(name: "show_id", TypeName = "uuid")]
    [Required]
    public Guid ShowId { get; set; }
    
    public virtual required ShowEntity Show { get; set; }

    public virtual List<EpisodeEntity> Episodes { get; set; } = [];
}