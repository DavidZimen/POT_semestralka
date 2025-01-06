using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class RatingEntity : BaseEntity<Guid>, IOwnerableEntity
{
    [Column(name: "value", TypeName = "integer")]
    [Required]
    public int Value { get; set; }
    
    [Column(name: "description", TypeName = "text")]
    public string? Description { get; set; }
    
    [Column(name: "user_id", TypeName = "varchar(36)")]
    public string UserId { get; set; }
    
    public UserEntity User { get; set; }
    
    [Column(name: "film_id", TypeName = "uuid")]
    public Guid? FilmId { get; set; }
    
    public FilmEntity? Film { get; set; }
    
    [Column(name: "show_id", TypeName = "uuid")]
    public Guid? ShowId { get; set; }
    
    public ShowEntity? Show { get; set; }
    
    [Column(name: "episode_id", TypeName = "uuid")]
    public Guid? EpisodeId { get; set; }
    
    public EpisodeEntity? Episode { get; set; }

    public bool IsOwner(string? userId)
    {
        return UserId.Equals(userId);
    }
}