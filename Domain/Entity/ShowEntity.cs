using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class ShowEntity : AuditableEntity<Guid>
{
    [Column(name: "title", TypeName = "varchar(100)")]
    [Required]
    public string Title { get; set; }
    
    [Column(name: "description", TypeName = "text")]
    [Required]
    public string Description { get; set; }
    
    public virtual List<SeasonEntity> Seasons { get; set; }
    
    public virtual List<RatingEntity> Ratings { get; set; } = [];

    public virtual List<GenreEntity> Genres { get; set; } = [];
}