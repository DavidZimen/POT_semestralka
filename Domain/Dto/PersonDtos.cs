namespace Domain.Dto;

/// <summary>
/// Dto to transfer data about Person to the UI.
/// </summary>
public class PersonDto
{
    public Guid PersonId { get; set; }
    
    public string FirstName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public string LastName { get; set; }
    
    public string? Bio { get; set; }
    
    public DateOnly BirthDate { get; set; }
    
    public string Country { get; set; }
}

/// <summary>
/// Request to create a new person in the database.
/// </summary>
/// <param name="FirstName">First name of person</param>
/// <param name="MiddleName">Middle name of the person. Optional.</param>
/// <param name="LastName">Last name of the person.</param>
/// <param name="Bio">Brief description about persons life.</param>
/// <param name="BirthDate">Date of birth.</param>
/// <param name="Country">Their current nationality.</param>
public record CreatePerson(string FirstName, string? MiddleName, string LastName, string? Bio, DateOnly BirthDate, string Country);

/// <summary>
/// Dto to request update of the person.
/// </summary>
/// <param name="PersonId">Id of the person to be updated</param>
/// <param name="FirstName">First name of person</param>
/// <param name="MiddleName">Middle name of the person. Optional.</param>
/// <param name="LastName">Last name of the person.</param>
/// <param name="Bio">Brief description about persons life.</param>
/// <param name="BirthDate">Date of birth.</param>
/// <param name="Country">Their current nationality.</param>
public record UpdatePerson(Guid PersonId, string FirstName, string? MiddleName, string LastName, string? Bio, DateOnly BirthDate, string Country);
