using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class ActorEntity : AuditableEntity<Guid>
{
    [ForeignKey(name: nameof(Id))]
    [Column(name: "person_id", TypeName = "uuid")]
    public Guid PersonId { get; set; }
    
    public virtual PersonEntity Person { get; set; }
    
    public virtual List<FilmEntity> Films { get; set; } = [];
    
    public virtual List<EpisodeEntity> Episodes { get; set; } = [];
}