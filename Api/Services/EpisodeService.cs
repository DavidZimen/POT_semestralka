using Api.Services.Abstraction;
using AutoMapper;
using Persistence.Repositories;

namespace Api.Services;

/// <summary>
/// Service for communicating with repository related to episodes.
/// </summary>
public interface IEpisodeService : IService;

public class EpisodeService : IEpisodeService
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IMapper _mapper;

    public EpisodeService(IEpisodeRepository episodeRepository, IMapper mapper)
    {
        _episodeRepository = episodeRepository;
        _mapper = mapper;
    }
}