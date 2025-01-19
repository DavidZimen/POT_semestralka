using Api.Services;
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Policy;

namespace Api.Controllers;

[ApiController]
[Route("film")]
public class FilmController : ControllerBase
{
    private readonly IFilmService _filmService;

    public FilmController(IFilmService filmService)
    {
        _filmService = filmService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<FilmDto>>> GetFilms()
    {
        var films = await _filmService.GetAllFilmsAsync();
        return Ok(films);
    }

    [HttpGet]
    [Route("{filmId:guid}")]
    public async Task<IActionResult> GetFilm([FromRoute] Guid filmId)
    {
        var film = await _filmService.GetFilmByIdAsync(filmId);
        return film is not null ? Ok(film) : NotFound();
    }

    [HttpPost]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> CreateFilm([FromBody] FilmCreate film)
    {
        var createdFilm = await _filmService.CreateFilmAsync(film);
        return createdFilm is not null ? Created($"film/{createdFilm.FilmId}", createdFilm) : NotFound();
    }

    [HttpPut]
    [Route("{filmId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<ActionResult<FilmDto>> UpdateFilm([FromRoute] Guid filmId, [FromBody] FilmUpdate filmUpdate)
    {
        if (filmId != filmUpdate.FilmId)
        {
            return BadRequest("FilmIds do not match");
        }
        
        var updatedFilm = await _filmService.UpdateFilmAsync(filmUpdate);
        return updatedFilm is not null ? Ok(updatedFilm) : Problem();
    }

    [HttpDelete]
    [Route("{filmId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> DeleteFilm([FromRoute] Guid filmId)
    {
        var result = await _filmService.DeleteFilmAsync(filmId);
        return result ? NoContent() : Problem();
    }

    [HttpGet]
    [Route("{filmId:guid}/character")]
    public async Task<ActionResult<ICollection<CharacterMediaDto>>> GetFilmCharacters([FromRoute] Guid filmId)
    {
        var characters = await _filmService.GetFilmCharactersAsync(filmId);
        return Ok(characters);
    }

    [HttpPost]
    [Route("{filmId:guid}/genre/{genreId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> AddGenreToFilm([FromRoute] Guid filmId, [FromRoute] Guid genreId)
    {
        var result = await _filmService.AddGenreAsync(filmId, genreId);
        return result ? NoContent() : Problem();
    }
    
    [HttpDelete]
    [Route("{filmId:guid}/genre/{genreId:guid}")]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> RemoveGenreFromFilm([FromRoute] Guid filmId, [FromRoute] Guid genreId)
    {
        var result = await _filmService.RemoveGenreAsync(filmId, genreId);
        return result ? NoContent() : Problem();
    }
}