using Api.Services;
using Microsoft.AspNetCore.Mvc;

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
}