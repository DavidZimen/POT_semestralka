using Api.Exceptions;
using Api.Services.Abstraction;
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Policy;

namespace Api.Controllers;

[ApiController]
[Route("person")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<PersonDto>>> GetPersons()
    {
        var persons = await _personService.GetPersonsAsync();
        return persons.Count > 0 ? Ok(persons) : NotFound();
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<PersonDto>> GetPerson(Guid id)
    {
        var person = await _personService.GetPersonByIdAsync(id);
        return person is not null ? Ok(person) : NotFound();
    }

    [HttpPost]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePerson person)
    {
        var personId = await _personService.CreatePersonAsync(person);
        return personId is not null ? Created($"person/{personId}", null) : Problem();
    }

    [HttpPut]
    [Route("{id:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<ActionResult<PersonDto>> UpdatePerson(Guid id, [FromBody] UpdatePerson updatePerson)
    {
        if (!id.Equals(updatePerson.PersonId))
        {
            throw new ConflictException("Person IDs in path and body do not match. Cannot perform update.");
        }

        var person = await _personService.UpdatePersonAsync(updatePerson);
        return person is not null ? Ok(person) : Problem();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        var success = await _personService.DeletePersonAsync(id);
        return success ? NoContent() : Problem();
    }
}