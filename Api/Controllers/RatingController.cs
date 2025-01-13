using Api.Exceptions;
using Api.Services.Abstraction;
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("rating")]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet]
    [Route("{ratingId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetRating(Guid ratingId)
    {
        var rating = await _ratingService.GetRatingAsync(ratingId);
        return rating is not null ? Ok(rating) : NotFound();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateRating([FromBody] RatingCreate request)
    {
        var ratingId = await _ratingService.CreateRatingAsync(request);
        return CreatedAtRoute($"/rating/{ratingId}", null);
    }

    [HttpPut]
    [Route("{ratingId:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateRating(Guid ratingId, [FromBody] RatingUpdate request)
    {
        if (!ratingId.Equals(request.Id))
        {
            throw new ConflictException("Rating IDs in path and body do not match. Cannot perform update.");
        }

        var result = await _ratingService.UpdateRatingAsync(request);
        return Ok(result);
    }

    [HttpDelete]
    [Route("{ratingId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteRating(Guid ratingId, [FromBody] RatingDelete request)
    {
        if (!ratingId.Equals(request.Id))
        {
            throw new ConflictException("Rating IDs in path and body do not match. Cannot perform delete.");
        }

        await _ratingService.DeleteRatingAsync(request.Id);
        return NoContent();
    }

    [HttpGet]
    [Route("user")]
    [Authorize]
    public async Task<IActionResult> GetUserRating([FromBody] UserRatingRequest request)
    {
        var userRating = await _ratingService.GetUserRatingAsync(request);
        return userRating is not null ? Ok(userRating) : NotFound();
    }

    [HttpGet]
    [Route("average")]
    public async Task<IActionResult> GetAverageRating([FromBody] AverageRatingRequest request)
    {
        var averageRating = await _ratingService.GetAverageRatingAsync(request.FilmId, request.ShowId, request.EpisodeId);
        return Ok(averageRating);
    }
}