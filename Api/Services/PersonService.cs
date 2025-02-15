using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

namespace Api.Services;

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

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<PersonDto?> GetPersonByIdAsync(Guid id)
    {
        var personEntity = await _personRepository.FindByIdAsync(id);
        return _mapper.Map<PersonDto>(personEntity);
    }

    public async Task<List<PersonDto>> GetPersonsAsync()
    {
        return (await _personRepository.GetAllAsync())
            .Select(_mapper.Map<PersonDto>)
            .ToList();
    }

    public async Task<Guid?> CreatePersonAsync(CreatePerson createPerson)
    {
        var personEntity = _mapper.Map<PersonEntity>(createPerson);
        return (await _personRepository.CreateAsync(personEntity)).Id;
    }

    public async Task<PersonDto?> UpdatePersonAsync(UpdatePerson updatePerson)
    {
        var personEntity = await _personRepository.FindByIdAsync(updatePerson.PersonId);
        if (personEntity is null)
        {
            throw new NotFoundException($"Person with id {updatePerson.PersonId} does not exist.");
        }
        
        _mapper.Map(updatePerson, personEntity);
        return _mapper.Map<PersonDto>(await _personRepository.UpdateAsync(personEntity));
    }

    public async Task<bool> DeletePersonAsync(Guid id)
    {
        var personEntity = await _personRepository.FindByIdAsync(id);
        if (personEntity is null)
        {
            throw new NotFoundException($"Person with id {id} does not exist.");
        }

        return await _personRepository.DeleteAsync(personEntity);
    }
}