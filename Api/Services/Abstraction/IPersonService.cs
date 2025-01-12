using Domain.Dto;

namespace Api.Services.Abstraction;

/// <summary>
/// Service for manipulating data for persons.
/// </summary>
public interface IPersonService : IService
{
    /// <summary>
    /// Retrieves Person with provided ID.
    /// </summary>
    Task<PersonDto?> GetPersonByIdAsync(Guid id);
    
    /// <summary>
    /// Retrieves list of all persons from DB.
    /// </summary>
    Task<List<PersonDto>> GetPersonsAsync();
    
    /// <summary>
    /// Creates new Person with provided base on provided parameter.
    /// </summary>
    /// <returns>ID of the person if created successfully.</returns>
    Task<Guid?> CreatePersonAsync(CreatePerson createPerson);

    /// <summary>
    /// Updates the person entity based on new requirements.
    /// </summary>
    Task<PersonDto?> UpdatePersonAsync(UpdatePerson updatePerson);
    
    /// <summary>
    /// Deletes person with provided ID from DB.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeletePersonAsync(Guid id);
}
