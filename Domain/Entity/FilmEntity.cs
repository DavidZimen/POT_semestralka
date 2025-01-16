using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class FilmEntity : AuditableImageEntity<Guid>
{
    [Column(name: "title", TypeName = "varchar(100)")]
    [Required]
    public string Title { get; set; }
    
    [Column(name: "description", TypeName = "text")]
    [Required]
    public string Description { get; set; }
    
    [Column(name: "release_date", TypeName = "date")]
    [Required]
    public DateOnly ReleaseDate { get; set; }
    
    [Column(name: "duration", TypeName = "integer")]
    public int Duration { get; set; }
    
    [Column(name: "director_id", TypeName = "uuid")]
    [Required]
    public Guid DirectorId { get; set; }
    
    public virtual DirectorEntity Director { get; set; }
    
    public virtual List<ActorEntity> Actors { get; set; } = [];
    
    public virtual List<RatingEntity> Ratings { get; set; } = [];
    
    public virtual List<GenreEntity> Genres { get; set; } = [];
    
    public virtual List<CharacterEntity> Characters { get; set; } = [];
}