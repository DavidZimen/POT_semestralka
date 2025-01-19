using Api.Exceptions;
using Api.Services.Abstraction;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

namespace Api.Services;

/// <summary>
/// Service for communicating with repository related to images.
/// </summary>
public interface IImageService : IService
{
    Task<Guid?> CreateImageAsync(ImageCreate imageCreate);

    Task<ImageEntity?> GetImageByIdAsync(Guid imageId);
    
    Task<bool> DeleteImageAsync(Guid imageId);
}

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IFilmRepository _filmRepository;
    private readonly IShowRepository _showRepository;
    private readonly IEpisodeRepository _episodeRepository;

    public ImageService(IImageRepository imageRepository, IPersonRepository personRepository, IFilmRepository filmRepository, IShowRepository showRepository, IEpisodeRepository episodeRepository)
    {
        _imageRepository = imageRepository;
        _personRepository = personRepository;
        _filmRepository = filmRepository;
        _showRepository = showRepository;
        _episodeRepository = episodeRepository;
    }

    public async Task<Guid?> CreateImageAsync(ImageCreate imageCreate)
    {
        var imageEntity = new ImageEntity
        {
            Name = imageCreate.Name,
            Type = imageCreate.Type,
            Data = imageCreate.Content
        };

        if (imageCreate.PersonId.HasValue)
        {
            imageEntity.Person = await _personRepository.FindByIdAsync(imageCreate.PersonId.Value);
        }
        else if (imageCreate.FilmId.HasValue)
        {
            imageEntity.Film = await _filmRepository.FindByIdAsync(imageCreate.FilmId.Value);
        }
        else if (imageCreate.ShowId.HasValue)
        {
            imageEntity.Show = await _showRepository.FindByIdAsync(imageCreate.ShowId.Value);
        }
        else
        {
            imageEntity.Episode = await _episodeRepository.FindByIdAsync(imageCreate.EpisodeId!.Value);
        }
        
        return (await _imageRepository.CreateAsync(imageEntity)).Id;
    }

    public Task<ImageEntity?> GetImageByIdAsync(Guid imageId)
    {
        return _imageRepository.FindByIdAsync(imageId);
    }

    public async Task<bool> DeleteImageAsync(Guid imageId)
    {
        var imageEntity = await _imageRepository.FindByIdAsync(imageId);
        if (imageEntity is null)
        {
            throw new NotFoundException($"Image with id {imageId} not found");
        }
        return await _imageRepository.DeleteAsync(imageEntity);
    }
}