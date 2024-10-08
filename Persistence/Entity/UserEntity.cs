using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Entity;

public class UserEntity : IdentityUser<int>
{
    [Column(name: "first_name", TypeName = "varchar(50)")]
    [Required(ErrorMessage = "Firstname is required")]
    public string FirstName { get; set; } = string.Empty;
    
    [Column(name: "last_name", TypeName = "varchar(50)")]
    [Required(ErrorMessage = "Lastname is required")]
    public string LastName { get; set; } = string.Empty;
}