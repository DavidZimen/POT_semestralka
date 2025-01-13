using Api.Services;
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Policy;

namespace Api.Controllers;

[ApiController]
[Route("character")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<CharacterDto>>> GetCharacters([FromQuery] Guid? filmId, [FromQuery] Guid? showId)
    {
        var characters = await _characterService.GetCharactersAsync(filmId, showId);
        return characters.Count > 0 ? Ok(characters) : NotFound();
    }

    [HttpPost]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> CreateCharacter([FromBody] CharacterCreate characterCreate)
    {
        var character = await _characterService.CreateCharacterAsync(characterCreate);
        return Created($"character/{character.CharacterId}", character);
    }
}