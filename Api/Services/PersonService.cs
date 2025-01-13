using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

namespace Api.Services;

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