using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class GenreEntity : BaseEntity<Guid>
{
    [Column(name: "name", TypeName = "varchar(50)")]
    public required string Name { get; set; }

    public virtual List<FilmEntity> Films { get; set; } = [];
    
    public virtual List<ShowEntity> Shows { get; set; } = [];
}