using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class CharacterEntity : AuditableEntity<Guid>
{
    [Column(name: "name", TypeName = "varchar(50)")]
    [Required]
    public string Name { get; set; }
    
    [ForeignKey(name: nameof(Id))]
    [Column(name: "person_id", TypeName = "uuid")]
    public Guid ActorId { get; set; }
    
    public virtual ActorEntity Actor { get; set; }
    
    [ForeignKey(name: nameof(Id))]
    [Column(name: "film_id", TypeName = "uuid")]
    public Guid? FilmId { get; set; }
    
    public virtual FilmEntity? Film { get; set; }
    
    [ForeignKey(name: nameof(Id))]
    [Column(name: "show_id", TypeName = "uuid")]
    public Guid? ShowId { get; set; }
    
    public virtual ShowEntity? Show { get; set; }
}