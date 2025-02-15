using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Abstraction;

namespace Domain.Entity;

public class PersonEntity : AuditableImageEntity<Guid>
{
    [Column(name: "first_name", TypeName = "varchar(100)")]
    [Required]
    public required string FirstName { get; set; }
    
    [Column(name: "middle_name", TypeName = "varchar(100)")]
    public string? MiddleName { get; set; }
    
    [Column(name: "last_name", TypeName = "varchar(100)")]
    [Required]
    public required string LastName { get; set; }
    
    [Column(name: "bio", TypeName = "varchar(1000)")]
    public string? Bio { get; set; }
    
    [Column(name: "birth_date", TypeName = "date")]
    [Required]
    public required DateOnly BirthDate { get; set; }
    
    [Column(name: "country", TypeName = "char(2)")]
    [Required]
    public required string Country { get; set; }
    
    public virtual ActorEntity? Actor { get; set; }
    
    public virtual DirectorEntity? Director { get; set; }
}