using System.Collections;
using Api.Services;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("actor")]
public class ActorController : ControllerBase
{
    private readonly IActorService _actorService;

    public ActorController(IActorService actorService)
    {
        _actorService = actorService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection>> GetActors()
    {
        var actors = await _actorService.GetActorsAsync();
        return Ok(actors);
    }

    [HttpGet]
    [Route("{actorId:guid}/media")]
    public async Task<ActionResult<ICollection<CharacterActorDto>>> GetActorMedias(Guid actorId)
    {
        var media = await _actorService.GetActorCharacters(actorId);
        return Ok(media) ;
    }
}