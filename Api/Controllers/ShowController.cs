using Api.Services;
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Policy;

namespace Api.Controllers;

[ApiController]
[Route("show")]
public class ShowController : ControllerBase
{
    private readonly IShowService _showService;

    public ShowController(IShowService showService)
    {
        _showService = showService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<ShowDto>>> GetShows()
    {
        var shows = await _showService.GetAllShowsAsync();
        return shows.Count > 0 ? Ok(shows) : NotFound();
    }

    [HttpGet]
    [Route("{showId:guid}")]
    public async Task<IActionResult> GetShow([FromRoute] Guid showId)
    {
        var show = await _showService.GetShowByIdAsync(showId);
        return show is not null ? Ok(show) : NotFound();
    }

    [HttpPost]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> CreateShow([FromBody] ShowCreate show)
    {
        var createdShow = await _showService.CreateShowAsync(show);
        return createdShow is not null ? Created($"show/{createdShow.ShowId}", createdShow) : NotFound();
    }

    [HttpPut]
    [Route("{showId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<ActionResult<ShowDto>> UpdateShow([FromRoute] Guid showId, [FromBody] ShowUpdate showUpdate)
    {
        if (showId != showUpdate.ShowId)
        {
            return BadRequest("ShowIds do not match");
        }
        
        var updatedShow = await _showService.UpdateShowAsync(showUpdate);
        return updatedShow is not null ? Ok(updatedShow) : Problem();
    }

    [HttpDelete]
    [Route("{showId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> DeleteShow([FromRoute] Guid showId)
    {
        var result = await _showService.DeleteShowAsync(showId);
        return result ? NoContent() : Problem();
    }

    [HttpGet]
    [Route("{showId:guid}/character")]
    public async Task<ActionResult<ICollection<CharacterMediaDto>>> GetShowCharacters([FromRoute] Guid showId)
    {
        var characters = await _showService.GetShowCharactersAsync(showId);
        return characters.Count != 0 ? Ok(characters) : NotFound();
    }

    [HttpPost]
    [Route("{showId:guid}/genre/{genreId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> AddGenreToShow([FromRoute] Guid showId, [FromRoute] Guid genreId)
    {
        var result = await _showService.AddGenreAsync(showId, genreId);
        return result ? NoContent() : Problem();
    }
    
    [HttpDelete]
    [Route("{showId:guid}/genre/{genreId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> RemoveGenreFromShow([FromRoute] Guid showId, [FromRoute] Guid genreId)
    {
        var result = await _showService.RemoveGenreAsync(showId, genreId);
        return result ? NoContent() : Problem();
    }
}
