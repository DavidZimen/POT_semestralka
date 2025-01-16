using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Policy;

namespace Api.Controllers;

[ApiController]
[Route("season")]
public class SeasonController : ControllerBase
{
    private readonly ISeasonService _seasonService;

    public SeasonController(ISeasonService seasonService)
    {
        _seasonService = seasonService;
    }

    /// <summary>
    /// Retrieves all seasons or filters by ShowID if provided.
    /// </summary>
    /// <param name="showId">Optional ShowID for filtering seasons.</param>
    /// <returns>List of seasons.</returns>
    [HttpGet]
    public async Task<ActionResult<ICollection<SeasonDto>>> GetSeasons([FromQuery] Guid? showId)
    {
        var seasons = await _seasonService.GetSeasonsAsync(showId);
        return seasons.Count > 0 ? Ok(seasons) : NotFound();
    }

    /// <summary>
    /// Retrieves season by its ID.
    /// </summary>
    /// <param name="seasonId">Season ID for the query.</param>
    /// <returns>Season with the specified ID.</returns>
    [HttpGet]
    [Route("{seasonId:guid}")]
    public async Task<IActionResult> GetSeason([FromRoute] Guid seasonId)
    {
        var season = await _seasonService.GetSeasonByIdAsync(seasonId);
        return season is not null ? Ok(season) : NotFound();
    }

    /// <summary>
    /// Creates a new season.
    /// </summary>
    /// <param name="seasonCreate">Details of the season to create.</param>
    /// <returns>The created season.</returns>
    [HttpPost]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> CreateSeason([FromBody] SeasonCreate seasonCreate)
    {
        var createdSeason = await _seasonService.CreateSeasonAsync(seasonCreate);
        return createdSeason is not null 
            ? Created($"season/{createdSeason.SeasonId}", createdSeason) 
            : Problem("Failed to create the season.");
    }

    /// <summary>
    /// Updates an existing season.
    /// </summary>
    /// <param name="seasonId">ID of the season to update.</param>
    /// <param name="seasonUpdate">Updated details of the season.</param>
    /// <returns>The updated season.</returns>
    [HttpPut]
    [Route("{seasonId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<ActionResult<SeasonDto>> UpdateSeason([FromRoute] Guid seasonId, [FromBody] SeasonUpdate seasonUpdate)
    {
        if (seasonId != seasonUpdate.SeasonId)
        {
            return BadRequest("Season IDs do not match.");
        }

        var updatedSeason = await _seasonService.UpdateSeasonAsync(seasonUpdate);
        return updatedSeason is not null ? Ok(updatedSeason) : Problem("Failed to update the season.");
    }

    /// <summary>
    /// Deletes a season by its ID.
    /// </summary>
    /// <param name="seasonId">ID of the season to delete.</param>
    /// <returns>Result of the deletion operation.</returns>
    [HttpDelete]
    [Route("{seasonId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> DeleteSeason([FromRoute] Guid seasonId)
    {
        var result = await _seasonService.DeleteSeasonAsync(seasonId);
        return result ? NoContent() : Problem("Failed to delete the season.");
    }
}