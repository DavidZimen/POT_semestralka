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
    [Route("{filmId:guid}")]
    public async Task<IActionResult> GetFilm(Guid filmId)
    {
        var film = await _filmService.GetFilmByIdAsync(filmId);
        return film is not null ? Ok(film) : NotFound();
    }

    [HttpPost]
    [Authorize(Policy = nameof(AdminPolicy))]
    public async Task<IActionResult> CreateFilm(FilmCreate film)
    {
        var createdFilm = await _filmService.CreateFilm(film);
        return createdFilm is not null ? Created($"film/{createdFilm.FilmId}", createdFilm) : NotFound();
    }
}