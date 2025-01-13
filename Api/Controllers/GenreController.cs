using Api.Exceptions;
using Api.Services.Abstraction;
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Policy;

namespace Api.Controllers;

[ApiController]
[Route("genre")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<GenreDto>>> GetGenres()
    {
        var genres = await _genreService.GetGenresAsync();
        return genres.Count > 0 ? Ok(genres) : NotFound();
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<GenreDto>> GetGenre(Guid id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);
        return genre is not null ? Ok(genre) : NotFound();
    }

    [HttpPost]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> CreateGenre([FromBody] GenreCreate genre)
    {
        var genreId = await _genreService.CreateGenreAsync(genre.Name);
        return genreId is not null ? Created($"genre/{genreId}", null) : Problem();
    }

    [HttpPut]
    [Route("{id:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<ActionResult<GenreDto>> UpdateGenre(Guid id, [FromBody] GenreUpdate genreUpdate)
    {
        if (!id.Equals(genreUpdate.GenreId))
        {
            throw new ConflictException("Genre IDs in path and body do not match. Cannot perform update.");
        }

        var genre = await _genreService.UpdateGenreAsync(genreUpdate);
        return genre is not null ? Ok(genre) : Problem();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> DeleteGenre(Guid id)
    {
        var success = await _genreService.DeleteGenreAsync(id);
        return success ? NoContent() : Problem();
    }
}