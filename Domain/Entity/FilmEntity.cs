using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class FilmEntity : AuditableEntity<Guid>
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

    public List<ActorEntity> Actors { get; set; } = [];
    
    [ForeignKey(name: nameof(Id))]
    [Column(name: "director_id", TypeName = "uuid")]
    [Required]
    public Guid DirectorId { get; set; }
    
    public DirectorEntity? Director { get; set; }
}