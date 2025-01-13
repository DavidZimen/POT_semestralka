using Api.Services;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("director")]
public class DirectorController : ControllerBase
{
    private readonly IDirectorService _directorService;

    public DirectorController(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    [HttpGet]
    [Route("{directorId:guid}/films")]
    public async Task<ActionResult<ICollection<DirectorFilmDto>>> GetDirectorFilms(Guid directorId)
    {
        var directorFilms = await _directorService.GetDirectorFilmsAsync(directorId);
        return directorFilms.Count > 0 ? Ok(directorFilms) : NotFound();
    }
    
    [HttpGet]
    [Route("{directorId:guid}/shows")]
    public async Task<ActionResult<ICollection<DirectorFilmDto>>> GetDirectorShows(Guid directorId)
    {
        var directorFilms = await _directorService.GetDirectorShowsAsync(directorId);
        return directorFilms.Count > 0 ? Ok(directorFilms) : NotFound();
    }
}