using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class GenreEntity : BaseEntity<Guid>
{
    [Column(name: "name", TypeName = "varchar(50)")]
    public string Name { get; set; }

    public List<FilmEntity> Films { get; set; } = [];
    
    public List<ShowEntity> Shows { get; set; } = [];
}