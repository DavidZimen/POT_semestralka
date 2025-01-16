using Api.Services;
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Policy;

namespace Api.Controllers;

[ApiController]
[Route("episode")]
public class EpisodeController : ControllerBase
{
    private readonly IEpisodeService _episodeService;

    public EpisodeController(IEpisodeService episodeService)
    {
        _episodeService = episodeService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<EpisodeDto>>> GetEpisodes([FromQuery] Guid? seasonId)
    {
        var episodes = await _episodeService.GetEpisodesAsync(seasonId);
        return episodes.Count > 0 ? Ok(episodes) : NotFound();
    }

    [HttpGet]
    [Route("{episodeId:guid}")]
    public async Task<IActionResult> GetEpisode([FromRoute] Guid episodeId)
    {
        var episode = await _episodeService.GetEpisodeByIdAsync(episodeId);
        return episode is not null ? Ok(episode) : NotFound();
    }

    [HttpPost]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> CreateEpisode([FromBody] EpisodeCreate episodeCreate)
    {
        var createdEpisode = await _episodeService.CreateEpisodeAsync(episodeCreate);
        return createdEpisode is not null ? Created($"episode/{createdEpisode.EpisodeId}", createdEpisode) : NotFound();
    }

    [HttpPut]
    [Route("{episodeId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<ActionResult<FilmDto>> UpdateEpisode([FromRoute] Guid episodeId, [FromBody] EpisodeUpdate episodeUpdate)
    {
        if (episodeId != episodeUpdate.EpisodeId)
        {
            return BadRequest("Episode Ids do not match");
        }
        
        var updatedEpisode = await _episodeService.UpdateEpisodeAsync(episodeUpdate);
        return updatedEpisode is not null ? Ok(updatedEpisode) : Problem();
    }

    [HttpDelete]
    [Route("{episodeId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> DeleteEpisode([FromRoute] Guid episodeId)
    {
        var result = await _episodeService.DeleteEpisodeAsync(episodeId);
        return result ? NoContent() : Problem();
    }
}