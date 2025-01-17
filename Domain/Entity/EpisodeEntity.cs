using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class EpisodeEntity : AuditableImageEntity<Guid>
{
    [Column(name: "title", TypeName = "varchar(100)")]
    [Required]
    public string Title { get; set; }
    
    [Column(name: "description", TypeName = "text")]
    [Required]
    public string Description { get; set; }
    
    [Column(name: "release_data", TypeName = "date")]
    [Required]
    public DateOnly ReleaseDate { get; set; }
    
    [Column(name: "duration", TypeName = "integer")]
    public int Duration { get; set; }
    
    [Column(name: "director_id", TypeName = "uuid")]
    [Required]
    public Guid DirectorId { get; set; }
    
    public virtual DirectorEntity Director { get; set; }
    
    [Column(name: "season_id", TypeName = "uuid")]
    [Required]
    public Guid SeasonId { get; set; }
    
    public virtual SeasonEntity Season { get; set; }
    
    public virtual List<RatingEntity> Ratings { get; set; } = [];
}