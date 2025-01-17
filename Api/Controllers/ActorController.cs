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
        return actors.Count > 0 ? Ok(actors) : NotFound();
    }

    [HttpGet]
    [Route("{actorId:guid}/media")]
    public async Task<ActionResult<ICollection<CharacterActorDto>>> GetActorMedias(Guid actorId)
    {
        var media = await _actorService.GetActorCharacters(actorId);
        return media.Count > 0 ? Ok(media) : NoContent();
    }
}