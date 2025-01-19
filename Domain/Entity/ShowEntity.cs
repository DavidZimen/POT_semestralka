using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class ShowEntity : AuditableImageEntity<Guid>
{
    [Column(name: "title", TypeName = "varchar(100)")]
    [Required]
    public required string Title { get; set; }
    
    [Column(name: "description", TypeName = "text")]
    [Required]
    public required string Description { get; set; }
    
    [Column(name: "release_date", TypeName = "date")]
    [Required]
    public DateOnly ReleaseDate { get; set; }
    
    [Column(name: "end_date", TypeName = "date")]
    public DateOnly? EndDate { get; set; }

    public virtual List<SeasonEntity> Seasons { get; set; } = [];
    
    public virtual List<RatingEntity> Ratings { get; set; } = [];

    public virtual List<GenreEntity> Genres { get; set; } = [];
    
    public virtual List<CharacterEntity> Characters { get; set; } = [];
}